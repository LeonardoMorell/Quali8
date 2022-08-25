using Application.Models.Requests;
using Application.Validators.ValidationExtensions;
using FluentValidation;
using FluentValidation.Validators;

namespace Application.Validators
{
    public class CustomerCreateValidation : AbstractValidator<CreateCustomerRequest>
    {
        public CustomerCreateValidation()
        {
            RuleFor(x => x.Fullname)
                .NotEmpty()
                .NotNull()
                .Must(x => x.IsValidFullname())
                .MaximumLength(300)
                .MinimumLength(2);

            RuleFor(x => x.Company)
                .NotEmpty()
                .NotNull()
                .MinimumLength(3)
                .MaximumLength(40);

            RuleFor(x => x.Email)
                .NotEmpty()
                .NotNull()
                .MaximumLength(256)
                .EmailAddress(EmailValidationMode.Net4xRegex);

            RuleFor(x => x.Cellphone)
                .NotEmpty()
                .NotNull()
                .Must(x => x.OnlyNumbers())
                .Length(11);

            RuleFor(x => x.CommercialPhone)
                .NotEmpty()
                .NotNull()
                .Must(x => x.OnlyNumbers())
                .Length(10);
        }
    }
}