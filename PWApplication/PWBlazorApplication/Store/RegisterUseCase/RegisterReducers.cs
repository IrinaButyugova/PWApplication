using Fluxor;

namespace PWBlazorApplication.Store.RegisterUseCase
{
	public class RegisterReducers
	{
		[ReducerMethod]
		public static RegisterState ReduceRegisterResultAction(RegisterState state, RegisterResultAction action)
		{
			var newState = new RegisterState(action.Result.Succeeded, action.Result.Errors);
			return newState;
		}
	}
}
