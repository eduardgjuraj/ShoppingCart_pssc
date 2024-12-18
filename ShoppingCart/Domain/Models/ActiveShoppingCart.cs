using System;
using System.Collections.Generic;

namespace ShoppingCart.Domain.Models
{
    public record ActiveShoppingCart(Guid Id, string UserId, IReadOnlyCollection<CartItem> Items) : IShoppingCart;
}
