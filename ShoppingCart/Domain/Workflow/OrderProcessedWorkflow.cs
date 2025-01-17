using Azure.Messaging.ServiceBus;
using System.Text.Json;
using ShoppingCart.Domain.Models;
public class OrderProcessedWorkflow
{
    private readonly AzureQueuePublisher _queuePublisher;

    public OrderProcessedWorkflow(AzureQueuePublisher queuePublisher)
    {
        _queuePublisher = queuePublisher;
    }

    public async Task ProcessOrderAsync(CheckedOutShoppingCart checkedOutCart)
    {
        
        Console.WriteLine($"Processing Order: {checkedOutCart.Id}");
       
        await _queuePublisher.PublishMessageAsync("OrderProcessedQueue", checkedOutCart);

        Console.WriteLine($"Order published to Azure Service Bus: {checkedOutCart.Id}");

    }
}
