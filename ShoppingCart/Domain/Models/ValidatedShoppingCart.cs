using ShoppingCart.Domain.ValueObjects;

namespace ShoppingCart.Domain.Models
{
    public record ValidatedShoppingCart(
        Guid Id,
        string UserId,
        IReadOnlyCollection<CartItem> Items,
        Money TotalAmount
    ) : IShoppingCart;
}
