using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using PWApplication.Models;
using PWApplication.Options;
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

        public async Task<IdentityResult> Register(User user, string password)
        {
            var result = await _userManager.CreateAsync(user, password);
            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, false);
                _transferService.IncreaseBalance(user.Id, _settingsOptions.RegistrationAward);
            }

            return result;
        }
        public async Task Login(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                throw new Exception();
            }

            var result = await _signInManager.PasswordSignInAsync(user.UserName, password, false, false);
            if (!result.Succeeded)
            {
                throw new Exception();
            }
        }

        public async Task Logout()
        {
            await _signInManager.SignOutAsync();
        }
    }
}
