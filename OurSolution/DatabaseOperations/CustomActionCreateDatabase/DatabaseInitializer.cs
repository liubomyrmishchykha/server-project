using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

namespace MyCustomAction
{
    public class DatabaseInitializer
    {
        public static void Initialize(string connectionString)
        {
           IEnumerable<string> commandStrings = Regex.Split(Resources.CreateDb, @"^\s*GO\s*$", RegexOptions.Multiline | RegexOptions.IgnoreCase);
            
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
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
    }
}
