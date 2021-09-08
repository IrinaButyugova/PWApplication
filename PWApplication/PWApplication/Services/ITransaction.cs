using PWApplication.Enums;
using PWApplication.Models;
using System;
using System.Collections.Generic;

namespace PWApplication.Services
{
    public interface ITransaction
    {
        Transaction GetTransaction(int id);

        List<Transaction> GetTransactions(string userName, DateTime? startDate, DateTime? endDate, string correspondentName,
            decimal? startAmount, decimal? endAmount, SortState sortOrder);
    }
}
