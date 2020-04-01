using System.Collections.Generic;
namespace DAL.Interfaces
{
    public interface IRepository<T> where T:class
    {
        T GetById(int id);
        IEnumerable<T> GetAll();
        T Create(T t);
        T Delete(int id);
        void Update (T t);
        void Save();

    }
}