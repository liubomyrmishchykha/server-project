using System.Collections.Generic;

namespace DataAccessLayer.DataModels.Interfaces
{
    public interface IOperations<T>
    {
        void Add(T obj);
        void Update(T obj);
        void Delete(int id);
        List<T> GetAll();
        T GetById(int id);
    }
}
