using System;
using Azure.Messaging.ServiceBus;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ShoppingCart.Data;
using ShoppingCart.Data.Repositories;
using ShoppingCart.Domain.Operations;
using ShoppingCart.Domain.Repositories;
using ShoppingCart.Domain.ValueObjects;
using ShoppingCart.Domain.Workflows;



var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<ShoppingCartDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ShoppingCartDatabase")));

var connectionString = builder.Configuration.GetConnectionString("ShoppingCartDatabase");
Console.WriteLine($"Connection String: {connectionString}");


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer(); // Required for Swagger
builder.Services.AddSwaggerGen();           // Registers Swagger generator

builder.Services.AddDbContext<ShoppingCartDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ShoppingCartDatabase")));
builder.Services.AddScoped<ICartRepository, CartRepository>();
builder.Services.AddScoped<OrderPlacedWorkflow>();
builder.Services.AddScoped<IOrderPublisher, OrderPublisher>();
builder.Services.AddScoped<OrderProcessedWorkflow>();

// azure service bus
builder.Services.AddSingleton(provider =>
{
    var configuration = provider.GetRequiredService<IConfiguration>();
    var connectionString = configuration.GetConnectionString("ServiceBus");
    return new ServiceBusClient(connectionString);
});

builder.Services.AddScoped<AzureQueueSubscriber>();
builder.Services.AddScoped<AzureQueuePublisher>();
builder.Services.AddSingleton<OrderProcessedQueueSubscriber>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var subscriber = scope.ServiceProvider.GetRequiredService<OrderProcessedQueueSubscriber>();
    await subscriber.StartListeningAsync("OrderProcessedQueue");
}

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
