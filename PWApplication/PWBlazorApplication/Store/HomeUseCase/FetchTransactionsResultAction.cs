using PWApplication.BLL.Enums;
using PWApplication.Domain.Models;
using PWComponents.Models;

namespace PWBlazorApplication.Store.HomeUseCase
{
	public record FetchTransactionsResultAction
	{
		public IEnumerable<Transaction> Transactions { get; init; }
		public FilterModel FilterModel { get; init; }

		public SortState CurrentSort { get; init; }
		public int PageNumber { get; init; }
		public int PagesCount { get; init; }

		public FetchTransactionsResultAction(IEnumerable<Transaction> transactions, FilterModel filterModel, SortState currentSort, int pageNumber, int pagesCount)
		{
			Transactions = transactions;
			FilterModel = filterModel;
			CurrentSort = currentSort;
			PageNumber = pageNumber;
			PagesCount = pagesCount;
		}
	}
}
