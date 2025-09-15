using System;
using MediatR;
using Microsoft.Extensions.Logging;
using VWFS.Customers.Application.Commands;
using VWFS.Customers.Application.Interfaces;

namespace VWFS.Customers.Application.Handlers
{
    public class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand>
    {
        private readonly ICustomerRepository _repository;
        private readonly ILogger<CreateCustomerCommandHandler> _logger;

        public UpdateCustomerCommandHandler(ICustomerRepository repository, ILogger<CreateCustomerCommandHandler> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var customer = await _repository.GetByIdAsync(request.Id);
                if (customer == null)
                {
                    throw new ApplicationException("Customer not found.");
                }

                customer.Name = request.Name;
                customer.BirthOrFoundationDate = request.BirthOrFoundationDate;

                if (!customer.IsLegalAge())
                { 
                    throw new ApplicationException("Customer must be of legal age.");
                }

                await _repository.UpdateAsync(customer);
                 _logger.LogInformation($"{customer} updated successfully.");
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);    
                throw;
            }
        }
    }
}
