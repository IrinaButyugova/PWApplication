using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PWApplication.Models;
using PWApplication.Services;
using PWApplication.ViewModels;
using System;
using System.Linq;

namespace PWApplication.Controllers
{
    public class TransactionController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly IExchange _exchangeService;

        public TransactionController(UserManager<User> userManager, IExchange exchangeService)
        {
            _userManager = userManager;
            _exchangeService = exchangeService;
        }

        [HttpGet]
        public IActionResult CreateTransaction(int? id)
        {
            var model = new CreateTransactionViewModel();
            if (id.HasValue)
            {
                var transaction = _exchangeService.GetTransaction(id.Value);
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
           return new SelectList(_userManager.Users
                .Where(x => x.UserName != User.Identity.Name)
                .Select(x => x.UserName)
                .ToList());
        }
    }
}
