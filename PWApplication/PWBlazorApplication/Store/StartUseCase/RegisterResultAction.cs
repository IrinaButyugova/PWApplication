using PWApplication.BLL.Result;

namespace PWBlazorApplication.Store.StartUseCase
{
    public record RegisterResultAction
    {
        public PWResult Result { get; init; }

        public RegisterResultAction(PWResult result)
        {
            Result = result;
        }
    }
}
