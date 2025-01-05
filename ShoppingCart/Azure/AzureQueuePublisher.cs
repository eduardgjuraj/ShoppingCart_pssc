using Azure.Messaging.ServiceBus;
using System.Text.Json;
using System.Threading.Tasks;

public class AzureQueuePublisher
{
    private readonly ServiceBusClient _serviceBusClient;

    public AzureQueuePublisher(ServiceBusClient serviceBusClient)
    {
        _serviceBusClient = serviceBusClient;
    }

    public async Task PublishMessageAsync<T>(string queueName, T message)
    {
        var sender = _serviceBusClient.CreateSender(queueName);

        try
        {
            var messageBody = JsonSerializer.Serialize(message);

            var serviceBusMessage = new ServiceBusMessage(messageBody);

            await sender.SendMessageAsync(serviceBusMessage);

            Console.WriteLine($"Message sent to queue '{queueName}': {messageBody}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error sending message to queue '{queueName}': {ex.Message}");
            throw;
        }
        finally
        {
            await sender.DisposeAsync();
        }
    }
}
