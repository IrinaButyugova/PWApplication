namespace PWApplication.Services
{
    public interface IExchange
    {
        void IncreaseBalance(string id, decimal amount);

        void CreateTransaction(string userName, string recipientName, decimal amount);
    }
}
