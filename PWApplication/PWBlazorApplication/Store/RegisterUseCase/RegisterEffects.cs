using Fluxor;
using PWApplication.BLL.Services;
using PWApplication.Domain.Models;

namespace PWBlazorApplication.Store.RegisterUseCase
{
	public class RegisterEffects
	{
		private IAccountService _accountService;
		public RegisterEffects(IAccountService accountService)
		{
			_accountService = accountService;
		}

		[EffectMethod]
		public async Task HandleRegisterAction(RegisterAction action, IDispatcher dispatcher)
		{
			var user = new User()
			{
				Email = action.Email,
				UserName = action.Name
			};

			var result = await _accountService.Register(user, action.Password);
			dispatcher.Dispatch(new RegisterResultAction(result));
		}
	}
}
