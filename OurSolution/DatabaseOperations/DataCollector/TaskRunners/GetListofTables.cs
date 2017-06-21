using System.Collections.Generic;
using System.Data.SqlClient;
using Models;

namespace DataCollector
{
    public class GetListofTables : ITask
    {
        private readonly SqlConnection _connection;
        private readonly BriefDatabase _db;
        private readonly List<Table> _tableList;


        public List<Table> ListofTables
        {
            get { return _tableList; }
        }


        public GetListofTables(SqlConnection conn, BriefDatabase db)
        {
            _connection = conn;
            _db = db;
            _tableList = new List<Table>();
        }


        public void Run()
        {
            string query = string.Format("USE [" + _db.Name + "] " + CollectionQueries.ListOfTablesQuery);
            using (SqlCommand cmd = new SqlCommand(query, _connection))
            {
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    _tableList.Add(new Table()
                    {
                        Name = (string)rdr["TableName"],
                        DatabaseId = _db.Id,
                        ColumnCount = (int)rdr["ColumnCount"]
                    });
                }
            }
        }
    }
}