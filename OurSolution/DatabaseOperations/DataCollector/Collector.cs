using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using LoggerService;
using Repository.ConnectionManager;

namespace DataCollector
{
    public static class Collector
    {
        private static RepositoryAccess repositoryAccess;
        private static ConnectionManager manager;


        static Collector()
        {
            repositoryAccess = new RepositoryAccess();
            manager = new ConnectionManager();
        }


        public static void CollectData()
        {
            //Getting all the instances from database
            List<InstanceInfo> listInstanceInfos = repositoryAccess.GetAllInstanceInfo();
            if (listInstanceInfos == null || listInstanceInfos.Count == 0)
            {
                Logger.WriteLog(LogLevel.Error, "There are no saved instances in db");
                return;
            }
            //Starting new task for each instance
            List<Task> tasks = new List<Task>();
            listInstanceInfos.ForEach((instanceInfo) => tasks.Add(Task.Factory.StartNew(() =>
            {
                ConnectToInstance(instanceInfo);
            }
            )));
            try
            {
                Task.WaitAll(tasks.ToArray());
            }
            catch (AggregateException ex)
            {
                throw ex.Flatten();
            }
        }


        private static void ConnectToInstance(InstanceInfo instanceInfo)
        {
            SqlConnection instanceConnection;
            //Trying to open connection. If successful, collecting data from instance
            try
            {
                instanceConnection = manager.GetOpenedInstanceConnection(instanceInfo);
            }
            catch (SqlException ex)
            {
                repositoryAccess.UpdateInstance(instanceInfo);
                Notify(instanceInfo, ex);
                return;
            }
            AllQueriesExecutor queriesExecutor = new AllQueriesExecutor(instanceConnection);
            queriesExecutor.ExecuteAllQueries(instanceInfo.Id, instanceInfo.UserId);
        }


        private static void Notify(InstanceInfo instance, Exception ex)
        {
            string msg = string.Format("Failure when trying to connect to {0}",
                        string.IsNullOrEmpty(instance.InstanceName)
                        ? instance.HostName
                        : String.Format("{0}\\{1}", instance.HostName, instance.InstanceName));
            Logger.WriteLog(LogLevel.Error, msg, ex);
        }
    }
}