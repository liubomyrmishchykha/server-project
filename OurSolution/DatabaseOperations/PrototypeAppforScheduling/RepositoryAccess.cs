using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using PrototypeAppforScheduling.Models;
using Repository;
using Repository.Interfaces;
using Repository.ConnectionManager;

namespace PrototypeAppforScheduling
{
    class RepositoryAccess
    {

        private IRepository<Instance> repoInstance;
        private IRepository<InstanceInfo> repoInstanceInfo;
        private IRepository<Database> repoDatabase;
        private IRepository<Table> repoTable;

        public RepositoryAccess()
        {
            repoInstance = new Repository<Instance>();
            repoInstanceInfo = new Repository<InstanceInfo>();
            repoDatabase=new Repository<Database>();
            repoTable=new Repository<Table>();
        }

        public List<InstanceInfo> GetAllInstanceInfo()
        {
            List<InstanceInfo> instList = repoInstanceInfo.GetAll();
            return instList;
        }

        public void UpdateInstance(Instance instance)
        {
            instance.Modified=DateTime.Now;
            repoInstance.Update(instance);
        }

        public void UpdateInstance(InstanceInfo instanceInfo)
        {
            Instance instance = new Instance()
            {
                Id = instanceInfo.Id,
                HostName = instanceInfo.HostName,
                InstanceName = instanceInfo.InstanceName,
                Modified = DateTime.Now,
                Status = 0,
                UserId = instanceInfo.UserId
            };
            repoInstance.Update(instance);
        }

        public int SaveDatabase(Database db)
        {
            int id = 0;
            id = repoDatabase.Exists(db);
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

        public void SaveTables(int dbId, List<string> tableList)
        {
            if (tableList != null & tableList.Count > 0)
            {
                foreach (var tableName in tableList)
                {
                    Table table = new Table()
                    {
                        DatabaseId = dbId,
                        Name = tableName
                    };
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
}
