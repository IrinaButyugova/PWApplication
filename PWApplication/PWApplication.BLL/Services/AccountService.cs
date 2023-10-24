using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using PWApplication.BLL.Errors;
using PWApplication.BLL.Result;
using PWApplication.DAL.Repositories;
using PWApplication.Domain.Options;
using PWApplication.Domain.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PWApplication.BLL.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ITransferService _transferService;
        private readonly SettingsOptions _settingsOptions;
        private readonly IRepositoryService _repositoryService;

        public AccountService(UserManager<User> userManager, SignInManager<User> signInManager,
            ITransferService transferService, IOptions<SettingsOptions> settingsOptionsProvider,
            IRepositoryService repositoryService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _transferService = transferService;
            _settingsOptions = settingsOptionsProvider.Value;
            _repositoryService = repositoryService;
        }

        public User GetUser(string userName)
        {
            return _repositoryService.Users.GetByUserName(userName);
        }

        public List<string> GetOtherUsersNames(string userName)
        {
            return _userManager.Users
                .Where(x => x.UserName != userName)
                .Select(x => x.UserName)
                .ToList();
        }

        public async Task<PWResult> Register(User user, string password)
        {
            var result = new PWResult();

            var createResult = await _userManager.CreateAsync(user, password);
            if (createResult.Succeeded)
            {
                await _signInManager.SignInAsync(user, false);
                _transferService.IncreaseBalance(user.Id, _settingsOptions.RegistrationAward);
                result.Succeeded = true;
            }
            else
            {
                foreach (var error in createResult.Errors)
                {
                    result.Errors.Add(new Error 
                    { 
                        Code = error.Code, 
                        Description = error.Description 
                    });
                }
            }
            
            return result;
        }

        public async Task<PWResult> Login(string email, string password)
        {
            var result = new PWResult();

            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                result.Errors.Add(new Error 
                { 
                    Code = ErrorCodes.USER_NOT_FOUND, 
                    Description = "User with provided Email doesn't exist" 
                });
                return result;
            }

            var signInResult = await _signInManager.PasswordSignInAsync(user.UserName, password, false, false);
            if (signInResult.Succeeded)
            {
                result.Succeeded = true;
            }
            else
            {
                result.Errors.Add(new Error
                {
                    Code = ErrorCodes.WRONG_PASSWORD,
                    Description = "Password is wrong"
                });
            }
            return result;
        }

        public async Task Logout()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<User> FindByEmail(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }

        public async Task<bool> CanSignIn(User user)
        {
            return await _signInManager.CanSignInAsync(user);
        }

        public async Task<SignInResult> CheckPasswordSignIn(User user, string password)
        {
            return await _signInManager.CheckPasswordSignInAsync(user, password, true);
        }
    }
}
