namespace PWBlazorApplication.Store.TransactionUseCase
{
	public record CreateTransactionAction
	{
		public string UserName { get; init; }
		public string RecipientName { get; init; }
		public decimal Amount { get; init; }

		public CreateTransactionAction(string userName, string recipientName, decimal amount)
		{
			UserName = userName;
			RecipientName = recipientName;
			Amount = amount;
		}
	}
}
