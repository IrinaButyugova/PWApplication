using Microsoft.AspNetCore.Identity;
using PWApplication.Domain.Models;
using System.Collections.Concurrent;
using static System.Net.Mime.MediaTypeNames;

namespace PWBlazorApplication
{
    public class BlazorCookieLogoutMiddleware
    {
        private readonly RequestDelegate _next;

        public BlazorCookieLogoutMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, SignInManager<User> signInManager)
        {
            if (context.Request.Path == "/logout")
            {
                await signInManager.SignOutAsync();
                context.Response.Redirect("/");
            }
            else
            {
                await _next.Invoke(context);
            }
        }
    }
}
