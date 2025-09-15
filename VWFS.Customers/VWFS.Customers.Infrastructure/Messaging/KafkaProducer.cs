using Confluent.Kafka;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using VWFS.Customers.Application.Interfaces.Messaging;
using Microsoft.Extensions.Configuration;

namespace VWFS.Customers.Infrastructure.Messaging;

public class KafkaProducer : IMessageProducer
{
    private readonly IProducer<Null, string> _producer;
    private readonly ILogger<KafkaProducer> _logger;
    private readonly IConfiguration _config;

    public KafkaProducer(IConfiguration config, ILogger<KafkaProducer> logger)
    {
        _logger = logger;
        var _config = config;
        var producerConfig = new ProducerConfig { BootstrapServers = _config["KAFKA_BOOTSTRAP_SERVERS"] };
        _producer = new ProducerBuilder<Null, string>(producerConfig).Build();
    }

    public async Task PublishAsync<T>(string topic, T message)
    {
        var value = JsonSerializer.Serialize(message);
        _logger.LogInformation(value);
        await _producer.ProduceAsync(topic, new Message<Null, string> { Value = value });
    }
}
