using System;
using System.Collections.Generic;
using ShoppingCart.Domain.ValueObjects;
using static ShoppingCart.Domain.Repositories.OrderPlacedEvent;

namespace ShoppingCart.Domain.Repositories
{
    public record OrderPlacedEvent(
        Guid OrderId,
        string UserId,
        IReadOnlyCollection<OrderItem> Items,
        Money TotalAmount,
        DateTime CheckedOutDate,
        Address ShippingAddress)
    {
        public record OrderItem(Guid ProductId, int Quantity, Money TotalPrice);
    }
}
