using Microsoft.EntityFrameworkCore;
using ShoppingCart.Data.Models;
using ShoppingCart.Domain.Models;
using ShoppingCart.Domain.Repositories;
using ShoppingCart.Domain.ValueObjects;
using System;
using System.Linq;

namespace ShoppingCart.Data.Repositories
{
    public class CartRepository : ICartRepository
    {
        private readonly ShoppingCartDbContext _context;

        public CartRepository(ShoppingCartDbContext context)
        {
            _context = context;
        }

        public ActiveShoppingCart GetActiveCartById(Guid cartId)
        {
            var cart = _context.ShoppingCarts
                .FirstOrDefault(sc => sc.Id == cartId && !sc.IsCheckedOut);

            if (cart == null)
                return null;

            var cartItems = _context.CartItems
                .Where(ci => ci.ShoppingCartId == cartId)
                .Select(item => new CartItem(
                    item.Id,
                    item.Quantity,
                    new ProductDetails(
                        item.ProductName,
                        item.ProductDescription,
                        new Money(item.PricePerUnit, "USD")
                    )
                ))
                .ToList();

            return new ActiveShoppingCart(
                cart.Id,
                cart.UserId,
                cartItems
            );
        }

        public void UpdateCartToCheckedOut(CheckedOutShoppingCart checkedOutCart)
        {
            // Save to CheckedOutShoppingCartDto table
            var checkedOutCartDto = new CheckedOutShoppingCartDto
            {
                Id = checkedOutCart.Id,
                UserId = checkedOutCart.UserId,
                TotalAmount = checkedOutCart.TotalAmount.Amount,
                CheckedOutDate = checkedOutCart.CheckedOutDate,
                ShippingAddress = checkedOutCart.ShippingAddress.ToString()
            };

            _context.CheckedOutShoppingCarts.Add(checkedOutCartDto);

            // Mark the shopping cart as checked out
            var cart = _context.ShoppingCarts.FirstOrDefault(sc => sc.Id == checkedOutCart.Id);
            if (cart != null)
            {
                cart.IsCheckedOut = true;
            }

            _context.SaveChanges();
        }
    }
}
