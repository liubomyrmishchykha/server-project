using System.Collections.Generic;
using System.Data.SqlClient;

namespace Repository.Interfaces
{
    public interface IQuery
    {
        void BuildStoredProcedureName<T>(string operation);
        void AddParameters(List<SqlParameter> parameters);
        string GetProcedureName();
        SqlParameter[] GetParameters();
    }
}
