using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using UsersManagementsEmployees.Web;
using MudBlazor.Services;
using Management;
using Refit;



var builder = WebAssemblyHostBuilder.CreateDefault(args);


ApiSetting apiSettings = builder.Configuration.GetSection(nameof(ApiSetting))
                                                     .Get<ApiSetting>()!;

builder.Services.AddRefitClient<IEmployeeSdk>()
    .ConfigureHttpClient(c => c.BaseAddress = new Uri(apiSettings.UrlManagementApi));

builder.Services.AddRefitClient<IUserSdk>()
    .ConfigureHttpClient(c => c.BaseAddress = new Uri(apiSettings.UrlManagementApi));



builder.Services.AddMudServices();

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

await builder.Build().RunAsync();
