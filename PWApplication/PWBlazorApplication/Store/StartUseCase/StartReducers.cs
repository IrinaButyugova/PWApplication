using Fluxor;

namespace PWBlazorApplication.Store.StartUseCase
{
    public class StartReducers
    {
        [ReducerMethod]
        public static StartState ReduceCheckSignInResultAction(StartState state, LoginResultAction action)
        {
            var newState = new StartState(action.Result.Succeeded, false, action.Result.Errors);
            return newState;
        }

        [ReducerMethod]
        public static StartState ReduceRegisterResultAction(StartState state, RegisterResultAction action)
        {
            var newState = new StartState(false, action.Result.Succeeded, action.Result.Errors);
            return newState;
        }
    }
}
