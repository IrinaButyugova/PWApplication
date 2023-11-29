using PWApplication.BLL.Enums;
using PWApplication.Domain.Models;
using PWComponents.Models;

namespace PWBlazorApplication.Store.HomeUseCase
{
    public record FetchHomeResultAction
    {
        public string Name { get; init; }

        public decimal Balance { get; init; }
        public IEnumerable<Transaction> Transactions { get; init; }
        public FilterModel FilterModel { get; init; }

        public SortState CurrentSort { get; init; }
		public IEnumerable<string> Users { get; init; } = new List<string>();
		public int PagesCount { get; init; }

		public FetchHomeResultAction(string name, decimal balance, IEnumerable<Transaction> transactions, FilterModel filterModel, SortState currentSort, IEnumerable<string> users,
		     int pagesCount)
        {
            Name = name;
            Balance = balance;
            Transactions = transactions;
            FilterModel = filterModel;
            CurrentSort = currentSort;
            Users = users;
            PagesCount = pagesCount;
        }
    }
}
