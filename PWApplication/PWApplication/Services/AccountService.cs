using Microsoft.EntityFrameworkCore;
using PWApplication.Models;
using System.Linq;

namespace PWApplication.Services
{
    public class AccountService : IAccountService
    {
        private readonly ApplicationContext _appContext;

        public AccountService(ApplicationContext appContext)
        {
            _appContext = appContext;
        }

        public User GetUser(string userName)
        {
            return _appContext.Users
                .Include(x => x.Transactions)
                .ThenInclude(x => x.Correspondent)
                .Where(x => x.UserName == userName)
                .FirstOrDefault();
        }
    }
}
