using Microsoft.AspNetCore.Identity;
using PWApplication.BLL.Result;
using PWApplication.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PWApplication.BLL.Services
{
    public interface IAccountService
    {
        User GetUser(string userName);

        List<string> GetOtherUsersNames(string userName);

        Task<PWResult> Register(User user, string password);
        Task<PWResult> RegisterAndSign(User user, string password);

        Task<PWResult> Login(string email, string password);

        Task Logout();
        Task<User> FindByEmail(string email);
        Task<bool> CanSignIn(User user);
        Task<SignInResult> CheckPasswordSignIn(User user, string password);
    }
}
