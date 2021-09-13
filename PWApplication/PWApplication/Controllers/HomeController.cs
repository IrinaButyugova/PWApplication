using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PWApplication.BLL.Enums;
using PWApplication.BLL.Services;
using PWApplication.Domain.Models;
using PWApplication.ViewModels;
using System;
using System.Diagnostics;

namespace PWApplication.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IAccountService _accountService;
        private readonly ITransactionService _transactionService;

        public HomeController(ILogger<HomeController> logger, IAccountService accountService, 
            ITransactionService transactionService)
        {
            _logger = logger;
            _accountService = accountService;
            _transactionService = transactionService;
        }

        public IActionResult IndexAsync(DateTime? startDate, DateTime? endDate, string correspondentName, decimal? startAmount,
            decimal? endAmount, SortState sortOrder = SortState.DateDesc)
        {
            if (User.Identity.IsAuthenticated)
            {
                var userName = User.Identity.Name;
                var user = _accountService.GetUser(userName);
                if (user != null)
                {
                    var transactions = _transactionService.GetTransactions(userName, startDate, endDate, correspondentName,
                        startAmount, endAmount, sortOrder);
                    var filterViewModel = new FilterViewModel
                    {
                        StartDate = startDate,
                        EndDate = endDate,
                        CorrespondentName = correspondentName,
                        StartAmount = startAmount,
                        EndAmount = endAmount
                    };
                    IndexViewModel viewModel = new IndexViewModel
                    {
                        Name = user.UserName,
                        Balance = user.Balance,
                        Transactions = transactions,
                        FilterViewModel = filterViewModel,
                        SortViewModel = new SortViewModel(sortOrder),
                    };

                    return View(viewModel);
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
