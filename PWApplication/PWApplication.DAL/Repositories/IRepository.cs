using System.Collections.Generic;

namespace PWApplication.DAL.Repositories
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();

        T Get(string id);

        T Get(int id);

        T GetByUserName(string name);

        IEnumerable<T> GetAllByUserName(string name);

        void Add(T entity);

        void AddRange(params T[] entities);
    }
}
