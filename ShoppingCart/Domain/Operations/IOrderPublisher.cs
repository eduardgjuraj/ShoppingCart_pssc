using ShoppingCart.Domain.Repositories;

namespace ShoppingCart.Domain.Operations
{
    public interface IOrderPublisher
    {
        void Publish(OrderPlacedEvent orderPlacedEvent);
    }
}
