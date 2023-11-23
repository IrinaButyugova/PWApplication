using Fluxor;

namespace PWBlazorApplication.Store.TransactionUseCase
{
    public class TransactionReducers
    {
        [ReducerMethod]
        public static TransactionState ReduceFetchUsersAction(TransactionState state, FetchUsersResultAction action)
        {
            var newState = new TransactionState(action.Users, action.UserName, true, null);
            if (state.Model != null)
            {
                newState.Model.RecipientName = state.Model.RecipientName;
                newState.Model.Amount = state.Model.Amount;
            }
            return newState;
        }

        [ReducerMethod]
        public static TransactionState ReduceFetchTransactionDataAction(TransactionState state, FetchTransactionResultAction action)
        {
            var newState = new TransactionState(state.Model.Users, state.UserName, true, null);
            newState.Model.RecipientName = action.RecipientName;
            newState.Model.Amount = action.Amount;
            return newState;
        }

		[ReducerMethod]
		public static TransactionState ReduceCreateTransactionAction(TransactionState state, CreateTransactionResultAction action)
        {
			var newState = new TransactionState(state.Model.Users, state.UserName, action.Succeeded, action.Errors);
            return newState;
		}
	}
}
