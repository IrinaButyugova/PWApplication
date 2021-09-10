using PWApplication.Models;

namespace PWApplication.Services
{
    public interface IAccountService
    {
        User GetUser(string userName);
    }
}
