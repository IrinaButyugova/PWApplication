using Fluxor;
using PWApplication.BLL.Enums;
using PWApplication.Domain.Models;
using PWComponents.Models;

namespace PWBlazorApplication.Store.HomeUseCase
{
	[FeatureState]
	public record HomeState
	{
		public string Name { get; init; }

		public decimal Balance { get; init; }
		public IEnumerable<Transaction> Transactions { get; init; }
		public FilterModel FilterModel { get; init; }

		public SortState CurrentSort { get; init; }

		private HomeState()
		{

		}

		public HomeState(string name, decimal balance, IEnumerable<Transaction> transactions, FilterModel filterModel, SortState currentSort)
		{
			Name = name ?? "";
			Balance = balance;
			Transactions = transactions ?? new List<Transaction>();
			FilterModel = filterModel ?? new FilterModel();	
			CurrentSort = currentSort;
		}
	}
}
