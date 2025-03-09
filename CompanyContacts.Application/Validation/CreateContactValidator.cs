using CompanyContacts.Application.Features.Contacts.CreateContact;
using FluentValidation;

namespace CompanyContacts.Application.Validation;

public sealed class CreateContactValidator : AbstractValidator<CreateContactCommand>
{
    public CreateContactValidator() 
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required")
            .MaximumLength(100).WithMessage("Name cannot be longer that 100 charachers");

        RuleFor(x => x.CompanyId)
            .GreaterThan(0).WithMessage("Company ID must be greater than zero");

        RuleFor(x => x.CountryId)
            .GreaterThan(0).WithMessage("Country ID must be greater than zero");
    }
}
