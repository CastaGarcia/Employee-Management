using Employees.Management.Data;
using Employees.Management.Data.Repos;
using Employees.Management.Services;
using Employees.Management.Services.Employees;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Registrar servicios para Swagger
builder.Services.AddEndpointsApiExplorer(); // Para explorar los endpoints
builder.Services.AddSwaggerGen(); // Para generar la documentación Swagger

// Add services to the container.

builder.Services.AddControllers();

string? connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseNpgsql(connectionString);

});


//Automapper
builder.Services.AddAutoMapper(typeof(DummyMarker).Assembly);
builder.Services.AddHttpContextAccessor();

//Repositorios
builder.Services.AddScoped<IEmployeeRepo, EmployeeRepo>();
builder.Services.AddScoped<IEmployeeServices, EmployeeServices>();
builder.Services.AddScoped<IUserRepo, UserRepo>();


// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();


var app = builder.Build();

var sp = app.Services.CreateScope().ServiceProvider;
var context = sp.GetService<AppDbContext>();
if (context?.Database.ProviderName != "Microsoft.EntityFrameworkCore.InMemory")
    context?.Database.Migrate();

// Activar Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    
    app.MapGet("/", () => Results.Redirect("/swagger"));
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
