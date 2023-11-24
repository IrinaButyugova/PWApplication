using Fluxor;
using Microsoft.AspNetCore.Identity;
using PWApplication.BLL.Errors;
using PWApplication.BLL.Result;
using PWApplication.BLL.Services;

namespace PWBlazorApplication.Store.LoginUseCase
{
    public class LoginEffects
    {
        private IAccountService _accountService;
        public LoginEffects(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [EffectMethod]
        public async Task HandleFetchUserAction(CheckSignInAction action, IDispatcher dispatcher)
        {
            var result = new PWResult();

            var user = await _accountService.FindByEmail(action.Email);
            if (user != null)
            {
                if (await _accountService.CanSignIn(user))
                {
                    var checkResult = await _accountService.CheckPasswordSignIn(user, action.Password);
                    if (checkResult != SignInResult.Success)
                    {
                        var error = new Error()
                        {
                            Code = ErrorCodes.WRONG_PASSWORD,
                            Description = "Login failed. Check your password"

                        };
                        result.Errors.Add(error);
                    }
                }
                else
                {
                    var error = new Error()
                    {
                        Code = ErrorCodes.ACCOUNT_BLOCKED,
                        Description = "Your account is blocked"
                    };
                    result.Errors.Add(error);
                }
            }
            else
            {
                var error = new Error()
                {
                    Code = ErrorCodes.USER_NOT_FOUND,
                    Description = "User with provided Email doesn't exist"
                };
                result.Errors.Add(error);
            }

            if (result.Errors.Count == 0)
            {
                result.Succeeded = true;
            }

            dispatcher.Dispatch(new CheckSignInResultAction(result));
        }
    }
}
