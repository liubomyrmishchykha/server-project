using System.Collections.Generic;
using System.Data.SqlClient;
using LoggerService;
using Models;


namespace InstanceSearcher
{
    class Connector
    {

        internal Instance GetInstanceData(FullInstanceName name, List<User> users)
        {
            Instance instance = new Instance
            {
                HostName = name.HostName,
                InstanceName = name.InstanceName,
                Status = 0,
                UserId = null
            };
            //Connecting without credentials in case SQL server instance has a windows authentication mode set 
            //and using same credentials 
            //If connection is successful, we get information about instance and create instance object
            if (TryConnect(name))
            {
                instance.Status = 1;
                instance.InstanceName = "SQLEXPRESS";
                return instance;
            }
            //Trying the User credentials from the Users Table 
            if (users == null || users.Count == 0) return instance;
            foreach (User user in users)
            {
                if (!TryConnect(name, user)) continue;
                instance.Status = 1;
                instance.UserId = user.Id;
                return instance;
            }
            Notify(name, false);
            return instance;
        }

        //Configuring connection using Integrated Security
        private string ConfigurateConnection(FullInstanceName name)
        {
            string instName = name.InstanceName;
            if (string.IsNullOrEmpty(instName))
            {
                instName = "SQLEXPRESS";
            }
            string dataSourse = string.Format("{0}\\{1}", name.HostName, instName);
            SqlConnectionStringBuilder stringBuilder = new SqlConnectionStringBuilder
            {
                DataSource = dataSourse,
                IntegratedSecurity = true,
                ConnectTimeout = 5
            };
            return stringBuilder.ConnectionString;
        }


        //Configuring connection using SQL Authentication
        private string ConfigurateConnection(FullInstanceName name, User user)
        {
            string dataSourse;
            if (name.InstanceName == null)
            {
                dataSourse = name.HostName;
            }
            else
            {
                dataSourse = string.Format("{0}\\{1}", name.HostName, name.InstanceName);
            }
            SqlConnectionStringBuilder stringBuilder = new SqlConnectionStringBuilder
            {
                DataSource = dataSourse,
                IntegratedSecurity = false,
                UserID = user.Name,
                Password = user.Password,
                ConnectTimeout = 5
            };
            return stringBuilder.ConnectionString;
        }


        private bool TryConnect(FullInstanceName name, User user)
        {
            SqlConnection connection = new SqlConnection(ConfigurateConnection(name, user));
            try
            {
                connection.Open();
                Notify(name, true);
                connection.Close();
                return true;
            }
            catch (SqlException)
            {
                return false;
            }
        }


        private bool TryConnect(FullInstanceName name)
        {
            SqlConnection connection = new SqlConnection(ConfigurateConnection(name));
            try
            {
                connection.Open();
                Notify(name, true);
                connection.Close();
                return true;
            }
            catch (SqlException)
            {
                return false;
            }
        }


        private void Notify(FullInstanceName name, bool status)
        {
            string dataSourse, msg;
            if (name.InstanceName == null)
            {
                dataSourse = name.HostName;
            }
            else
            {
                dataSourse = string.Format("{0}\\{1}", name.HostName, name.InstanceName);
            }
            msg = status ? "Has been successfully connected to instance " : "Couldn't connect to instance ";
            msg = string.Concat(msg, dataSourse);
            Logger.WriteLog(LogLevel.Debug, msg);
        }
    }
}
