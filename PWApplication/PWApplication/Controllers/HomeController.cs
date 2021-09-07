using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PWApplication.Models;
using PWApplication.Services;
using System.Diagnostics;
using System.Threading.Tasks;

namespace PWApplication.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<User> _userManager;
        private readonly IAccount _accountService;

        public HomeController(ILogger<HomeController> logger, UserManager<User> userManager, IAccount accountService)
        {
            _logger = logger;
            _userManager = userManager;
            _accountService = accountService;
        }

        public IActionResult IndexAsync()
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = _accountService.GetUser(User.Identity.Name);
                if (user != null)
                {
                    return View(user);
                }
            }
            
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
