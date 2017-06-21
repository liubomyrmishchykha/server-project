using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using LoggerService;
using Models;

namespace DataCollector
{
    public class AllQueriesExecutor
    {
        private readonly SqlConnection _sqlConnection;
        GetBaseInstanceInfo getBaseInstanceInfo;
        GetListofDatabases getListofDatabases;
        private RepositoryAccess repositoryAccess;


        public AllQueriesExecutor(SqlConnection sqlConnection)
        {
            _sqlConnection = sqlConnection;
            getBaseInstanceInfo = new GetBaseInstanceInfo(_sqlConnection);
            getListofDatabases = new GetListofDatabases(_sqlConnection);
            repositoryAccess = new RepositoryAccess();
        }


        public void ExecuteAllQueries(int instanceId, int? userId)
        {
            using (_sqlConnection)
            {
                List<Task> tasks = new List<Task>()
                {
                    //Task for collecting basic information about instance
                    Task.Factory.StartNew(() =>
                    {
                        getBaseInstanceInfo.Run();
                        Instance instance = getBaseInstanceInfo.Instance;
                        instance.Id = instanceId;
                        instance.UserId = userId;
                        repositoryAccess.UpdateInstance(instance);
                    }),
                    //Getting list of databases from instance
                    Task.Factory.StartNew(() =>
                    {
                        getListofDatabases.Run();
                        List<BriefDatabase> dbList = getListofDatabases.DbList;
                        DatabaseQueriesExecutor dbExecutor = new DatabaseQueriesExecutor(_sqlConnection, dbList, instanceId);
                        dbExecutor.ExecuteDatabaseQueries();
                    })
                };
                try
                {
                    Task.WaitAll(tasks.ToArray());
                    string msg = string.Format("Have been successfully updated information on {0}",
                        _sqlConnection.DataSource);
                    Logger.WriteLog(LogLevel.Info, msg);
                }
                catch (AggregateException ex)
                {
                    throw ex.Flatten();
                }
            }
        }
    }
}