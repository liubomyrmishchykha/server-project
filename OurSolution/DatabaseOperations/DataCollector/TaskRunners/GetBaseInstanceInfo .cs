using System;
using System.Data.SqlClient;
using Models;

namespace DataCollector
{
    public class GetBaseInstanceInfo : ITask
    {
        private Instance _instance;
        private readonly SqlConnection _connection;


        public Instance Instance
        {
            get { return _instance; }
        }


        public GetBaseInstanceInfo(SqlConnection conn)
        {
            _connection = conn;
        }


        public void Run()
        {
            using (SqlCommand cmd = new SqlCommand(CollectionQueries.BaseInstanceInfoQuery, _connection))
            {
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    _instance = new Instance()
                    {
                        HostName = (string)rdr["HostName"],
                        InstanceName = (string)rdr["InstanceName"],
                        Version = (string)rdr["Version"],
                        RAM = (int)Math.Round((long)rdr["RAM"] / (double)1048576),
                        CPUCount = (int)rdr["CPUCount"],
                        Status = 1
                    };
                }
                rdr.Close();
            }
        }
    }
}