using PWApplication.Errors;
using PWApplication.Models;
using PWApplication.Repositories;
using PWApplication.Result;
using System;

namespace PWApplication.Services
{
    public class TransferService : ITransferService
    {
        private readonly IRepositoryService _repositoryService;

        public TransferService(IRepositoryService repositoryService)
        {
            _repositoryService = repositoryService;
        }

        public void IncreaseBalance(string id, decimal amount)
        {
            var user = _repositoryService.Users.Get(id);
            user.Balance += amount;
            _repositoryService.Save();
        }

        public PWResult CreateTransaction(string userName, string recipientName, decimal amount)
        {
            var result = new PWResult();

            var user = _repositoryService.Users.GetByUserName(userName);
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
            var recipient = _repositoryService.Users.GetByUserName(recipientName);
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
            _repositoryService.Transactions.AddRange(currentUserTransaction, recipientTransaction);

            _repositoryService.Save();

            result.Succeeded = true;
            return result;
        }
    }
}
