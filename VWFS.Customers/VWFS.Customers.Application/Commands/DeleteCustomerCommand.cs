using System;
using MediatR;

namespace VWFS.Customers.Application.Commands
{
   public record DeleteCustomerCommand(Guid Id): IRequest;
}
