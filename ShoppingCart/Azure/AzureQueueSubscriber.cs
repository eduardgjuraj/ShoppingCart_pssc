using Azure.Messaging.ServiceBus;
using ShoppingCart.Domain.Repositories;
using System.Text.Json;

public class AzureQueueSubscriber
{
    private readonly ServiceBusClient _client;

    public AzureQueueSubscriber(ServiceBusClient client)
    {
        _client = client;
    }

    public async Task SubscribeAsync(string queueName, Func<OrderPlacedEvent, Task> onMessageReceived)
    {
        var processor = _client.CreateProcessor(queueName, new ServiceBusProcessorOptions());

        processor.ProcessMessageAsync += async args =>
        {
            var messageBody = args.Message.Body.ToString();
            var orderPlacedEvent = JsonSerializer.Deserialize<OrderPlacedEvent>(messageBody);

            if (orderPlacedEvent != null)
            {
                await onMessageReceived(orderPlacedEvent);
            }

            await args.CompleteMessageAsync(args.Message);
        };

        processor.ProcessErrorAsync += async args =>
        {
            Console.WriteLine($"Error: {args.Exception.Message}");
        };

        await processor.StartProcessingAsync();
    }
}
