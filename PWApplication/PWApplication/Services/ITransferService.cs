namespace PWApplication.Services
{
    public interface ITransferService
    {
        void IncreaseBalance(string id, decimal amount);

        void CreateTransaction(string userName, string recipientName, decimal amount);
    }
}
