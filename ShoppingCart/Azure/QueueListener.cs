public class QueueListener
{
    private readonly AzureQueueSubscriber _subscriber;

    public QueueListener(AzureQueueSubscriber subscriber)
    {
        _subscriber = subscriber;
    }

    public async Task StartListening()
    {
        await _subscriber.SubscribeAsync("OrderProcessedQueue", async orderPlacedEvent =>
        {
            Console.WriteLine($"Processing order: {orderPlacedEvent.OrderId}");
            Console.WriteLine($"User: {orderPlacedEvent.UserId}");
            Console.WriteLine($"Total: {orderPlacedEvent.TotalAmount}");
        });
    }
}
