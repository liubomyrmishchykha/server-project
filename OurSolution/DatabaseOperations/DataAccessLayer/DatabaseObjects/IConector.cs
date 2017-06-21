using System.Data.SqlClient;

namespace DataAccessLayer.DatabaseObjects
{
    public interface IConector
    {
        SqlConnection GetConnection();
    }
}
