using FluentValidation;
using VWFS.Customers.Application.Commands;

namespace VWFS.Customers.Api.Validators
{
    public class DeleteCustomerCommandValidator : AbstractValidator<DeleteCustomerCommand>
    {
        public DeleteCustomerCommandValidator()
        {
            RuleFor(t => t.Id)
                .NotEmpty()
                .WithMessage("Id is required");
        }
    }
}
