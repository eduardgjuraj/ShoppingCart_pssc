using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ShoppingCart.Data;
using ShoppingCart.Data.Repositories;
using ShoppingCart.Domain.Operations;
using ShoppingCart.Domain.Repositories;
using ShoppingCart.Domain.Workflows;



var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<ShoppingCartDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ShoppingCartDatabase")));

var connectionString = builder.Configuration.GetConnectionString("ShoppingCartDatabase");
Console.WriteLine($"Connection String: {connectionString}");

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer(); // Required for Swagger
builder.Services.AddSwaggerGen();           // Registers Swagger generator

builder.Services.AddDbContext<ShoppingCartDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ShoppingCartDatabase")));
builder.Services.AddScoped<ICartRepository, CartRepository>();
builder.Services.AddScoped<OrderPlacedWorkflow>();
builder.Services.AddScoped<IOrderPublisher, OrderPublisher>();

var app = builder.Build();

// Enable Swagger in development and production
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Example API v1");
    c.RoutePrefix = "";
});

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

app.Run();
