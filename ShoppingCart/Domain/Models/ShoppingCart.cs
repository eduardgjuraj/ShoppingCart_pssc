using System;
using System.Collections.Generic;

namespace ShoppingCart.Domain.Models
{
    public static class ShoppingCart
    {
        public interface IShoppingCart { }

        public record UnvalidatedShoppingCart(string UserId, IReadOnlyCollection<string> ItemIds) : IShoppingCart;

        public record ActiveShoppingCart(Guid Id, string UserId, IReadOnlyCollection<CartItem> Items) : IShoppingCart;

        public record ValidatedShoppingCart(Guid Id, string UserId, IReadOnlyCollection<CartItem> Items, decimal TotalAmount) : IShoppingCart;

        public record CheckedOutShoppingCart(Guid Id, string UserId, IReadOnlyCollection<CartItem> Items, decimal TotalAmount, DateTime CheckedOutDate) : IShoppingCart;
    }
}
