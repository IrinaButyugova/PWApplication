using Fluxor;
using PWApplication.BLL.Errors;
using PWApplication.BLL.Result;

namespace PWBlazorApplication.Store.LoginUseCase
{
    [FeatureState]
    public record LoginState
	{
        public bool Succeeded { get; init; }
        public List<Error> Errors { get; init; } = new List<Error>();

        private LoginState()
		{

		}
		public LoginState(bool succeeded, List<Error> errors)
		{
            Succeeded = succeeded;
            Errors = errors;
		}
    }
}
