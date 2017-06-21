using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Models;
using PrototypeAppforScheduling.Models;

namespace PrototypeAppforScheduling
{
    public class GetListofTables : ITask
    {
        private readonly SqlConnection _connection;
        private readonly Database _db;
        private readonly List<string> _tableList;

        public List<string> ListofTables
        {
            get { return _tableList; }
        }

        public GetListofTables(SqlConnection conn, Database db)
        {
            _connection = conn;
            _db = db;
            _tableList = new List<string>();
        }

        public void Run()
        {
            string query = string.Format("USE [" + _db.Name + "] " + CollectionQueries.ListOfTablesQuery);
            using (SqlCommand cmd = new SqlCommand(query, _connection))
            {
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    string tableName = (string)rdr["TableName"];
                    if (!string.IsNullOrEmpty(tableName))
                    {
                        _tableList.Add(tableName);
                    }
                }
            }
        }
    }
}