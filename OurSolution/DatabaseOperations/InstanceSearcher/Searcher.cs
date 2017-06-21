using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Sql;
using System.Threading.Tasks;
using LoggerService;
using Models;
using Repository;
using Repository.Interfaces;


namespace InstanceSearcher
{
    public static class Searcher
    {

        public static void InstanceSearch()
        {
            //Getting list of instances in the network
            List<FullInstanceName> fullinstanceNames = GetAvailableInstances();
            if (fullinstanceNames.Count == 0)
            {
                Logger.WriteLog(LogLevel.Debug, "No instances in local network were found");
                return;
            }
            Repository<User> repoUser = new Repository<User>();
            List<User> users = repoUser.GetAll();
            //Iterating through the instances to fit credentials
            List<Task> tasks = new List<Task>();
            fullinstanceNames.ForEach((name) => tasks.Add(Task.Factory.StartNew(() =>
            {
                Connector connector = new Connector();
                Instance instance = connector.GetInstanceData(name, users);
                SaveInstance(instance);
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


        private static List<FullInstanceName> GetAvailableInstances()
        {
            List<FullInstanceName> names = new List<FullInstanceName>();
            DataTable dt = SqlDataSourceEnumerator.Instance.GetDataSources();
            int count = dt.Rows.Count;
            if (count == 0) return names;
            string msg = string.Format("Was found {0} instances", count);
            Logger.WriteLog(LogLevel.Info, msg);
            for (int i = 0; i < count; i++)
            {
                string host = (string)dt.Rows[i]["ServerName"];
                string instance = (string)dt.Rows[i]["InstanceName"];
                if (string.IsNullOrEmpty(instance))
                    instance = "";
                names.Add(new FullInstanceName(host, instance));
            }
            return names;
        }


        private static void SaveInstance(Instance instance)
        {
            IRepository<Instance> repoInstance = new Repository<Instance>();
            int instid = repoInstance.Exists(instance);
            if (instid > 0)
            {
                IRepository<BriefInstance> repoBriefInstance = new Repository<BriefInstance>();
                BriefInstance briefInstance = new BriefInstance()
                {
                    Id = instid,
                    Modified = DateTime.Now,
                    Status = 1,
                    UserID = instance.UserId,
                };
                repoBriefInstance.Update(briefInstance);
            }
            else
            {
                instance.Added = DateTime.Now;
                repoInstance.Add(instance);
            }
        }
    }
}
