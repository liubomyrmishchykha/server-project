using System;
using System.Reflection;
using DataAccessLayer.DatabaseObjects;
using DataAccessLayer.DataModels;
using DataAccessLayer.DataModels.Interfaces;
using log4net;
using log4net.Config;


namespace DataAccessLayer
{
    /// <summary>
    /// Class which provides data between application and database
    /// </summary>
    public class DbDataProvider
    {
        public IUserOperations User { get; set; }
        public IInstanceOperations Instance { get; set; }
        private  IDatabaseInitializer _initializer;
        private static DbDataProvider _provider;

        /// <summary>
        /// Using singeton patern to avoid database initialization in every query to database
        /// </summary>
        /// <returns>If DbDataProvider is not initialized we return new DbDataProvider</returns>
        public static DbDataProvider Get()
        {
                return _provider ?? (_provider = new DbDataProvider(new User(), new Instance(), new DatabaseInitializer()));
        }

        public DbDataProvider(){}

        public  DbDataProvider(IUserOperations user, IInstanceOperations instance, IDatabaseInitializer initializer)
        {
            Logger.Setup();
            _initializer = initializer;
            _initializer.Initialize();
            this.User = user;
            this.Instance = instance;
        }
    }
}
