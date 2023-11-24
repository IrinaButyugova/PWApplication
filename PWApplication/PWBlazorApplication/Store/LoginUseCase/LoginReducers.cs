using Fluxor;
using PWApplication.BLL.Errors;

namespace PWBlazorApplication.Store.LoginUseCase
{
    public class LoginReducers
    {
        [ReducerMethod]
        public static LoginState ReduceCheckSignInAction(LoginState state, CheckSignInAction action)
        {
            var newState = new LoginState(false, new List<Error>());
            return newState;
        }

        [ReducerMethod]
        public static LoginState ReduceCheckSignInResultAction(LoginState state, CheckSignInResultAction action)
        {
            var newState = new LoginState(action.Result.Succeeded, action.Result.Errors);
            return newState;
        }
    }
}
