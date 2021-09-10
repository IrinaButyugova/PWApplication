using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PWApplication.Services;
using PWApplication.ViewModels;
using System;

namespace PWApplication.Controllers
{
    public class TransactionController : Controller
    {
        private readonly IAccountService _accountService;
        private readonly ITransferService _exchangeService;
        private readonly ITransactionService _transactionService;

        public TransactionController(IAccountService accountService, ITransferService exchangeService, ITransactionService transactionService)
        {
            _accountService = accountService;
            _exchangeService = exchangeService;
            _transactionService = transactionService;
        }

        [HttpGet]
        public IActionResult CreateTransaction(int? id)
        {
            var model = new CreateTransactionViewModel();
            if (id.HasValue)
            {
                var transaction = _transactionService.GetTransaction(id.Value);
                model.Amount = transaction.Amount;
                model.RecipientName = transaction.Correspondent.UserName;
            }
            model.Users = GetUserList();
            return View(model);
        }

         [HttpPost]
         public IActionResult CreateTransaction(CreateTransactionViewModel model)
         {
            if (ModelState.IsValid)
            {
                try
                {
                    if (model.Amount == 0)
                    {
                        throw new Exception("Amount must be > 0");
                    }

                    var userName = User.Identity.Name;
                    _exchangeService.CreateTransaction(userName, model.RecipientName, model.Amount);
                    return RedirectToAction("Index", "Home");
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("", e.Message);
                }
            }
            model.Users = GetUserList();
            return View(model);
        }

        private SelectList GetUserList()
        {
           return new SelectList(_accountService.GetOtherUsersNames(User.Identity.Name));
        }
    }
}
