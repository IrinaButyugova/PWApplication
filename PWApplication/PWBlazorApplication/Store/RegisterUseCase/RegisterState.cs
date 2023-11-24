using Fluxor;
using PWApplication.BLL.Errors;

namespace PWBlazorApplication.Store.RegisterUseCase
{
	[FeatureState]
	public record RegisterState
	{
		public bool Succeeded { get; init; }
		public List<Error> Errors { get; init; } = new List<Error>();

		private RegisterState()
		{

		}
		public RegisterState(bool succeeded, List<Error> errors)
		{
			Succeeded = succeeded;
			Errors = errors;
		}
	}
}
