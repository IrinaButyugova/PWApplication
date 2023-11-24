using PWApplication.BLL.Result;

namespace PWBlazorApplication.Store.LoginUseCase
{
    public record CheckSignInResultAction
    {
        public PWResult Result { get; init; }

        public CheckSignInResultAction(PWResult result)
        {
            Result = result;
        }
    }
}
