using Employees.Management.Api;
using Management;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using Refit;
using UsersManagementsEmployees.Web;



var builder = WebAssemblyHostBuilder.CreateDefault(args);

//ApiSetting Instance
ApiSetting apiSettings = builder.Configuration.GetSection(nameof(ApiSetting))
                                                     .Get<ApiSetting>()!;

builder.Services.AddTransient<AuthDelegatingHandler>();
//Injecting SDK
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

//Injecting AuthServices
builder.Services.AddScoped<AuthService>(); 
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

await builder.Build().RunAsync();
