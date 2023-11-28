namespace PWBlazorApplication.Store.HomeUseCase
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
