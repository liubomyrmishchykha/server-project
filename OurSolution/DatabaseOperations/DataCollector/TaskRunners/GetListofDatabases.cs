using System;
using System.Collections.Generic;
using System.Data.SqlClient;


namespace DataCollector
{
    public class GetListofDatabases : ITask
    {
        private readonly SqlConnection _connection;
        private readonly List<BriefDatabase> _dbList;


        public List<BriefDatabase> DbList { get { return _dbList; } }


        public GetListofDatabases(SqlConnection conn)
        {
            _connection = conn;
            _dbList = new List<BriefDatabase>();
        }


        public void Run()
        {
            using (SqlCommand cmd = new SqlCommand(CollectionQueries.ListOfDatabasesQuery, _connection))
            {
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    _dbList.Add(new BriefDatabase
                    {
                        Name = (string)rdr["DBName"],
                        CreateTime = (DateTime)rdr["CreationDate"],
                        TotalSize = (int)rdr["TotalSize"],
                        DbState = (byte)rdr["DbState"]
                    });
                }
                rdr.Close();
            }
        }
    }
}