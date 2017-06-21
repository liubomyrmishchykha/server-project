using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Repository.ConnectionManager;

namespace PrototypeAppforScheduling
{
    public static class DataCollector
    {
        private static RepositoryAccess repositoryAccess;
        private static ConnectionManager manager;

        static DataCollector()
        {
            repositoryAccess = new RepositoryAccess();
            manager = new ConnectionManager();
        }

        public static void CollectData()
        {
            List<InstanceInfo> listInstanceInfos = repositoryAccess.GetAllInstanceInfo();
            var tasks = new List<Task>();
            listInstanceInfos.ForEach((instanceInfo) => tasks.Add(Task.Factory.StartNew(() =>
            {
                SqlConnection instanceConnection;
                try
                {
                    instanceConnection = manager.GetOpenedInstanceConnection(instanceInfo);
                }
                catch (SqlException)
                {
                    repositoryAccess.UpdateInstance(instanceInfo);
                    return;
                }
                DataCollectorQueryExecutor queryExecutor = new DataCollectorQueryExecutor(instanceConnection);
                queryExecutor.ExecuteInstanceQueries(instanceInfo.Id);
            }
                )));
            Task.WaitAll(tasks.ToArray());
        }
    }
}