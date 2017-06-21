using System.Data;
using System.Data.SqlClient;
using Repository.QueryBuilder;

namespace Repository.QueryExecutor
{
    public class QueryExecutor
    {
        public QueryResult Execute(Query query, SqlConnection connection)
        {
            string name = query.GetProcedureName();
            using (SqlCommand cmd = new SqlCommand(name, connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddRange(query.GetParameters());
                SqlDataReader reader = cmd.ExecuteReader();
                reader.Read();
                var result = new QueryResult(reader);
                return result;
            }
        }
    }
}
