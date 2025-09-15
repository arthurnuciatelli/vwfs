using System;
using MediatR;
using Microsoft.Extensions.Logging;
using VWFS.Customers.Application.Commands;
using VWFS.Customers.Application.Interfaces;

namespace VWFS.Customers.Application.Handlers
{
    public class DeleteCustomerCommandHandler : IRequestHandler<DeleteCustomerCommand>
    {

        private readonly ICustomerRepository _repository;
        private readonly ILogger<DeleteCustomerCommandHandler> _logger;

        public DeleteCustomerCommandHandler(ICustomerRepository repository, ILogger<DeleteCustomerCommandHandler> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var customer = await _repository.GetByIdAsync(request.Id);
                
                if (customer == null)
                {
                    throw new ApplicationException("Customer not found.");
                }
                
                await _repository.DeleteAsync(request.Id);

                _logger.LogInformation($"{customer} deleted successfully.");
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw;
            }

        }
    }
}
