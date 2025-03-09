using MediatR;

namespace CompanyContacts.Application.Features.Contacts.UpdateContact;

public sealed record UpdateContactCommand(
    int Id,
    string Name,
    int CompanyId,
    int CountryId) : IRequest;