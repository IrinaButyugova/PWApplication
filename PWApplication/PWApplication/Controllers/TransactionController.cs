using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PWApplication.BLL.Services;
using PWApplication.ViewModels;
using System;

namespace PWApplication.Controllers
{
    public class TransactionController : Controller
    {
        private readonly IAccountService _accountService;
        private readonly ITransferService _transferService;
        private readonly ITransactionService _transactionService;

        public TransactionController(IAccountService accountService, ITransferService transferService, ITransactionService transactionService)
        {
            _accountService = accountService;
            _transferService = transferService;
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
                        throw new Exception("Amount should be greater than zero");
                    }

                    var userName = User.Identity.Name;
                    var result = _transferService.CreateTransaction(userName, model.RecipientName, model.Amount);
                    if (result.Succeeded)
                    {
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
                catch (Exception e)
                {
                    ModelState.AddModelError(string.Empty, e.Message);
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
