namespace PWBlazorApplication.Store.StartUseCase
{
    public record CheckSignInAction
    {
        public string Email { get; init; }
        public string Password { get; init; }

        public CheckSignInAction(string email, string password)
        {
            Email = email;
            Password = password;
        }
    }
}
