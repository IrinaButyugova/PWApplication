using PWApplication.BLL.Result;

namespace PWBlazorApplication.Store.StartUseCase
{
    public record LoginResultAction
    {
        public PWResult Result { get; init; }

        public LoginResultAction(PWResult result)
        {
            Result = result;
        }
    }
}
