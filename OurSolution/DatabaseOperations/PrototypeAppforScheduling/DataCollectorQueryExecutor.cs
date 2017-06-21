using Models;
using PrototypeAppforScheduling.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace PrototypeAppforScheduling
{
    public class DataCollectorQueryExecutor
    {
        private readonly SqlConnection _sqlConnection;
        GetBaseInstanceInfo getBaseInstanceInfo;
        GetListofDatabases getListofDatabases;
        private RepositoryAccess repositoryAccess;

        public DataCollectorQueryExecutor(SqlConnection sqlConnection)
        {
            _sqlConnection = sqlConnection;
            getBaseInstanceInfo = new GetBaseInstanceInfo(_sqlConnection);
            getListofDatabases = new GetListofDatabases(_sqlConnection);
            repositoryAccess = new RepositoryAccess();
        }

        public void ExecuteInstanceQueries(int instanceId)
        {
            using (_sqlConnection)
            {
                List<Task> tasks = new List<Task>()
                {
                    Task.Factory.StartNew(() =>
                    {
                        getBaseInstanceInfo.Run();
                        Instance instance = getBaseInstanceInfo.Instance;
                        instance.Id = instanceId;
                        instance.Modified = DateTime.Now;
                        repositoryAccess.UpdateInstance(instance);
                    }),
                    Task.Factory.StartNew(() =>
                    {
                        getListofDatabases.Run();
                        List<Database> dbList = getListofDatabases.DbList;
                        DatabaseQueriesExecutor dbExecutor = new DatabaseQueriesExecutor(_sqlConnection, dbList, instanceId);
                        dbExecutor.ExecuteDatabaseQueries();
                    })
                };
                Task.WaitAll(tasks.ToArray());
            }
        }
    }
}