using Microsoft.EntityFrameworkCore;
using PWApplication.Enums;
using PWApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PWApplication.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly ApplicationContext _appContext;

        public TransactionService(ApplicationContext appContext)
        {
            _appContext = appContext;
        }

        public Transaction GetTransaction(int id)
        {
            return _appContext.Transactions
                .Include(x => x.Correspondent)
                .Where(x => x.Id == id)
                .FirstOrDefault();
        }

        public List<Transaction> GetTransactions(string userName, DateTime? startDate, DateTime? endDate, 
            string correspondentName, decimal? startAmount, decimal? endAmount, SortState sortOrder)
        {
            var transactions = _appContext.Transactions
                .Include(x => x.User)
                .Include(x => x.Correspondent)
                .Where(x => x.User.UserName == userName);

            if (startDate.HasValue)
            {
                transactions = transactions.Where(x => x.Date >= startDate);
            }
            if (endDate.HasValue)
            {
                transactions = transactions.Where(x => x.Date <= endDate);
            }
            if (!String.IsNullOrEmpty(correspondentName))
            {
                transactions = transactions.Where(x => x.Correspondent.UserName.Contains(correspondentName));
            }
            if (startAmount.HasValue)
            {
                transactions = transactions.Where(x => x.Amount >= startAmount);
            }
            if(endAmount.HasValue)
            {
                transactions = transactions.Where(x => x.Amount <= endAmount);
            }

            switch (sortOrder)
            {
                case SortState.DateAsc:
                    transactions = transactions.OrderBy(x => x.Date);
                    break;
                case SortState.CorrespondentNameAsc:
                    transactions = transactions.OrderBy(x => x.Correspondent.UserName);
                    break;
                case SortState.CorrespondentNameDesc:
                    transactions = transactions.OrderByDescending(x => x.Correspondent.UserName);
                    break;
                case SortState.AmountAsc:
                    transactions = transactions.OrderBy(x => x.Amount);
                    break;
                case SortState.AmountDesc:
                    transactions = transactions.OrderByDescending(x => x.Amount);
                    break;
                default:
                    transactions = transactions.OrderByDescending(x => x.Date);
                    break;
            }

            return transactions.ToList();
        }
    }
}
