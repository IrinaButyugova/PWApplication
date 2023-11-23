namespace PWBlazorApplication.Store.TransactionUseCase
{
	public record FetchTransactionDataAction
	{
		public int Id { get; init; }

		public FetchTransactionDataAction(int id)
		{
			Id = id;
		}
	}
}
