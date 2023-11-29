namespace PWBlazorApplication.Store.HomeUseCase
{
    public record FetchHomeDataAction
    {
		public int PageSize { get; init; }

		public FetchHomeDataAction(int pageSize)
		{
			PageSize = pageSize;
		}
	}
}
