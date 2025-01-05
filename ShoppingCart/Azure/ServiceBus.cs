public interface IServiceBusPublisher
{
    void Publish<T>(string topic, T message);
}
