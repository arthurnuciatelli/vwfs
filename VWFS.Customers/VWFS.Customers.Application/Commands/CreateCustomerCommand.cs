using MediatR;
using System;
using VWFS.BuildingBlocks.Domain.Enum;

namespace VWFS.Customers.Application.Commands
{
    public record CreateCustomerCommand(
        string Name,
        string Document,                 // CPF ou CNPJ
        CustomerType Type
    ) : IRequest<Guid>;
}
