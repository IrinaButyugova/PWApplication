using PWApplication.Models;

namespace PWApplication.Services
{
    public interface IAccount
    {
        User GetUser(string userName);
    }
}
