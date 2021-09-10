using PWApplication.Result;

namespace PWApplication.Services
{
    public interface ITransferService
    {
        void IncreaseBalance(string id, decimal amount);

        PWResult CreateTransaction(string userName, string recipientName, decimal amount);
    }
}
