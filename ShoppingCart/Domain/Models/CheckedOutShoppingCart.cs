using ShoppingCart.Domain.ValueObjects;

namespace ShoppingCart.Domain.Models
{
    public record CheckedOutShoppingCart(
        Guid Id,
        string UserId,
        IReadOnlyCollection<CartItem> Items,
        Money TotalAmount,
        DateTime CheckedOutDate,
        Address ShippingAddress
    ) : IShoppingCart;
}
