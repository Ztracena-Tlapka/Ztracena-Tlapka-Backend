using Microsoft.EntityFrameworkCore;
using Ztracena_Tlapka_Backend.Api.Middleware;
using Ztracena_Tlapka_Backend.Application.Common;
using Ztracena_Tlapka_Backend.Application.Interfaces;
using Ztracena_Tlapka_Backend.Application.Services;
using Ztracena_Tlapka_Backend.Infrastructure.Persistence;
using Ztracena_Tlapka_Backend.Infrastructure.Repositories;

DotNetEnv.Env.Load();

var connectionString = Environment.GetEnvironmentVariable("DB_STRING_CONNECTION")
    ?? throw new InvalidOperationException("DB_STRING_CONNECTION is not set in .env");

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString));

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();

var app = builder.Build();

app.UseMiddleware<ExceptionHandlingMiddleware>();

if (app.Environment.IsDevelopment())
    app.MapOpenApi();

app.MapControllers();

app.Run();
