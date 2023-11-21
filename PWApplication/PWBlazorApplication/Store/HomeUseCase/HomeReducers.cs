using Fluxor;

namespace PWBlazorApplication.Store.HomeUseCase
{
	public class HomeReducers
	{
		[ReducerMethod]
		public static HomeState ReduceFetchHomeDataAction(HomeState state, FetchHomeResultAction action) => new HomeState(action.Name, action.Balance, action.Transactions, action.FilterModel,
			action.CurrentSort);

		[ReducerMethod]
		public static HomeState ReduceFetchTransactionAction(HomeState state, FetchTransactionsResultAction action)  {
			var newState = state with { Transactions = action.Transactions, FilterModel = action.FilterModel, CurrentSort = action.CurrentSort };
			return newState;
			}
	}
}
