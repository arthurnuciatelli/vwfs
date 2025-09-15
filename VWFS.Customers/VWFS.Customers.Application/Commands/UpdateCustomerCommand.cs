using System;
using MediatR;

namespace VWFS.Customers.Application.Commands
{
     public record UpdateCustomerCommand(
        Guid Id,
        string Name,
        DateTime BirthOrFoundationDate
    ): IRequest ;
}
