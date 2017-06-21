using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Models;
using Repository.QueryExecutor;
using Repository.QueryBuilder;
using Repository.Interfaces;

namespace Repository
{
    public class Repository<T> : IRepository<T> where T : class, new()
    {
        QueryBuilder<T> queryBuilder = new QueryBuilder<T>();
        QueryExecutor.QueryExecutor queryExecutor = new QueryExecutor.QueryExecutor();
        ConnectionManager.ConnectionManager connectionManager = new ConnectionManager.ConnectionManager();

        public List<T> GetAll()
        {
            Query query = queryBuilder.GetAll<T>();
            using (SqlConnection connection = connectionManager.GetOpenedRepositoryConnection())
            {
                QueryResult result = queryExecutor.Execute(query, connection);
                T inst = DataMapper.DataMapper.Map<T>(result);
                List<T> instList = DataMapper.DataMapper.MapList<T>(result);
                return instList;
            }
        }

        public void Add(T obj)
        {
            Query query = queryBuilder.Add<T>(obj);
            using (SqlConnection connection = connectionManager.GetOpenedRepositoryConnection())
            {
                QueryResult result = queryExecutor.Execute(query, connection);
            }
        }

        public void Delete(int id)
        {
            Query query = queryBuilder.Delete<T>(id);
            using (SqlConnection connection = connectionManager.GetOpenedRepositoryConnection())
            {
                QueryResult result = queryExecutor.Execute(query, connection);
            }
        }

        public T SelectByID(int id)
        {
            Query query = queryBuilder.SelectById<T>(id);
            using (SqlConnection connection = connectionManager.GetOpenedRepositoryConnection())
            {
                QueryResult result = queryExecutor.Execute(query, connection);
                T instance = DataMapper.DataMapper.Map<T>(result);
                return instance;
            }
        }

        public void Update(T obj)
        {
            Query query = queryBuilder.Update<T>(obj);
            using (SqlConnection connection = connectionManager.GetOpenedRepositoryConnection())
            {
                QueryResult result = queryExecutor.Execute(query, connection);
            }
        }

        public int Exists(T obj)
        {
            Query query = queryBuilder.Exists(obj);
            using (SqlConnection connection = connectionManager.GetOpenedRepositoryConnection())
            {
                QueryResult result = queryExecutor.Execute(query, connection);
                return result.HasResult() ? result.GetId() : 0;
            }
        }

        public int Count()
        {
            Query query = queryBuilder.Count<T>();
            using (SqlConnection connection = connectionManager.GetOpenedRepositoryConnection())
            {
                QueryResult result = queryExecutor.Execute(query, connection);
                return result.RowCount();
            }
        }

        public Tuple<int, List<T>> Search(SearchDataOut search)
        {
            Query query = queryBuilder.Search<T>(search);
            using (SqlConnection connection = connectionManager.GetOpenedRepositoryConnection())
            {
                QueryResult result = queryExecutor.Execute(query, connection);
                int count = result.RowCount();
                T inst = DataMapper.DataMapper.Map<T>(result);
                List<T> genericList = DataMapper.DataMapper.MapList<T>(result);
                var items = Tuple.Create(count, genericList);
                return items;
            }
        }

        public InstanceWithUserInfo SelectInstanceByID(int id)
        {
            Query query = queryBuilder.SelectById<InstanceWithUserInfo>(id);
            InstanceWithUserInfo instance;
            using (SqlConnection connection = connectionManager.GetOpenedRepositoryConnection())
            {
                QueryResult result = queryExecutor.Execute(query, connection);
                instance = DataMapper.DataMapper.Map<InstanceWithUserInfo>(result);
            }
            if (instance.Id != 0)
            {
                instance.Databases = GetDatabases(instance.Id);
            }
            return instance;

        }

        private List<Database> GetDatabases(int id)
        {
            Query query = queryBuilder.SelectDatabasesByInstanceId(id);
            List<Database> databases;
            using (SqlConnection connection = connectionManager.GetOpenedRepositoryConnection())
            {
                QueryResult result = queryExecutor.Execute(query, connection);
                databases = DataMapper.DataMapper.MapList<Database>(result);
            }
            if (databases != null && databases.Count > 0)
            {
                int[] ids = databases.AsEnumerable().Select(r => (int)r.Id).ToArray();
                List<Table> tables = GetTables(ids);
                foreach (Database database in databases)
                {
                    database.Tables = tables.Where(t => t.DatabaseId == database.Id).ToList();
                }
            }
            return databases;
        }

        private List<Table> GetTables(int[] id)
        {
            Query query = queryBuilder.SelectTablesByDatabaseIds(id);
            using (SqlConnection connection = connectionManager.GetOpenedRepositoryConnection())
            {
                QueryResult result = queryExecutor.Execute(query, connection);
                List<Table> tables = DataMapper.DataMapper.MapList<Table>(result);
                return tables;
            }
        }
    }
}
