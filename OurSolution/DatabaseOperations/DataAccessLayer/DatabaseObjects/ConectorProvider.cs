using System;
using System.Data.SqlClient;
using System.Configuration;
using System.Reflection;
using log4net;

namespace DataAccessLayer.DatabaseObjects
{
    public class ConnectionProvider : IConector
    {
        /// <summary>
        /// Connection string to specific database
        /// </summary>
        private string ConnectionString { get; set; }
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public ConnectionProvider() { }
        public ConnectionProvider(string connectionStringName)
        {
            GetConnectionString(connectionStringName);
        }

        /// <summary>
        /// Gets Sql connection to database 
        /// </summary>
        /// <returns>SqlConneciton object</returns>
        public SqlConnection GetConnection()
        {
            SqlConnection connection = new SqlConnection();
            try
            {
                connection = new SqlConnection(ConnectionString);
                connection.Open();
            }
            catch(Exception ex)
            {
                Log.Error("Failed to get connection", ex);
                throw;
            }
            return connection;
        }

        /// <summary>
        /// Gets connection string from app.config file and sets that value into property ConnectionString
        /// </summary>
        /// <param name="connectionStringName">UserName of connection String in app.config</param>
        private void GetConnectionString(string connectionStringName)
        {
            try
            {
                //Getting connection string from app.config
                ConnectionString = ConfigurationManager.ConnectionStrings[connectionStringName].ConnectionString;
            }
            catch(Exception ex)
            {
                Log.Error("Unable to get connections string", ex);
                throw;
            }            
        }
    }
}
