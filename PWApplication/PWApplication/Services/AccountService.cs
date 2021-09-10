using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using PWApplication.Errors;
using PWApplication.Models;
using PWApplication.Options;
using PWApplication.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PWApplication.Services
{
    public class AccountService : IAccountService
    {
        private readonly ApplicationContext _appContext;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ITransferService _transferService;
        private readonly SettingsOptions _settingsOptions;

        public AccountService(ApplicationContext appContext, UserManager<User> userManager, SignInManager<User> signInManager,
            ITransferService transferService, IOptions<SettingsOptions> settingsOptionsProvider)
        {
            _appContext = appContext;
            _userManager = userManager;
            _signInManager = signInManager;
            _transferService = transferService;
            _settingsOptions = settingsOptionsProvider.Value;
        }

        public User GetUser(string userName)
        {
            return _appContext.Users
                .Include(x => x.Transactions)
                .ThenInclude(x => x.Correspondent)
                .Where(x => x.UserName == userName)
                .FirstOrDefault();
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
    }
}
