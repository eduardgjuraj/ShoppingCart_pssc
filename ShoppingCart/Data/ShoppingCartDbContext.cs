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

        public DbSet<ShoppingCartDto> ShoppingCarts { get; set; }
        public DbSet<CartItemDto> CartItems { get; set; }
        public DbSet<CheckedOutShoppingCartDto> CheckedOutShoppingCarts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CartItemDto>(entity =>
            {
                entity.HasKey(ci => ci.Id);
                entity.Property(ci => ci.ProductName).IsRequired().HasMaxLength(255);
                entity.Property(ci => ci.ProductDescription).HasMaxLength(1000);
                entity.Property(ci => ci.PricePerUnit).HasColumnType("decimal(18, 2)");
                entity.HasOne<ShoppingCartDto>()
                      .WithMany()
                      .HasForeignKey(ci => ci.ShoppingCartId);
            });

            modelBuilder.Entity<ShoppingCartDto>(entity =>
            {
                entity.HasKey(sc => sc.Id);
                entity.Property(sc => sc.UserId).IsRequired().HasMaxLength(100);
                entity.Property(sc => sc.CreatedAt).HasDefaultValueSql("GETDATE()");
            });

            modelBuilder.Entity<CheckedOutShoppingCartDto>(entity =>
            {
                entity.HasKey(cosc => cosc.Id);
                entity.Property(cosc => cosc.UserId).IsRequired().HasMaxLength(100);
                entity.Property(cosc => cosc.TotalAmount).HasColumnType("decimal(18, 2)");
                entity.Property(cosc => cosc.ShippingAddress).HasMaxLength(1000);
            });
        }
    }
}
