using System;
using MediatR;
using Microsoft.Extensions.Logging;
using VWFS.Customers.Application.Interfaces;
using VWFS.Customers.Application.Queries;
using VWFS.Customers.Domain.Entities;

namespace VWFS.Customers.Application.Handlers
{
    public class GetCustomerByIdQueryHandler : IRequestHandler<GetCustomerByIdQuery, Customer?>
    {
        private readonly ICustomerRepository _repository;
        private readonly ILogger<GetCustomerByIdQueryHandler> _logger;

        public GetCustomerByIdQueryHandler(ICustomerRepository repository, ILogger<GetCustomerByIdQueryHandler> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<Customer?> Handle(GetCustomerByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var customer = await _repository.GetByIdAsync(request.Id);
                
                if (customer == null)
                {
                    throw new ApplicationException("Customer not found.");
                }
                _logger.LogInformation($"{customer} found successfully.");

                return customer;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw;
            }
        }
    }
}
