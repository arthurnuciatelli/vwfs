using Confluent.Kafka;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace VWFS.Customers.Infrastructure.Messaging;

public class KafkaProducer
{
    private readonly IProducer<Null, string> _producer;
    private readonly ILogger<KafkaProducer> _logger;

    public KafkaProducer(string bootstrapServers, ILogger<KafkaProducer> logger)
    {
        _logger = logger;
        _logger.LogInformation($"Address: {bootstrapServers}");
        var config = new ProducerConfig { BootstrapServers = bootstrapServers };
        _producer = new ProducerBuilder<Null, string>(config).Build();
    }

    public async Task PublishAsync<T>(string topic, T message)
    {
        var value = JsonSerializer.Serialize(message);
        _logger.LogInformation(value);
        await _producer.ProduceAsync(topic, new Message<Null, string> { Value = value });
    }
}
