﻿using PWApplication.BLL.Enums;
using PWApplication.Domain.Models;
using PWBlazorApplication.Models;

namespace PWBlazorApplication.Store.HomeUseCase
{
    public record FetchHomeResultAction
    {
        public string Name { get; init; }

        public decimal Balance { get; init; }
        public IEnumerable<Transaction> Transactions { get; init; }
        public FilterModel FilterModel { get; init; }

        public SortState CurrentSort { get; init; }

        public FetchHomeResultAction(string name, decimal balance, IEnumerable<Transaction> transactions, FilterModel filterModel, SortState currentSort)
        {
            Name = name;
            Balance = balance;
            Transactions = transactions;
            FilterModel = filterModel;
            CurrentSort = currentSort;
        }
    }
}