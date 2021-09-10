using Microsoft.AspNetCore.Identity;
using PWApplication.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PWApplication.Services
{
    public interface IAccountService
    {
        User GetUser(string userName);

        List<string> GetOtherUsersNames(string userName);

        Task<IdentityResult> Register(User user, string password);

        Task Login(string email, string password);

        Task Logout();
    }
}
