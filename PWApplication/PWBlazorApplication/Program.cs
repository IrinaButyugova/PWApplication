using Fluxor;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PWApplication.DAL.Data;
using PWApplication.BLL.Services;
using PWApplication.Domain.Models;
using PWApplication.DAL.Repositories;
using PWBlazorApplication;
using PWApplication.Domain.Options;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("ApplicationContextConnection") ?? throw new InvalidOperationException("Connection string 'ApplicationContextConnection' not found.");

builder.Services.AddDbContext<ApplicationContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddIdentity<User, IdentityRole>(opts => {
    opts.User.RequireUniqueEmail = true;
})
.AddEntityFrameworkStores<ApplicationContext>();

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

builder.Services.Configure<SettingsOptions>(builder.Configuration.GetSection("Settings"));
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<ITransferService, TransferService>();
builder.Services.AddScoped<ITransactionService, TransactionService>();
builder.Services.AddScoped<IRepository<User>, UserRepository>();
builder.Services.AddScoped<IRepository<Transaction>, TransactionRepository>();
builder.Services.AddScoped<IRepositoryService, RepositoryService>();
var currentAssembly = typeof(Program).Assembly;
builder.Services.AddFluxor(options => {
    options.ScanAssemblies(currentAssembly);
#if DEBUG
	options.UseReduxDevTools();
#endif
});

var app = builder.Build();

app.UseStaticFiles();

app.MapBlazorHub();

app.MapFallbackToPage("/_Host");
app.UseAuthentication();;
app.UseMiddleware<BlazorCookieLoginMiddleware>();
app.UseMiddleware<BlazorCookieLogoutMiddleware>();

app.Run();
