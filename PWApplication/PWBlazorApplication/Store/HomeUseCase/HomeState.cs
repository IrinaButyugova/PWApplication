using Fluxor;
using PWApplication.BLL.Enums;
using PWApplication.BLL.Errors;
using PWApplication.Domain.Models;
using PWComponents.Models;

namespace PWBlazorApplication.Store.HomeUseCase
{
	[FeatureState]
	public record HomeState
	{
		public string Name { get; init; }

		public decimal Balance { get; init; }
		public IEnumerable<Transaction> Transactions { get; init; } = new List<Transaction>();
		public FilterModel FilterModel { get; init; } = new FilterModel();

		public SortState CurrentSort { get; init; }

		public IEnumerable<string> Users { get; init; } = new List<string>();
		public CreateTransactionModel CreateTransactionModel { get; init; } = new CreateTransactionModel();
		public bool CreationSucceeded { get; init; }
		public IEnumerable<Error> CreationErrors { get; init; } = new List<Error>();

		private HomeState()
		{

		}

		public HomeState(string name, decimal balance, IEnumerable<Transaction> transactions, FilterModel filterModel, SortState currentSort, IEnumerable<string> users)
		{
			Name = name ?? "";
			Balance = balance;
			Transactions = transactions ?? new List<Transaction>();
			FilterModel = filterModel ?? new FilterModel();	
			CurrentSort = currentSort;
			Users = users;
		}
	}
}
