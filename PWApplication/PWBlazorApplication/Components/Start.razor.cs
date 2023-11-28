using Fluxor;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using PWBlazorApplication.Store.StartUseCase;
using PWComponents.Models;

namespace PWBlazorApplication.Components
{
	public partial class Start
	{
		[Inject]
		NavigationManager Navigation { get; set; }
		[Inject]
		public IDispatcher Dispatcher { get; set; }
		[Inject]
		public IState<StartState> StartState { get; set; }

		private bool _loginVisible;
		private bool _registrationVisible;
        private LoginModel _loginModel = new LoginModel();
        private RegisterModel _registerModel = new RegisterModel();

        protected override void OnInitialized()
        {
            base.OnInitialized();
            StartState.StateChanged += OnStateChanged;
        }

        private void OnStateChanged(object? sender, EventArgs e)
        {
            if (!StartState.Value.LoginSucceeded && !StartState.Value.RegisterSucceeded)
            {
				return;
            }

			if(StartState.Value.RegisterSucceeded)
			{
				_loginModel.Email = _registerModel.Email;
				_loginModel.Password = _registerModel.Password;

            }

            Guid key = Guid.NewGuid();
            BlazorCookieLoginMiddleware.Logins[key] = _loginModel;
            Navigation.NavigateTo($"/login?key={key}", true);
        }

        private void Login()
		{
			_loginVisible = true;
		}

		private void CancelLogin()
		{
			_loginVisible = false;
		}

		private void Registration()
		{
			_registrationVisible = true;
		}

		private void CancelRegistration()
		{
			_registrationVisible = false;
		}

		public void LoginSubmit(EditContext editContext)
		{
			if (editContext == null || !editContext.Validate())
			{
				return;
			}

			Dispatcher.Dispatch(new CheckSignInAction(_loginModel.Email, _loginModel.Password));
		}

		public void RegisterSubmit(EditContext editContext)
		{
            if (editContext == null || !editContext.Validate())
            {
                return;
            }

            Dispatcher.Dispatch(new RegisterAction(_registerModel.Name, _registerModel.Email, _registerModel.Password));
        }
	}
}
