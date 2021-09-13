using PWApplication.Domain.Models;

namespace PWApplication.DAL.Repositories
{
    public interface IRepositoryService
    {
        void Save();

        IRepository<User> Users { get; }

        IRepository<Transaction> Transactions { get; }
    }
}
