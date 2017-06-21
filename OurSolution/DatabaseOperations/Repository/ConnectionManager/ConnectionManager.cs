using Models.Constants;
using System;
using System.Data.SqlClient;

namespace Repository.ConnectionManager
{
    public class ConnectionManager
    {
        private string _repositoryConnectionString = ConnectionString.ConnectionStr;

        public ConnectionManager()
        {
        }

        /// <summary>
        /// Get opened connection for repository
        /// </summary>
        /// <returns>Initsialized connection</returns>
        public SqlConnection GetOpenedRepositoryConnection()
        {
            SqlConnection connection = new SqlConnection(_repositoryConnectionString);
            connection.Open();
            return connection;
        }

        /// <summary>
        /// Get opened connection for instance
        /// </summary>
        /// <returns>Initsialized connection</returns>
        public SqlConnection GetOpenedInstanceConnection(InstanceInfo instanceInfo)
        {
            if (String.IsNullOrEmpty(instanceInfo.HostName))
            {
                throw new ArgumentNullException("instanceInfo.HostName");
            }

            SqlConnectionStringBuilder builder = InitSqlConnectionBuilder(instanceInfo);
            String connectionString = builder.ToString();
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            return connection;
        }
            

        /// <summary>
        /// Init SqlConnectionStringBuilder
        /// </summary>
        /// <param name="instanceInfo"></param>
        /// <returns>SqlConnectionStringBuilder</returns>
        private SqlConnectionStringBuilder InitSqlConnectionBuilder(InstanceInfo instanceInfo)
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
            builder.MultipleActiveResultSets = true;

            //set builder.DataSource
            if (String.IsNullOrEmpty(instanceInfo.InstanceName))
            {
                builder.DataSource = instanceInfo.HostName;
            }
            else
            {
                builder.DataSource = instanceInfo.HostName + "\\" + instanceInfo.InstanceName;
            }
            
            //look if user IntegratedSecurity or SQL Server login
            if(String.IsNullOrEmpty(instanceInfo.UserName) && String.IsNullOrEmpty(instanceInfo.Password))
            {
                builder.IntegratedSecurity = true;
            }
            else
            {
                builder.UserID = instanceInfo.UserName;
                builder.Password = instanceInfo.Password;
            }
            return builder;
        }

        }

}
