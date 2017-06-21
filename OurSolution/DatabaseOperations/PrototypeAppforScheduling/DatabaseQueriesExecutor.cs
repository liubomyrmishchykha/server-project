using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using PrototypeAppforScheduling.Models;

namespace PrototypeAppforScheduling
{
    class DatabaseQueriesExecutor
    {
        private readonly SqlConnection _sqlConnection;
        private readonly List<Database> _dbList;
        private readonly int _instanceId;

        public DatabaseQueriesExecutor(SqlConnection sqlConnection, List<Database> dbList, int instanceId)
        {
            _sqlConnection = sqlConnection;
            _dbList = dbList;
            _instanceId = instanceId;
        }

        public void ExecuteDatabaseQueries()
        {
            List<Task> databaseTasks = new List<Task>();
            _dbList.ForEach((db) => databaseTasks.Add(Task.Factory.StartNew(() =>
            {
                RepositoryAccess repositoryAccess=new RepositoryAccess();
                db.InstanceId = _instanceId;
                int dbId = repositoryAccess.SaveDatabase(db);
                GetListofTables getListofTables = new GetListofTables(_sqlConnection, db);
                getListofTables.Run();
                repositoryAccess.SaveTables(dbId, getListofTables.ListofTables);
            }
            )));
            Task.WaitAll(databaseTasks.ToArray());
        }
    }
}
