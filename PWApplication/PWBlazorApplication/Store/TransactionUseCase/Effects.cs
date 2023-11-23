using Fluxor;
using Microsoft.AspNetCore.Components.Authorization;
using PWApplication.BLL.Services;

namespace PWBlazorApplication.Store.TransactionUseCase
{
    public class Effects
    {
        AuthenticationStateProvider _authenticationStateProvider;
        IAccountService _accountService;
        ITransactionService _transactionService;
        ITransferService _transferService;

		public Effects(AuthenticationStateProvider authenticationStateProvider, IAccountService accountService, ITransactionService transactionService, ITransferService transferService) 
        {
            _authenticationStateProvider = authenticationStateProvider;
            _accountService = accountService;
            _transactionService = transactionService;
			_transferService = transferService;
		}

        [EffectMethod]
        public async Task HandleFetchUsersAction(FetchUsersAction action, IDispatcher dispatcher)
        {
            var state = await _authenticationStateProvider.GetAuthenticationStateAsync();
            var userName = state.User.Identity.Name;
            var users = new List<string>();
            users.Add("");
            users.AddRange(_accountService.GetOtherUsersNames(userName));
            dispatcher.Dispatch(new FetchUsersResultAction(users, userName));
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
	}
}
