using Microsoft.EntityFrameworkCore;
using PWApplication.DAL.Data;
using PWApplication.Domain.Models;
using System.Collections.Generic;
using System.Linq;

namespace PWApplication.DAL.Repositories
{
    public class TransactionRepository : IRepository<Transaction>
    {
        private readonly ApplicationContext _appContext;

        public TransactionRepository(ApplicationContext appContext)
        {
            _appContext = appContext;
        }

        public IEnumerable<Transaction> GetAll()
        {
            return _appContext.Transactions
                .Include(x => x.User)
                .Include(x => x.Correspondent);
        }

        public Transaction Get(string id)
        {
            int transactionId;
            if (int.TryParse(id, out transactionId))
            {
                return Get(transactionId);
            }
            return null;
        }

        public Transaction Get(int id)
        {
            return _appContext.Transactions
                .Include(x => x.Correspondent)
                .Where(x => x.Id == id)
                .FirstOrDefault(); ;
        }

        public Transaction GetByUserName(string name)
        {
            return GetAllByUserName(name).FirstOrDefault();
        }

        public IEnumerable<Transaction> GetAllByUserName(string name)
        {
            return GetAll().Where(x => x.User.UserName == name);
        }

        public void Add(Transaction entity)
        {
            _appContext.Transactions.Add(entity);
        }

        public void AddRange(params Transaction[] entities)
        {
            _appContext.Transactions.AddRange(entities);
        }
    }
}
