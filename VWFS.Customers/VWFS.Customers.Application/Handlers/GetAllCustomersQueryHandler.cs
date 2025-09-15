using System;
using System.Collections.Generic;
using MediatR;
using Microsoft.Extensions.Logging;
using VWFS.Customers.Application.Interfaces;
using VWFS.Customers.Application.Queries;
using VWFS.Customers.Domain.Entities;

namespace VWFS.Customers.Application.Handlers
{
    public class GetAllCustomersQueryHandler : IRequestHandler<GetAllCustomersQuery, IEnumerable<Customer>?>
    {
        private readonly ICustomerRepository _repository;
        private readonly ILogger<GetAllCustomersQueryHandler> _logger;

        public GetAllCustomersQueryHandler(ICustomerRepository repository, ILogger<GetAllCustomersQueryHandler> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<IEnumerable<Customer>?> Handle(GetAllCustomersQuery request, CancellationToken cancellationToken)
        {
            try
            {
                return await _repository.GetAllAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }
    }
}
