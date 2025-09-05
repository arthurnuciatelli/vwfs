
using Confluent.Kafka;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace VWFS.BuildingBlocks.Infrastructure;

public class KafkaProducer : IDisposable
{
    private readonly IProducer<Null, string> _producer;

    public KafkaProducer(string broker)
    {
        var config = new ProducerConfig { BootstrapServers = broker };
        _producer = new ProducerBuilder<Null, string>(config).Build();
    }

    public async Task PublishAsync<T>(string topic, T message)
    {
        var value = JsonSerializer.Serialize(message);
        await _producer.ProduceAsync(topic, new Message<Null, string> { Value = value });
    }

    public void Dispose() => _producer.Dispose();
}
