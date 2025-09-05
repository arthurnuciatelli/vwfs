using System.Text.Json;
using Confluent.Kafka;
using VWFS.BuildingBlocks.Contracts;
using VWFS.Proposals.Domain.Services.ProposalService;
using VWFS.Proposals.Infrastructure.Persistence;

namespace VWFS.Proposals.Infrastructure.Messaging;

public class KafkaConsumerService : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IProposalService _proposalService;
    private readonly string _topic = "customer-created";
    private readonly string _broker = "kafka:9092";

    public KafkaConsumerService(IServiceProvider serviceProvider, IProposalService proposalService, IConfiguration configuration)
    {
        _serviceProvider = serviceProvider;
        _proposalService = proposalService;
        _broker = configuration["KAFKA_BOOTSTRAP_SERVERS"] ?? "kafka:9092";
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var config = new ConsumerConfig
        {
            BootstrapServers = _broker,
            GroupId = "proposal-service",
            AutoOffsetReset = AutoOffsetReset.Earliest
        };

        using var consumer = new ConsumerBuilder<Ignore, string>(config).Build();
        consumer.Subscribe(_topic);

        try
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var cr = consumer.Consume(stoppingToken);

                var customerEvent = JsonSerializer.Deserialize<CustomerCreatedEvent>(cr.Message.Value);

                if (customerEvent != null)
                {
                    var proposal = _proposalService.GerarProposta(customerEvent);

                    using var scope = _serviceProvider.CreateScope();
                    var repository = scope.ServiceProvider.GetRequiredService<IProposalRepository>();

                    await repository.AddAsync(proposal);
                }
            }
        }
        catch (OperationCanceledException)
        {
            consumer.Close();
        }
    }
}
