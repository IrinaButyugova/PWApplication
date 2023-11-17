using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Identity;
using PWBlazorApplication.Models;
using PWApplication.BLL.Services;
using Microsoft.AspNetCore.Components;

namespace PWBlazorApplication.Components
{
	public partial class Login
	{
		[Inject]
		IAccountService AccountService { get; set; }
		[Inject]
		NavigationManager Navigation { get; set; }

		[Parameter]
		public EventCallback OnCancelClickCallback { get; set; }

		private LoginModel loginModel = new LoginModel();
		private EditContext? editContext;
		private ValidationMessageStore? messageStore;

		protected override void OnInitialized()
		{
			editContext = new(loginModel);
			messageStore = new(editContext);
		}

		private async void ValidSubmit()
		{
			messageStore.Clear();

			if (editContext == null || !editContext.Validate())
			{
				return;
			}

			var user = await AccountService.FindByEmail(loginModel.Email);
			if (user != null)
			{
				if (await AccountService.CanSignIn(user))
				{
					var checkResult = await AccountService.CheckPasswordSignIn(user, loginModel.Password);
					if (checkResult == SignInResult.Success)
					{
						Guid key = Guid.NewGuid();
						BlazorCookieLoginMiddleware.Logins[key] = loginModel;
						Navigation.NavigateTo($"/login?key={key}", true);
					}
					else
					{
						messageStore.Add(() => loginModel.Password, "Login failed. Check your password");
					}
				}
				else
				{
					messageStore.Add(() => loginModel.Email, "Your account is blocked");
				}
			}
			else
			{
				messageStore.Add(() => loginModel.Email, "User not found");
			}

			if (editContext.GetValidationMessages().Count() > 0)
			{
				editContext.NotifyValidationStateChanged();
			}
		}
	}
}
