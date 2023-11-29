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
		private ITransferService _transferService;

		public HomeEffects(AuthenticationStateProvider authenticationStateProvider, IAccountService accountService, ITransactionService transactionService, ITransferService transferService)
        {
            _authenticationStateProvider = authenticationStateProvider;
            _accountService = accountService;
            _transactionService = transactionService;
            _transferService = transferService;
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
			var transactions = _transactionService.GetTransactions(user.UserName, filter.StartDate, filter.EndDate, filter.CorrespondentName, filter.StartAmount, filter.EndAmount, currentSort, 
                1, action.PageSize);

			var users = new List<string>();
			users.Add("");
			users.AddRange(_accountService.GetOtherUsersNames(user.UserName));

            var transactionsCount = _transactionService.GetTransactionsCount(user.UserName, filter.StartDate, filter.EndDate, filter.CorrespondentName, filter.StartAmount, filter.EndAmount);
            var pagesCount = GetPagesCount(transactionsCount, action.PageSize);

			dispatcher.Dispatch(new FetchHomeResultAction(user.UserName, user.Balance, transactions, filter, currentSort, users, pagesCount));
        }

		[EffectMethod]
		public async Task HandleFetchTransactionsActions(FetchTransactionsAction action, IDispatcher dispatcher)
		{
			var transactions = _transactionService.GetTransactions(action.UserName, action.StartDate, action.EndDate, action.CorrespondentName, action.StartAmount, action.EndAmount, action.SortState,
                action.PageNumber, action.PageSize);
            var filter = new FilterModel()
            {
                StartDate = action.StartDate,
                EndDate = action.EndDate,
                CorrespondentName = action.CorrespondentName,
                StartAmount = action.StartAmount,
                EndAmount = action.EndAmount
			};

			var transactionsCount = _transactionService.GetTransactionsCount(action.UserName, action.StartDate, action.EndDate, action.CorrespondentName, action.StartAmount, action.EndAmount);
			var pagesCount = GetPagesCount(transactionsCount, action.PageSize);

			dispatcher.Dispatch(new FetchTransactionsResultAction(transactions, filter, action.SortState, action.PageNumber, pagesCount));
		}

		[EffectMethod]
		public async Task HandleFetchTransactionDataAction(FetchTransactionDataAction action, IDispatcher dispatcher)
		{
			var transaction = _transactionService.GetTransaction(action.Id);
			dispatcher.Dispatch(new FetchTransactionResultAction(transaction.Correspondent.UserName, transaction.Amount));
		}

		[EffectMethod]
		public async Task HandleCreateTransactionAction(CreateTransactionAction action, IDispatcher dispatcher)
		{
			var result = _transferService.CreateTransaction(action.UserName, action.RecipientName, action.Amount);
			dispatcher.Dispatch(new CreateTransactionResultAction(result.Succeeded, result.Errors));
		}

        private int GetPagesCount(int transactionsCount, int pageSize)
        {
			return (int)Math.Ceiling(transactionsCount / (double)pageSize);
		}
	}
}
