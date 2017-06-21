using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Models.Constants;
using Repository.DataMapper;
using Repository.Interfaces;

namespace Repository.QueryBuilder
{
    public class Query : IQuery
    {
        private string _storedProcedureName;
        private List<SqlParameter> _parameters = new List<SqlParameter>();

        public void BuildStoredProcedureName<T>(string operation)
        {
            if (string.IsNullOrEmpty(operation))
                throw new ArgumentException("Operation can't be null or empty");
            string typeName = TypeMetadataProvider.GetTypeSqlName<T>();
            _storedProcedureName = string.Format(StoredProcedureConstants.ProcedureNamePattern, operation, typeName);
        }

        public void AddParameters(List<SqlParameter> parameters)
        {
            _parameters.AddRange(parameters);
        }

        public string GetProcedureName()
        {
            return _storedProcedureName;
        }

        public SqlParameter[] GetParameters()
        {
            return _parameters.ToArray();
        }
    }
}
