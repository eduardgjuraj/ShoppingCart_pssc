using ShoppingCart.Domain.Repositories;

namespace ShoppingCart.Domain.Operations
{
    public class OrderPublisher : IOrderPublisher
    {
        public void Publish(OrderPlacedEvent orderPlacedEvent)
        {
            Console.WriteLine($"Order placed: {orderPlacedEvent.OrderId}, Total: {orderPlacedEvent.TotalAmount}");
        }
    }
}
