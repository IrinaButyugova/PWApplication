using Fluxor;
using PWApplication.BLL.Errors;

namespace PWBlazorApplication.Store.StartUseCase
{
    [FeatureState]
    public record StartState
    {
        public bool LoginSucceeded { get; init; }
        public bool RegisterSucceeded { get; init; }
        public IEnumerable<Error> Errors { get; init; } = new List<Error>();

        public StartState()
        {

        }

        public StartState(bool loginSucceeded, bool registerSucceeded, IEnumerable<Error> errors)
        {
            LoginSucceeded = loginSucceeded;
            RegisterSucceeded = registerSucceeded;
            Errors = errors;
        }
    }
}
