using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Models;
using Models.Constants;
using Repository.DataMapper;
using Repository.Interfaces;

namespace Repository.QueryBuilder
{
    public class QueryBuilder<T> : IQueryBuilder<T>
    {
        public Query GetAll<T>()
        {
            Query query = new Query();
            query.BuildStoredProcedureName<T>(StoredProcedureConstants.GetAll);
            return query;
        }

        public Query SelectById<T>(int id)
        {
            Query query = new Query();
            if (id <= 0)
                throw new ArgumentException("Id can't be less than 0");

            query.BuildStoredProcedureName<T>(StoredProcedureConstants.SelectById);
            query.AddParameters(new List<SqlParameter>() { new SqlParameter("@Id", id) });
            return query;
        }

        public Query Add<T>(T obj)
        {
            Query query = new Query();
            if (obj == null)
                throw new NullReferenceException("Passed object can't be null");

            query.BuildStoredProcedureName<T>(StoredProcedureConstants.Add);
            query.AddParameters(GetParametersFromObject(obj));
            return query;
        }

        public Query Update<T>(T obj)
        {
            Query query = new Query();
            if (obj == null)
                throw new NullReferenceException("Passed object can't be null");

            query.BuildStoredProcedureName<T>(StoredProcedureConstants.Update);
            query.AddParameters(GetParametersFromObject(obj));
            return query;
        }

        public Query Delete<T>(int id)
        {
            Query query = new Query();
            if (id <= 0)
                throw new ArgumentException("Id can't be less than 0");

            query.BuildStoredProcedureName<T>(StoredProcedureConstants.Delete);
            query.AddParameters(new List<SqlParameter>() { new SqlParameter("@Id", id) });
            return query;
        }

        public Query Exists<T>(T obj)
        {
            Query query = new Query();
            if (obj == null)
                throw new ArgumentException();
            query.BuildStoredProcedureName<T>(StoredProcedureConstants.Exists);
            query.AddParameters(GetParametersFromObject(obj));
            return query;
        }

        public Query Count<T>()
        {
            Query query = new Query();
            query.BuildStoredProcedureName<T>(StoredProcedureConstants.Count);
            query.AddParameters(new List<SqlParameter>() { new SqlParameter("@RowsCount", SqlDbType.Int) });
            return query;
        }

        public Query Search<T>(SearchDataOut search)
        {
            Query query = new Query();
            if (search == null)
                throw new ArgumentException();

            query.BuildStoredProcedureName<T>(StoredProcedureConstants.Search);
            query.AddParameters(GetParametersFromObject(search));
            return query;
        }


        public Query SelectDatabasesByInstanceId(int id)
        {
            Query query = new Query();
            if (id <= 0)
                throw new ArgumentException("Id can't be less than 0");

            query.BuildStoredProcedureName<Database>(StoredProcedureConstants.SelectByInstanceId);
            query.AddParameters(new List<SqlParameter>() { new SqlParameter("@Id", id) });
            return query;
        }

        public Query SelectTablesByDatabaseIds(int[] ids)
        {
            Query query = new Query();
            DataTable DatabaseIds = new DataTable();    //temporary table to set array of Database Ids
            DatabaseIds.Columns.Add("DatabaseId", typeof(Int32));
            DataRow workRow;

            if (ids.Length > 0)
            {
                foreach (int id in ids)
                {
                    workRow = DatabaseIds.NewRow();
                    workRow["DatabaseId"] = id;
                    DatabaseIds.Rows.Add(workRow);
                }
            }
            query.BuildStoredProcedureName<Table>(StoredProcedureConstants.SelectByDatabaseIds);
            query.AddParameters(new List<SqlParameter>() { new SqlParameter("@Ids", DatabaseIds) });
            return query;
        }



        private List<SqlParameter> GetParametersFromObject<T>(T obj)
        {
            if (obj == null)
                throw new ArgumentException();

            IEnumerable<TypePropery> typeMetadata = TypeMetadataProvider.GetPropertiesSqlName<T>();
            List<SqlParameter> parameters = new List<SqlParameter>();
            foreach (TypePropery typeProperty in typeMetadata)
            {
                parameters.Add(new SqlParameter(string.Format(StoredProcedureConstants.ParameterNamePattern, typeProperty.SqlName), //Add parameter name from Attribute
                    obj.GetType().GetProperty(typeProperty.PropertyName).GetValue(obj))); // Add parameter value
            }
            return parameters;
        }



    }
}
