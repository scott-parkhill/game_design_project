global using Chaos.Services;

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Chaos.Areas.Identity;
using Chaos.Models.DbModels;
using Chaos.Services.GameDbService;
using Blazored.Toast;
using Chaos.Business;
using Chaos.Data;

var builder = WebApplication.CreateBuilder(args);

#region Logging

builder.Logging.ClearProviders();
builder.Logging.AddConsole();

#endregion

#region Database

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContextFactory<GameDbContext>(options =>
    options.UseSqlite(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddDefaultIdentity<GameUser>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
    options.Password.RequireDigit = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric = false;
})
    .AddUserManager<UserManager<GameUser>>()
    .AddRoles<IdentityRole>()
    .AddSignInManager<SignInManager<GameUser>>()
    .AddEntityFrameworkStores<GameDbContext>();

#endregion

#region Services

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddBlazoredToast();

#endregion

#region DI Services

builder.Services.AddScoped<AuthenticationStateProvider, RevalidatingIdentityAuthenticationStateProvider<GameUser>>();
builder.Services.AddScoped<IGameDbService, GameDbService>();
builder.Services.AddScoped<BattleCalculator>();
builder.Services.AddScoped<BattleManager>();

#endregion

var app = builder.Build();

#region HTTP Request Pipeline

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

#endregion

app.Run();
