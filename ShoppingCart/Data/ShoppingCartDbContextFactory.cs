using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using ShoppingCart.Data;

public class ShoppingCartDbContextFactory : IDesignTimeDbContextFactory<ShoppingCartDbContext>
{
    public ShoppingCartDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<ShoppingCartDbContext>();
        optionsBuilder.UseSqlServer("Server=localhost;Database=ShoppingCartDb;Trusted_Connection=True;Encrypt=True;TrustServerCertificate=True;");

        return new ShoppingCartDbContext(optionsBuilder.Options);
    }
}
