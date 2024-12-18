using Microsoft.EntityFrameworkCore;
using ShoppingCart.Data.Models;

namespace ShoppingCart.Data
{
    public class ShoppingCartDbContext : DbContext
    {
        public ShoppingCartDbContext(DbContextOptions<ShoppingCartDbContext> options)
            : base(options)
        {
        }

        // DbSets for your tables
        public DbSet<ShoppingCartDto> ShoppingCarts { get; set; }
        public DbSet<CartItemDto> CartItems { get; set; }
        public DbSet<CheckedOutShoppingCartDto> CheckedOutShoppingCarts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure ShoppingCartDto table
            modelBuilder.Entity<ShoppingCartDto>(entity =>
            {
                entity.HasKey(sc => sc.Id); // Primary Key
                entity.Property(sc => sc.UserId)
                    .IsRequired()
                    .HasMaxLength(100); // UserId column constraints
                entity.Property(sc => sc.CreatedAt)
                    .HasDefaultValueSql("GETDATE()"); // Default value for CreatedAt
            });

            // Configure CartItemDto table
            modelBuilder.Entity<CartItemDto>(entity =>
            {
                entity.HasKey(ci => ci.Id); // Primary Key
                entity.Property(ci => ci.ShoppingCartId)
                    .IsRequired(); // Reference to ShoppingCartId
                entity.Property(ci => ci.ProductName)
                    .IsRequired()
                    .HasMaxLength(255); // ProductName constraints
                entity.Property(ci => ci.ProductDescription)
                    .HasMaxLength(1000); // ProductDescription constraints
                entity.Property(ci => ci.PricePerUnit)
                    .HasColumnType("decimal(18, 2)"); // Specify decimal precision
                entity.Property(ci => ci.Quantity)
                    .IsRequired(); // Quantity constraints
            });

            // Configure CheckedOutShoppingCartDto table
            modelBuilder.Entity<CheckedOutShoppingCartDto>(entity =>
            {
                entity.HasKey(cosc => cosc.Id); // Primary Key
                entity.Property(cosc => cosc.UserId)
                    .IsRequired()
                    .HasMaxLength(100); // UserId column constraints
                entity.Property(cosc => cosc.TotalAmount)
                    .HasColumnType("decimal(18, 2)"); // Specify decimal precision
                entity.Property(cosc => cosc.CheckedOutDate)
                    .HasDefaultValueSql("GETDATE()"); // Default value for CheckedOutDate
                entity.Property(cosc => cosc.ShippingAddress)
                    .IsRequired()
                    .HasMaxLength(1000); // ShippingAddress constraints
            });
        }
    }
}
