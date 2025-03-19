using Employees.Management.Api;
using Management;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.JSInterop;
using MudBlazor.Services;
using Refit;
using UsersManagementsEmployees.Web;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddScoped<ProveedorAthenticationPrueba>();
builder.Services.AddScoped<AuthenticationStateProvider, ProveedorAthenticationPrueba>();
builder.Services.AddAuthorizationCore();

builder.Services.AddTransient<AuthDelegatingHandler>();
builder.Services.AddScoped<TokenServiceReader>();

builder.Services.AddAuthorizationCore(options =>
{
    options.AddPolicy("Authenticated", policy => policy.RequireAuthenticatedUser());
});

//ApiSetting Instance
ApiSetting apiSettings = builder.Configuration.GetSection(nameof(ApiSetting))
                                                     .Get<ApiSetting>()!;
//Injecting SDK and AuthDelegatingHandler trow skd wit refit
builder.Services.AddRefitClient<IEmployeeSdk>()
    .ConfigureHttpClient(c => c.BaseAddress = new Uri(apiSettings.UrlManagementApi))
    .AddHttpMessageHandler(x => x.GetRequiredService<AuthDelegatingHandler>());

builder.Services.AddRefitClient<IUserSdk>()
    .ConfigureHttpClient(c => c.BaseAddress = new Uri(apiSettings.UrlManagementApi))
    .AddHttpMessageHandler(x => x.GetRequiredService<AuthDelegatingHandler>());

builder.Services.AddRefitClient<IAccountSdk>()
    .ConfigureHttpClient(c => c.BaseAddress = new Uri(apiSettings.UrlManagementApi))
    .AddHttpMessageHandler(x => x.GetRequiredService<AuthDelegatingHandler>());

//MudBlazor services
builder.Services.AddMudServices();

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddSingleton(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) }); //use with Http Requestess

await builder.Build().RunAsync();
