using Microsoft.AspNetCore.Components.Forms;
using PWBlazorApplication.Models;
using Microsoft.AspNetCore.Components;
using Fluxor;
using PWBlazorApplication.Store.LoginUseCase;

namespace PWBlazorApplication.Components
{
	public partial class Login
	{
		[Inject]
		NavigationManager Navigation { get; set; }
        [Inject]
        public IDispatcher Dispatcher { get; set; }
        [Inject]
		public IState<LoginState> LoginState { get; set; }

		[Parameter]
		public EventCallback OnCancelClickCallback { get; set; }

		private LoginModel _loginModel = new LoginModel();

        protected override void OnInitialized()
        {
            base.OnInitialized();
            LoginState.StateChanged += OnStateChanged;
        }

        private void OnStateChanged(object? sender, EventArgs e)
        {
            if (LoginState.Value.Succeeded)
            {
                Guid key = Guid.NewGuid();
                BlazorCookieLoginMiddleware.Logins[key] = _loginModel;
                Navigation.NavigateTo($"/login?key={key}", true);
            }
        }

        private void ValidSubmit(EditContext editContext)
		{
            if (editContext == null || !editContext.Validate())
			{
				return;
			}

			Dispatcher.Dispatch(new CheckSignInAction(_loginModel.Email, _loginModel.Password));
        }
	}
}
