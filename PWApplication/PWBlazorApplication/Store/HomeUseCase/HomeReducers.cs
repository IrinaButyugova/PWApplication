using Fluxor;
using PWApplication.BLL.Errors;
using PWComponents.Models;

namespace PWBlazorApplication.Store.HomeUseCase
{
	public class HomeReducers
	{
		[ReducerMethod]
		public static HomeState ReduceFetchHomeDataAction(HomeState state, FetchHomeResultAction action)
		{
			var newState = new HomeState(action.Name, action.Balance, action.Transactions, action.FilterModel,
			action.CurrentSort, action.Users);
			return newState;
		}

		[ReducerMethod]
		public static HomeState ReduceFetchTransactionAction(HomeState state, FetchTransactionsResultAction action) 
		{
			var newState = state with { Transactions = action.Transactions, FilterModel = action.FilterModel, CurrentSort = action.CurrentSort, CreationSucceeded = false, 
				CreationErrors = new List<Error>() };
			return newState;
		}

		[ReducerMethod]
		public static HomeState ReduceFetchTransactionDataAction(HomeState state, FetchTransactionResultAction action)
		{
			var createTransactionModel = new CreateTransactionModel()
			{ 
				RecipientName = action.RecipientName,
				Amount = action.Amount
			};
			var newState = state with { CreateTransactionModel = createTransactionModel, CreationSucceeded = false, CreationErrors = new List<Error>() };

			return newState;
		}

		[ReducerMethod]
		public static HomeState ReduceCreateTransactionAction(HomeState state, CreateTransactionResultAction action)
		{
			var newState = state with { CreationSucceeded = action.Succeeded, CreationErrors = action.Errors };
			return newState;
		}
	}
}
