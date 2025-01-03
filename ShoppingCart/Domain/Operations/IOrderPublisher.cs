using ShoppingCart.Domain.Repositories;
using ShoppingCart.Domain.Workflow;

namespace ShoppingCart.Domain.Operations
{
    public interface IOrderPublisher
    {
        void Publish(OrderPlacedEvent orderPlacedEvent);
    }
}
