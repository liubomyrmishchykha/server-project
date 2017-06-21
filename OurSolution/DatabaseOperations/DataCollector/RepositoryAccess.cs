using System;
using System.Collections.Generic;
using Models;
using Repository;
using Repository.ConnectionManager;
using Repository.Interfaces;


namespace DataCollector
{
    class RepositoryAccess
    {

        private IRepository<Instance> repoInstance;
        private IRepository<InstanceInfo> repoInstanceInfo;
        private IRepository<BriefDatabase> repoDatabase;
        private IRepository<Table> repoTable;
        private IRepository<BriefInstance> repoBriefInstance;


        public RepositoryAccess()
        {
            repoInstance = new Repository<Instance>();
            repoInstanceInfo = new Repository<InstanceInfo>();
            repoDatabase = new Repository<BriefDatabase>();
            repoTable = new Repository<Table>();
            repoBriefInstance = new Repository<BriefInstance>();
        }


        public List<InstanceInfo> GetAllInstanceInfo()
        {
            List<InstanceInfo> instList = repoInstanceInfo.GetAll();
            return instList;
        }


        public void UpdateInstance(Instance instance)
        {
            if (instance == null) return;
            instance.Modified = DateTime.Now;
            repoInstance.Update(instance);
        }


        public void UpdateInstance(InstanceInfo instanceInfo)
        {
            BriefInstance briefInstance = new BriefInstance()
            {
                Id = instanceInfo.Id,
                Modified = DateTime.Now,
                Status = 0,
                UserID = instanceInfo.UserId
            };
            repoBriefInstance.Update(briefInstance);
        }


        public int SaveDatabase(BriefDatabase db)
        {
            int id = repoDatabase.Exists(db);
            if (id == 0)
            {
                repoDatabase.Add(db);
                id = repoDatabase.Exists(db);
            }
            else
            {
                repoDatabase.Update(db);
            }
            return id;
        }


        public void SaveTables(List<Table> tableList)
        {
            if (tableList.Count == 0) return;
            foreach (Table table in tableList)
            {
                if (repoTable.Exists(table) > 0)
                {
                    repoTable.Update(table);
                }
                else
                {
                    repoTable.Add(table);
                }
            }
        }
    }
}
