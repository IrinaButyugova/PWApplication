namespace PWBlazorApplication.Components
{
	public partial class Start
	{
		private bool _loginVisible;
		private bool _registrationVisible;

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
	}
}
