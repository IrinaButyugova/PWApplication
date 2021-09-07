using Microsoft.EntityFrameworkCore;
using PWApplication.Models;
using System;
using System.Linq;
using System.Security.Principal;
using SecurityClaims = System.Security.Claims;

namespace PWApplication.Services
{
    public class ExchangeService : IExchange
    {
        private readonly ApplicationContext _appContext;

        public ExchangeService(ApplicationContext appContext)
        {
            _appContext = appContext;
        }

        public void IncreaseBalance(string id, decimal amount)
        {
            var user = _appContext.Users.Where(x => x.Id == id).FirstOrDefault();
            user.Balance += amount;
            _appContext.SaveChanges();
        }

        public void CreateTransaction(string userName, string recipientName, decimal amount)
        {
            try
            {
                var user = _appContext.Users
                    .Where(x => x.UserName == userName)
                    .FirstOrDefault();
                if (user.Balance < amount)
                {
                    throw new Exception("Transaction amount is less than the current user balance");
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

            }
            catch (Exception e)
            {
                throw new Exception($"Transaction exception: {e.Message}");
            }
        }
    }
}
