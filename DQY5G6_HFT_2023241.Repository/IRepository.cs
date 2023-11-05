using System;
using System.Linq;

namespace DQY5G6_HFT_2023241.Repository
{
    public interface IRepository<T> where T : class
    {
        void Create(T item);
        T Read(int id);
        void Update(T item);
        void Delete(int id);
        IQueryable<T> ReadAll();
    }
}
