using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using PWApplication.Models;
using PWApplication.Options;
using PWApplication.Services;
using PWApplication.ViewModels;
using System;
using System.Threading.Tasks;

namespace PWApplication.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly SettingsOptions _settingsOptions;
        private readonly ITransferService _exchangeService;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager,
            IOptions<SettingsOptions> settingsOptionsProvider, ITransferService exchangeService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _settingsOptions = settingsOptionsProvider.Value;
            _exchangeService = exchangeService;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                User user = new User { UserName = model.Name, Email = model.Email};
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, false);
                    _exchangeService.IncreaseBalance(user.Id, _settingsOptions.RegistrationAward);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View(new LoginViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var user = await _userManager.FindByEmailAsync(model.Email);
                    if (user == null)
                    {
                        throw new Exception();
                    }

                    var result = await _signInManager.PasswordSignInAsync(user.UserName, model.Password, false, false);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        throw new Exception();
                    }
                }
                catch
                {
                    ModelState.AddModelError("", "Uncorrect email and (or) password");
                }
                
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
