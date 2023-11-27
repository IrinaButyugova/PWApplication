using Fluxor;
using Microsoft.AspNetCore.Components.Authorization;
using PWApplication.BLL.Enums;
using PWApplication.BLL.Services;
using PWComponents.Models;

namespace PWBlazorApplication.Store.HomeUseCase
{
    public class HomeEffects
    {
        private AuthenticationStateProvider _authenticationStateProvider;
        private IAccountService _accountService;
        private ITransactionService _transactionService;

        public HomeEffects(AuthenticationStateProvider authenticationStateProvider, IAccountService accountService, ITransactionService transactionService)
        {
            _authenticationStateProvider = authenticationStateProvider;
            _accountService = accountService;
            _transactionService = transactionService;
        }

        [EffectMethod]
        public async Task HandleFetchHomeDataAction(FetchHomeDataAction action, IDispatcher dispatcher)
        {
            var state = await _authenticationStateProvider.GetAuthenticationStateAsync();
            var stateUser = state.User;
            if (stateUser == null)
            {
                return;
            }

            var user = _accountService.GetUser(stateUser.Identity.Name);
            if (user == null)
            {
                return;
            }

            var filter = new FilterModel();
            var currentSort = SortState.DateDesc;
            var transactions = _transactionService.GetTransactions(user.UserName, filter.StartDate, filter.EndDate, filter.CorrespondentName, filter.StartAmount, filter.EndAmount, currentSort);
            dispatcher.Dispatch(new FetchHomeResultAction(user.UserName, user.Balance, transactions, filter, currentSort));
        }

		[EffectMethod]
		public async Task HandleFetchTransactionsActions(FetchTransactionsAction action, IDispatcher dispatcher)
		{
			var transactions = _transactionService.GetTransactions(action.UserName, action.StartDate, action.EndDate, action.CorrespondentName, action.StartAmount, action.EndAmount, action.SortState);
            var filter = new FilterModel()
            {
                StartDate = action.StartDate,
                EndDate = action.EndDate,
                CorrespondentName = action.CorrespondentName,
                StartAmount = action.StartAmount,
                EndAmount = action.EndAmount
            };

            dispatcher.Dispatch(new FetchTransactionsResultAction(transactions, filter, action.SortState));
		}

	}
}
