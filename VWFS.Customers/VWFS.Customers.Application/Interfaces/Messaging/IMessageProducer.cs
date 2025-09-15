namespace VWFS.Customers.Application.Interfaces.Messaging
{
    public interface IMessageProducer
    {
        Task PublishAsync<T>(string topic, T message);
    }
}
