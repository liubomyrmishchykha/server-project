using Repository.QueryBuilder;
using Models;

namespace Repository.Interfaces
{
    public interface IQueryBuilder<T>
    {
        Query GetAll<T>();
        Query SelectById<T>(int id);
        Query Add<T>(T obj);
        Query Update<T>(T obj);
        Query Delete<T>(int id);
        Query Exists<T>(T obj);
        Query Count<T>();
        Query Search<T>(SearchDataOut search);
    }
}
