using Microsoft.AspNetCore.Components.Forms;
using PWBlazorApplication.Models;
using PWBlazorApplication.Store.RegisterUseCase;
using Microsoft.AspNetCore.Components;
using Fluxor;

namespace PWBlazorApplication.Components
{
	public partial class Register
	{
		[Inject]
		NavigationManager Navigation { get; set; }
		[Inject]
		public IDispatcher Dispatcher { get; set; }
		[Inject]
		public IState<RegisterState> RegisterState { get; set; }

		[Parameter]
        public EventCallback OnCancelClickCallback { get; set; }

		private RegisterModel _registerModel = new RegisterModel();

		protected override void OnInitialized()
		{
			base.OnInitialized();
			RegisterState.StateChanged += OnStateChanged;
		}

		private void OnStateChanged(object? sender, EventArgs e)
		{
			if (RegisterState.Value.Succeeded)
			{
				Guid key = Guid.NewGuid();
				var loginModel = new LoginModel()
				{
					Email = _registerModel.Email,
					Password = _registerModel.Password
				};
				BlazorCookieLoginMiddleware.Logins[key] = loginModel;
				Navigation.NavigateTo($"/login?key={key}", true);
			}
		}

		public void ValidSubmit(EditContext editContext)
		{
			if (editContext == null || !editContext.Validate())
			{
				return;
			}

			Dispatcher.Dispatch(new RegisterAction(_registerModel.Name, _registerModel.Email, _registerModel.Password));
		}
	}
}
