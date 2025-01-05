using Azure.Messaging.ServiceBus;
using System.Text.Json;
using ShoppingCart.Domain.Models;
public class OrderProcessedQueueSubscriber
{
    private readonly ServiceBusClient _serviceBusClient;
    private readonly OrderProcessedWorkflow _orderProcessedWorkflow;

    public OrderProcessedQueueSubscriber(ServiceBusClient serviceBusClient, OrderProcessedWorkflow orderProcessedWorkflow)
    {
        _serviceBusClient = serviceBusClient;
        _orderProcessedWorkflow = orderProcessedWorkflow;
    }

    public async Task StartListeningAsync(string queueName)
    {
        var processor = _serviceBusClient.CreateProcessor(queueName, new ServiceBusProcessorOptions());

        processor.ProcessMessageAsync += async args =>
        {
            var messageBody = args.Message.Body.ToString();
            var checkedOutCart = JsonSerializer.Deserialize<CheckedOutShoppingCart>(messageBody);

            if (checkedOutCart != null)
            {
                await _orderProcessedWorkflow.ProcessOrderAsync(checkedOutCart);
            }

            await args.CompleteMessageAsync(args.Message);
        };

        processor.ProcessErrorAsync += args =>
        {
            Console.WriteLine($"Error: {args.Exception.Message}");
            return Task.CompletedTask;
        };

        await processor.StartProcessingAsync();
    }
}
