﻿namespace PWBlazorApplication.Store.StartUseCase
{
    public record RegisterAction
    {
        public string Name { get; init; }

        public string Email { get; init; }

        public string Password { get; init; }

        public RegisterAction(string name, string email, string password)
        {
            Name = name;
            Email = email;
            Password = password;
        }
    }
}
