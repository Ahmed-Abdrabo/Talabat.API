using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;
using Talabat.API.Errors;
using Talabat.API.Extensions;
using Talabat.API.Helpers;
using Talabat.API.Middlwares;
using Talabat.Core.Data;
using Talabat.Core.Entities.Identity;
using Talabat.Core.Repositories.Contract;
using Talabat.Repository;
using Talabat.Repository.Data;
using Talabat.Repository.Identity;
using Talabat.Repository.Identity.DataSeed;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<StoreContext>(option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddDbContext<AppIdentityDbContext>(option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("IdentityConnection"));
});

builder.Services.AddSingleton<IConnectionMultiplexer>((serviceProvider) =>
{
    var connection = builder.Configuration.GetConnectionString("Redis");
    return ConnectionMultiplexer.Connect(connection);
});

builder.Services.AddApplicationServices();

builder.Services.AddIdentityServices(builder.Configuration); 

var app = builder.Build();


using var scope = app.Services.CreateScope();

var services = scope.ServiceProvider;

var _dbContext = services.GetRequiredService<StoreContext>();
var _identityDbContext = services.GetRequiredService<AppIdentityDbContext>();

var loggerFactory = services.GetRequiredService<ILoggerFactory>();

try
{
	await _dbContext.Database.MigrateAsync();
    await StoreContextSeed.SeedAsync(_dbContext);

    var _userManager = services.GetRequiredService<UserManager<AppUser>>();
    await AppIdentityDbContextSeed.SeedUserAsync(_userManager);

    await _identityDbContext.Database.MigrateAsync();
}
catch (Exception ex)
{
    var logger = loggerFactory.CreateLogger<Program>();
    logger.LogError(ex, "an error has been accured during apply the application");  
}

app.UseMiddleware<ExceptionMiddlware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseStatusCodePagesWithReExecute("/errors/{0}");

app.UseHttpsRedirection();

app.UseStaticFiles();

app.MapControllers();

app.UseAuthentication();

app.UseAuthorization();

app.Run();
