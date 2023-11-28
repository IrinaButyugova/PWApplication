using Microsoft.AspNetCore.Identity;
using System.Collections.Concurrent;
using PWApplication.Domain.Models;
using PWComponents.Models;

namespace PWBlazorApplication
{
    public class BlazorCookieLoginMiddleware
    {
        public static IDictionary<Guid, LoginModel> Logins { get; private set; }
            = new ConcurrentDictionary<Guid, LoginModel>();

        private readonly RequestDelegate _next;

        public BlazorCookieLoginMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, UserManager<User> userManager, SignInManager<User> signInManager)
        {
            if (context.Request.Path == "/login" && context.Request.Query.ContainsKey("key"))
            {
                var key = Guid.Parse(context.Request.Query["key"]);
                var info = Logins[key];

                var user = await userManager.FindByEmailAsync(info.Email);
                var result = await signInManager.PasswordSignInAsync(user.UserName, info.Password, false, false);
                if (result.Succeeded)
                {
                    Logins.Remove(key);
                    context.Response.Redirect("/");
                }
                else
                {
                    //TODO: Proper error handling
                    context.Response.Redirect("/loginfailed");
                }
            }
            else
            {
                await _next.Invoke(context);
            }
        }
    }
}
