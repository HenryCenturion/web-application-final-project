using dtaquito_backend_web_app.Users.Domain.Services;
using dtaquito_backend_web_app.Users.Domain.Repositories;
using dtaquito_backend_web_app.Shared.Infrastructure.Persistance.EFC.Configuration;
using dtaquito_backend_web_app.SportSpaces.Application.CommandServices;
using dtaquito_backend_web_app.SportSpaces.Application.QueryServices;
using dtaquito_backend_web_app.SportSpaces.Domain.Repositories;
using dtaquito_backend_web_app.SportSpaces.Domain.Services;
using dtaquito_backend_web_app.SportSpaces.Infrastructure.Persistance.EFC.Repositories;
using Microsoft.EntityFrameworkCore;
using dtaquito_backend_web_app.Users.Infrastructure.Persistance.EFC.Repositories;
using dtaquito_backend_web_app.Shared.Interfaces.ASP.Configuration;
using System.Text.Json.Serialization;
using dtaquito_backend_web_app.Payments.Application.CommandServices;
using dtaquito_backend_web_app.Payments.Application.QueryServices;
using dtaquito_backend_web_app.Payments.Domain.Repositories;
using dtaquito_backend_web_app.Payments.Domain.Services;
using dtaquito_backend_web_app.Payments.Infrastructure.Persistance.EFC.Repositories;
using dtaquito_backend_web_app.Reservations.Application.CommandServices;
using dtaquito_backend_web_app.Reservations.Application.QueryServices;
using dtaquito_backend_web_app.Reservations.Domain.Repositories;
using dtaquito_backend_web_app.Reservations.Domain.Services;
using dtaquito_backend_web_app.Reservations.Infrastructure.Persistance.EFC.Repositories;
using dtaquito_backend_web_app.Users.Application.CommandServices;
using dtaquito_backend_web_app.Shared.Domain.Repositories;
using dtaquito_backend_web_app.Shared.Infrastructure.Persistance.EFC.Repositories;
using dtaquito_backend_web_app.Suscriptions.Application.CommandServices;
using dtaquito_backend_web_app.Suscriptions.Application.QueryServices;
using dtaquito_backend_web_app.Suscriptions.Domain.Repositories;
using dtaquito_backend_web_app.Suscriptions.Domain.Services;
using dtaquito_backend_web_app.Suscriptions.Infrastructure.Persistance.EFC.Repositories;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers(options =>
{
    options.Conventions.Add(new KebabCaseRouteNamingConvention());
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<AppDBContext>(
    options =>
    {
        if (connectionString != null)
            if (builder.Environment.IsDevelopment())
                options.UseMySQL(connectionString)
                    .LogTo(Console.WriteLine, LogLevel.Information)
                    .EnableSensitiveDataLogging()
                    .EnableDetailedErrors();
            else if (builder.Environment.IsProduction())
                options.UseMySQL(connectionString)
                .LogTo(Console.WriteLine, LogLevel.Error)
                .EnableSensitiveDataLogging()
                .EnableDetailedErrors();
    });

builder.Services.AddRouting(options => options.LowercaseUrls = true);

builder.Services.AddControllers(options =>
{
    options.Conventions.Add(new KebabCaseRouteNamingConvention());
});

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

builder.Services.AddControllers().AddNewtonsoftJson(options =>
{
    options.SerializerSettings.Converters.Add(new StringEnumConverter());
});

builder.Services.AddControllers().AddNewtonsoftJson(options =>
{
    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
});

// Add CORS services
builder.Services.AddCors(options =>
{
    options.AddPolicy("MyPolicy",
        builder =>
        {
            builder.WithOrigins("https://dtaquitoupc.netlify.app", "http://localhost:8080", "http://localhost:3000", "http://localhost:4200", "https://dtaquito-backend-web-app.azurewebsites.net",
                "http://localhost:5173", "https://main--dtaquitoo.netlify.app", "https://datacazo.netlify.app")
                   .AllowAnyHeader()
                   .AllowAnyMethod()
                   .AllowCredentials();
        });
});

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddScoped<IUserCommandService, UserCommandService>();
builder.Services.AddScoped<IUserQueryService, UserQueryService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ISportSpacesRepository, SportSpacesRepository>();
builder.Services.AddScoped<ISportSpacesCommandService, SportSpacesCommandService>();
builder.Services.AddScoped<ISportSpacesQueryService, SportSpacesQueryService>();
builder.Services.AddScoped<ISuscriptionCommandService, SuscriptionCommandService>();
builder.Services.AddScoped<ISuscriptionQueryService, SuscriptionQueryService>();
builder.Services.AddScoped<ISuscriptionRepository, SuscriptionRepository>();
builder.Services.AddScoped<IPaymentCommandService, PaymentCommandService>();
builder.Services.AddScoped<IPaymentQueryService, PaymentQueryService>();
builder.Services.AddScoped<IPaymentRepository, PaymentRepository>();
builder.Services.AddScoped<IReservationRepository, ReservationRepository>();
builder.Services.AddScoped<IReservationCommandService, ReservationCommandService>();
builder.Services.AddScoped<IReservationQueryService, ReservationQueryService>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<AppDBContext>();
    context.Database.EnsureCreated();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();

app.UseCors("MyPolicy"); // Use CORS policy

app.UseAuthorization();

app.MapControllers();

app.Run();