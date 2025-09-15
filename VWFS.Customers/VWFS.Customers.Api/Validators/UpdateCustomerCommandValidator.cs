using System;
using FluentValidation;
using VWFS.Customers.Application.Commands;

namespace VWFS.Customers.Api.Validators
{
    public class UpdateCustomerCommandValidator : AbstractValidator<UpdateCustomerCommand>
    {
        public UpdateCustomerCommandValidator()
        {
            RuleFor(t => t.Id)
                .NotEmpty()
                .WithMessage("Id is required");

            RuleFor(t => t.Name)
                .NotEmpty()
                .WithMessage("Name is required");

            RuleFor(t => t.BirthOrFoundationDate)
                .LessThanOrEqualTo(DateTime.Today)
                .WithMessage("BirthOrFoundationDate must be in the past");
        }
    }
}
