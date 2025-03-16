using Employees.Management.Api;
using Employees.Management.Data;
using Employees.Management.Data.Repos;
using Employees.Management.Services;
using Employees.Management.Services.Employees;
using Employees.Management.Services.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Serilog;

Log.Logger = new LoggerConfiguration()
    .WriteTo.File("log.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();
try
{
    var builder = WebApplication.CreateBuilder(args);
    builder.Services.AddSerilog(); // <-- Add this line


    // Swagger
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen(options =>
    {
        //define the Security for authentication
        options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            Name = "Authorization",
            Type = SecuritySchemeType.Http,
            Scheme = "Bearer",
            BearerFormat = "JWT",
            In = ParameterLocation.Header,
            Description = "JWT Authorization Header using Bearer Scheme"
        });

        options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[]{}
        }
    });
    });



    // Add services to the container.

    builder.Services.AddControllers();

    string? connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    builder.Services.AddDbContext<AppDbContext>(options =>
    {
        options.UseNpgsql(connectionString);

    });

    //Add service of JWT Autorization
    builder.Services.AddJwtTokenService(builder.Configuration);

    
    //Automapper
    builder.Services.AddAutoMapper(typeof(DummyMarker).Assembly);
    builder.Services.AddHttpContextAccessor();

    //Repositorios
    builder.Services.AddScoped<IEmployeeRepo, EmployeeRepo>();
    builder.Services.AddScoped<IEmployeeServices, EmployeeServices>();
    builder.Services.AddScoped<IUserRepo, UserRepo>();
    builder.Services.AddScoped<IUserServices, UserServices>();   

    builder.Services.AddCors(options =>
    {
        options.AddPolicy("AllowAllOrigins", policy =>
        {
            policy.AllowAnyOrigin() // Allows all origins
                  .AllowAnyMethod()  // Allows all HTTP methods (GET, POST, PUT, DELETE, etc.)
                  .AllowAnyHeader(); // Allows all headers
        });

        // If you want to restrict it to certain origins, use this instead:
        // options.AddPolicy("AllowSpecificOrigins", policy =>
        // {
        //     policy.WithOrigins("https://example.com", "https://anotherdomain.com") 
        //           .AllowAnyMethod()
        //           .AllowAnyHeader();
        // });
    });

    // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
    builder.Services.AddOpenApi();


    var app = builder.Build();

    app.UseSerilogRequestLogging(); // <-- Add this line

    //Create DB each time it run
    var sp = app.Services.CreateScope().ServiceProvider;
    var context = sp.GetService<AppDbContext>();
    if (context?.Database.ProviderName != "Microsoft.EntityFrameworkCore.InMemory")
        context?.Database.Migrate();

    // Active Swagger
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();

        app.MapGet("/", () => Results.Redirect("/swagger"));
    }

    // Enable CORS globally in the application
    app.UseCors("AllowAllOrigins"); // <-- Add this line

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.MapOpenApi();
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
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}



public partial class Program { } //to use Integration Test