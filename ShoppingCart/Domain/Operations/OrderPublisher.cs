using ShoppingCart.Domain.Repositories;

namespace ShoppingCart.Domain.Operations
{
    public class OrderPublisher : IOrderPublisher
    {
        public void Publish(OrderPlacedEvent orderPlacedEvent)
        {
            // Example: Log the event to the console or integrate with a message broker
            Console.WriteLine($"Order placed: {orderPlacedEvent.OrderId}, Total: {orderPlacedEvent.TotalAmount}");
        }
    }
}
