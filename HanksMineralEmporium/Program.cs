using System.Globalization;
using Microsoft.AspNetCore.Localization;

using MudBlazor.Services;

using HanksMineralEmporium.Core.InventoryManagement;
using HanksMineralEmporium.Core.SalesManagement;
using HanksMineralEmporium.Core.UserManagement;
using HanksMineralEmporium.Data.DatabaseIO;
using HanksMineralEmporium.Data.DatabaseIO.Json;
using HanksMineralEmporium.Service.AuthenticationService;
using HanksMineralEmporium.Service.InventoryService;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

// Add services to the container.
services.AddRazorPages();
services.AddServerSideBlazor();
services.AddMudServices();

// Configure HttpContextAccessor and Session
services.AddHttpContextAccessor();
services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// Configure database services
services.AddSingleton<IItemDatabaseOperator, JsonItemDatabaseOperator>();
services.AddSingleton<IUserDatabaseOperator, JsonUserDatabaseOperator>();
services.AddSingleton<IReceiptDatabaseOperator, JsonReceiptDatabaseOperator>();
services.AddSingleton<ISalesDatabaseOperator, JsonSalesDatabaseOperator>();

// Configure manager services
services.AddSingleton<IInventoryManager, InventoryManager>();
services.AddSingleton<IUserManager, UserManager>();
services.AddSingleton<ISalesManager, SalesManager>();

// Configure services
services.AddScoped<IAuthenticationService, AuthenticationService>();
services.AddScoped<IInventoryService, InventoryService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

var supportedCultures = new[] { new CultureInfo("en-US") };
app.UseRequestLocalization(new RequestLocalizationOptions
{
    DefaultRequestCulture = new RequestCulture("en-US"),
    SupportedCultures = supportedCultures,
    SupportedUICultures = supportedCultures
});

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseSession();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();