using CompanyContacts.Application.Behaviors;
using CompanyContacts.Application.Features.Contacts.GetContact;
using CompanyContacts.Application.Validation;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace CompanyContacts.Application.DependencyInjection;

public static class ApplicationServicesRegistration
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        //Register MediatR & CQRS handler
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(GetContactQuery).Assembly));

        //Register FluentValidation
        services.AddValidatorsFromAssemblyContaining<CreateContactValidator>();

        //Register Pipeling Behavior
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

        return services;
    }
}
