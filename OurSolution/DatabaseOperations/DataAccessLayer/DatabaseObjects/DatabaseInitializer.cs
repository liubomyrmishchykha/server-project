using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Reflection;
using System.Text.RegularExpressions;
using log4net;

namespace DataAccessLayer.DatabaseObjects
{
    public class DatabaseInitializer:IDatabaseInitializer
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        public void Initialize()
        {
            try
            {
                string sqlCommandText = Properties.Resources.CreateDb;

                IEnumerable<string> commandStrings = Regex.Split(sqlCommandText, @"^\s*GO\s*$", RegexOptions.Multiline | RegexOptions.IgnoreCase);

                using (SqlConnection connection = new SqlConnection())
                {
                    connection.ConnectionString = ConfigurationManager.ConnectionStrings["ConnectionStringForCreate"].ConnectionString;
                    connection.Open();
                    foreach (string commandString in commandStrings)
                    {
                        if (commandString.Trim() != "")
                        {
                            using (SqlCommand cmd = new SqlCommand(commandString, connection))
                            {
                                cmd.ExecuteNonQuery();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error("Database initialization failed", ex);
            }
        }
    }
}