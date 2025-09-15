using MediatR;
using VWFS.Customers.Domain.Entities;

namespace VWFS.Customers.Application.Queries
{
    public record GetCustomerByIdQuery(Guid Id) : IRequest<Customer?>;
}
