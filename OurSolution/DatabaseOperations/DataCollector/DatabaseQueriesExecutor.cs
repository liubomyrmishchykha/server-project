using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;


namespace DataCollector
{
    class DatabaseQueriesExecutor
    {
        private readonly SqlConnection _sqlConnection;
        private readonly List<BriefDatabase> _dbList;
        private readonly int _instanceId;


        public DatabaseQueriesExecutor(SqlConnection sqlConnection, List<BriefDatabase> dbList, int instanceId)
        {
            _sqlConnection = sqlConnection;
            _dbList = dbList;
            _instanceId = instanceId;
        }


        public void ExecuteDatabaseQueries()
        {
            //Starting new tasks for each database
            List<Task> databaseTasks = new List<Task>();
            _dbList.ForEach((db) => databaseTasks.Add(Task.Factory.StartNew(() =>
            {
                RepositoryAccess repositoryAccess = new RepositoryAccess();
                db.InstanceId = _instanceId;
                db.Id = repositoryAccess.SaveDatabase(db);
                //If database' state is not online, finishing task, else collecting data about database' tables
                if (db.DbState > 0) return;
                GetListofTables getListofTables = new GetListofTables(_sqlConnection, db);
                getListofTables.Run();
                repositoryAccess.SaveTables(getListofTables.ListofTables);
            }
            )));
            try
            {
                Task.WaitAll(databaseTasks.ToArray());
            }
            catch (AggregateException ex)
            {
                throw ex.Flatten();
            }
        }
    }
}
