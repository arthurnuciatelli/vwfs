using MediatR;
using Microsoft.Extensions.Logging;
using VWFS.BuildingBlocks.Contracts;
using VWFS.Customers.Application.Commands;
using VWFS.Customers.Domain.Entities;
using VWFS.Customers.Domain.Interfaces;
using VWFS.Customers.Infrastructure.Messaging;

namespace VWFS.Customers.Application.Handlers
{
    public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, Guid>
    {
        private readonly ICustomerRepository _repository;
        private readonly KafkaProducer _kafkaProducer;
        private readonly ILogger<CreateCustomerCommandHandler> _logger;

        public CreateCustomerCommandHandler(ICustomerRepository repository, KafkaProducer kafkaProducer, ILogger<CreateCustomerCommandHandler> logger)
        {
            _repository = repository;
            _kafkaProducer = kafkaProducer;
            _logger = logger;
        }

        public async Task<Guid> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            // Cria o customer com validação de CPF/CNPJ e idade
            var customer = new Customer(
                request.Name,
                request.Document,
                request.Type
            );

            // Persiste no repositório
            await _repository.AddAsync(customer);

            // Publica evento no Kafka
            var customerCreatedEvent = new CustomerCreatedEvent(
                customer.Id,
                customer.Name,
                customer.Document,
                customer.Type.ToString()
            );

            try
            {
                _logger.LogInformation($"{customerCreatedEvent}");
                await _kafkaProducer.PublishAsync("customer-created", customerCreatedEvent);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return customer.Id;
        }

    }
}
