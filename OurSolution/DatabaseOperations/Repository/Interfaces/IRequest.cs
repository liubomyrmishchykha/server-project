using System.Data.SqlClient;

namespace Repository.Interfaces
{
    public interface IRequest
    {
        SqlDataReader GetAll<T>();
    }
}
