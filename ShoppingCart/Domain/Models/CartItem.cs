using ShoppingCart.Domain.ValueObjects;

namespace ShoppingCart.Domain.Models
{
    public record CartItem(Guid ProductId, int Quantity, ProductDetails Details)
    {
        public Money TotalPrice => Details.Price with { Amount = Details.Price.Amount * Quantity };
    }
}
