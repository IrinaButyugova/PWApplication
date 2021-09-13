using PWApplication.BLL.Enums;
using PWApplication.Domain.Models;
using System;
using System.Collections.Generic;

namespace PWApplication.BLL.Services
{
    public interface ITransactionService
    {
        Transaction GetTransaction(int id);

        List<Transaction> GetTransactions(string userName, DateTime? startDate, DateTime? endDate, string correspondentName,
            decimal? startAmount, decimal? endAmount, SortState sortOrder);
    }
}
