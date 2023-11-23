namespace PWBlazorApplication.Store.TransactionUseCase
{
	public record FetchTransactionResultAction
	{
		public string RecipientName { get; init; }
		public decimal Amount { get; init; }
		public FetchTransactionResultAction(string recipientName, decimal amount)
		{
			RecipientName = recipientName;
			Amount = amount;
		}
	}
}
