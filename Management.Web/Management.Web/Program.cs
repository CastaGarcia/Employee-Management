using CurrieTechnologies.Razor.SweetAlert2;
using Management;
using Management.Web.Components;
using MudBlazor.Services;
using Refit;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpContextAccessor();
builder.Services.AddControllersWithViews();
ApiSetting apiSettings = builder.Configuration.GetSection(nameof(ApiSetting))
                                                     .Get<ApiSetting>()!;

builder.Services.AddRefitClient<IEmployeeSdk>()
    .ConfigureHttpClient(c => c.BaseAddress = new Uri(apiSettings.UrlEmployeesApi));
//.AddHttpMessageHandler(sp => sp.GetRequiredService<TokenValidatorMessageHandler>()); ToDo: Add TokenValidator

builder.Services.AddRefitClient<IUserSdk>()
    .ConfigureHttpClient(c => c.BaseAddress = new Uri(apiSettings.UrlUsersApi));

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveWebAssemblyComponents();

builder.Services.AddMudServices(); // Add MudBlazor services to the services collection
var app = builder.Build();

void ConfigureServices(IServiceCollection services)
{
    services.AddSweetAlert2();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();


app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveWebAssemblyRenderMode();
    

app.Run();
