using PWApplication.BLL.Enums;

namespace PWBlazorApplication.Store.HomeUseCase
{
	public record FetchTransactionsAction
	{
		public string UserName { get; init; }
		public DateTime? StartDate { get; init; }
		public DateTime? EndDate { get; init; }
		public string CorrespondentName { get; init; }
		public decimal? StartAmount { get; init; }
		public decimal? EndAmount { get; init; }
		public SortState SortState { get; set; }
	}
}
