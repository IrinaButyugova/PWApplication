using PWApplication.Errors;
using PWApplication.Models;
using PWApplication.Result;
using System;
using System.Linq;

namespace PWApplication.Services
{
    public class TransferService : ITransferService
    {
        private readonly ApplicationContext _appContext;

        public TransferService(ApplicationContext appContext)
        {
            _appContext = appContext;
        }

        public void IncreaseBalance(string id, decimal amount)
        {
            var user = _appContext.Users.Where(x => x.Id == id).FirstOrDefault();
            user.Balance += amount;
            _appContext.SaveChanges();
        }

        public PWResult CreateTransaction(string userName, string recipientName, decimal amount)
        {
            var result = new PWResult();

            var user = _appContext.Users
                .Where(x => x.UserName == userName)
                .FirstOrDefault();
            if (user.Balance < amount)
            {
                result.Errors.Add(new Error
                {
                    Code = ErrorCodes.BALANCE_LESS_THAN_AMOUNT,
                    Description = "Current user balance is less than transaction amount"
                });
                return result;
            }

            user.Balance = user.Balance - amount;
            var recipient = _appContext.Users
                .Where(x => x.UserName == recipientName)
                .FirstOrDefault();
            recipient.Balance += amount;

            var currentUserTransaction = new Transaction()
            {
                Date = DateTime.UtcNow,
                Type = TransactionType.Debit,
                Amount = amount,
                UserBalance = user.Balance,
                User = user,
                Correspondent = recipient

            };
            var recipientTransaction = new Transaction()
            {
                Date = DateTime.UtcNow,
                Type = TransactionType.Credit,
                Amount = amount,
                UserBalance = recipient.Balance,
                User = recipient,
                Correspondent = user
            };
            _appContext.Transactions.AddRange(currentUserTransaction, recipientTransaction);

            _appContext.SaveChanges();

            result.Succeeded = true;
            return result;
        }
    }
}
