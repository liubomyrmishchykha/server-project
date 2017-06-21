using System;
using Models;
using System.Collections.Generic;

namespace Repository.Interfaces
{
    public interface IRepository<T>
    {
        List<T> GetAll();
        T SelectByID(int id);
        void Add(T obj);
        void Update(T obj);
        void Delete(int id);
        int Exists(T obj);
        int Count();
        Tuple<int, List<T>> Search(SearchDataOut search);
    }
}
