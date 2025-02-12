using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Dima.Web;
using MudBlazor.Services;
using Dima.Web.Security;
using Microsoft.AspNetCore.Components.Authorization;
using Dima.Core.Handlers;
using Dima.Web.Handlers;
using System.Globalization;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
Configuration.BackendUrl = builder.Configuration.GetValue<string>("BackendUrl") ?? string.Empty;
builder.RootComponents.Add<HeadOutlet>("head::after");
//trazendo o cookie que foi criado
builder.Services.AddScoped<CookieHandler>();
//dependencia do addAuthorizationCore
builder.Services.AddAuthorizationCore();
//De Para, Dependencia do CookieAuthenticationStateProvider
builder.Services.AddScoped<AuthenticationStateProvider, CookieAuthenticationStateProvider>();
//Requirido sempre usar o IcookieAuthentacionStateProvider
builder.Services.AddScoped(x => (ICookieAuthenticationStateProvider)x.GetRequiredService<AuthenticationStateProvider>());
builder.Services.AddHttpClient(Configuration.HttpClientName, opt =>
{
    opt.BaseAddress = new Uri(Configuration.BackendUrl);
}).AddHttpMessageHandler<CookieHandler>();

builder.Services.AddMudServices();
builder.Services.AddTransient<IAccountHandler, AccountHandler>();
builder.Services.AddTransient<ITransactionHandler, TransactionHandler>();
builder.Services.AddTransient<ICategoryHandler, CategoryHandler>();
builder.Services.AddTransient<IReportHandler, ReportHandler>();

builder.Services.AddLocalization();
CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("pt-BR");
CultureInfo.DefaultThreadCurrentUICulture = new CultureInfo("pt-BR");

await builder.Build().RunAsync();
