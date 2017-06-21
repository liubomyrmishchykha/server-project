using System;
using PrototypeAppforScheduling.Models;
using System.Collections.Generic;
using System.Data.SqlClient;
using Models;

namespace PrototypeAppforScheduling
{
    public class GetListofDatabases : ITask
    {
        private readonly SqlConnection _connection;
        private readonly List<Database> _dbList;

        public List<Database> DbList { get { return _dbList; } }

        public GetListofDatabases(SqlConnection conn)
        {
            _connection = conn;
            _dbList = new List<Database>();
        }

        public void Run()
        {
            using (SqlCommand cmd = new SqlCommand(CollectionQueries.ListOfDatabasesQuery, _connection))
            {
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    _dbList.Add(new Database { Name = (string)rdr["DBName"], CreateTime = (DateTime)rdr["CreationDate"] });
                }
                rdr.Close();
            }
        }
    }
}