namespace PWBlazorApplication.Store.TransactionUseCase
{
    public record FetchUsersResultAction
    {
        public IEnumerable<string> Users { get; init; }
		public string UserName { get; init; }

		public FetchUsersResultAction(IEnumerable<string> users, string userName)
        {
            Users = users;
            UserName = userName;
        }
    }
}
