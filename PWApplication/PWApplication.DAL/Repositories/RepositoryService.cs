using PWApplication.DAL.Data;
using PWApplication.Domain.Models;

namespace PWApplication.DAL.Repositories
{
    public class RepositoryService : IRepositoryService
    {
        private readonly ApplicationContext _appContext;
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<Transaction> _transactionRepository;

        public RepositoryService(ApplicationContext appContext, IRepository<User> userRepository, 
            IRepository<Transaction> transactionRepository)
        {
            _appContext = appContext;
            _userRepository = userRepository;
            _transactionRepository = transactionRepository;
        }

        public void Save()
        {
            _appContext.SaveChanges();
        }

        public IRepository<User> Users 
        {
            get
            {
                return _userRepository;
            }
        }

        public IRepository<Transaction> Transactions
        {
            get
            {
                return _transactionRepository;
            }
        }
    }
}
