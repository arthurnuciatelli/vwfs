using MediatR;
using Microsoft.Extensions.Logging;
using VWFS.BuildingBlocks.Contracts;
using VWFS.Customers.Application.Commands;
using VWFS.Customers.Application.Interfaces;
using VWFS.Customers.Application.Interfaces.Messaging;
using VWFS.Customers.Domain.Entities;
namespace VWFS.Customers.Application.Handlers
{
    public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, Guid>
    {
        private readonly ICustomerRepository _repository;
        private readonly IMessageProducer _messageProducer;
        private readonly ILogger<CreateCustomerCommandHandler> _logger;

        public CreateCustomerCommandHandler(ICustomerRepository repository, IMessageProducer messageProducer, ILogger<CreateCustomerCommandHandler> logger)
        {
            _repository = repository;
            _messageProducer = messageProducer;
            _logger = logger;
        }

        public async Task<Guid> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var existingCustomer = await _repository.GetByDocumentAsync(request.Document);
                if (existingCustomer != null)
                {
                    throw new ApplicationException("A customer with the same document already exists.");
                }

                var customer = new Customer(
                    request.Name,
                    request.Document,
                    request.Type,
                    request.BirthOrFoundationDate
                );

                if (!customer.IsLegalAge())
                {
                    throw new ApplicationException("Customer must be of legal age.");
                }

                await _repository.AddAsync(customer);
                // Publica evento no Kafka
                var customerCreatedEvent = new CustomerCreatedEvent(
                    customer.Id,
                    customer.Name,
                    customer.Document,
                    customer.Type.ToString()
                );

                _logger.LogInformation($"{customerCreatedEvent}");
                await _messageProducer.PublishAsync("customer-created", customerCreatedEvent);

                return customer.Id;
            }
            catch (ApplicationException)
            {
                throw;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw;
            }
        }
    }
}
