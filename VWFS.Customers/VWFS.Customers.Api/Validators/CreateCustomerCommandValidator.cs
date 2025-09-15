using System;
using FluentValidation;
using VWFS.Customers.Application.Commands;

namespace VWFS.Customers.Api.Validators
{
    public class CreateCustomerCommandValidator : AbstractValidator<CreateCustomerCommand>
    {
        public CreateCustomerCommandValidator()
        {
            RuleFor(t => t.Name)
                .NotEmpty()
                .WithMessage("Name is required");

            RuleFor(t => t.Document)
                .NotEmpty()
                .WithMessage("Document is required");

            RuleFor(t => t.Type)
                .IsInEnum()
                .WithMessage("Type must be between 0 and 1");

            RuleFor(t => t.BirthOrFoundationDate)
                .LessThanOrEqualTo(DateTime.Today)
                .WithMessage("BirthOrFoundationDate must be in the past");

        }
    }
}
