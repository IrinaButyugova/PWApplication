using PWApplication.BLL.Result;

namespace PWApplication.BLL.Services
{
    public interface ITransferService
    {
        void IncreaseBalance(string id, decimal amount);

        PWResult CreateTransaction(string userName, string recipientName, decimal amount);
    }
}
