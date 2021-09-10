using PWApplication.Models;

namespace PWApplication.Repositories
{
    public interface IRepositoryService
    {
        void Save();

        IRepository<User> Users { get; }

        IRepository<Transaction> Transactions { get; }
    }
}
