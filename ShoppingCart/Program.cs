using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ShoppingCart.Data;
using ShoppingCart.Data.Models;
using ShoppingCart.Data.Repositories;

class Program
{
    static async Task Main(string[] args)
    {
        // Configure DbContext
        var options = new DbContextOptionsBuilder<ShoppingCartDbContext>()
            .UseSqlServer("Server=localhost;Database=ShoppingCartDb;Trusted_Connection=True;Encrypt=True;TrustServerCertificate=True;")
            .Options;

        using var context = new ShoppingCartDbContext(options);
        var shoppingCartRepo = new ShoppingCartRepository(context);
        var shoppingCart = new ShoppingCartDto
        {
            Id = Guid.NewGuid(),
            UserId = "USERLALALALALA",
            CreatedAt = DateTime.UtcNow
        };
        await shoppingCartRepo.AddAsync(shoppingCart);


        // Confirm the user was added
        var addedCart = await context.ShoppingCarts.FirstOrDefaultAsync(sc => sc.UserId == "BOFA_TEST");

        if (addedCart != null)
        {
            Console.WriteLine($"User BOFA_TEST added successfully: ID = {addedCart.Id}, CreatedAt = {addedCart.CreatedAt}");
        }
        else
        {
            Console.WriteLine("Failed to add user BOFA_TEST.");
        }
    }
}
