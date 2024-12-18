using System.Collections.Generic;

namespace ShoppingCart.Domain.Models
{
    public record UnvalidatedShoppingCart(string UserId, IReadOnlyCollection<string> ItemIds) : IShoppingCart;
}
