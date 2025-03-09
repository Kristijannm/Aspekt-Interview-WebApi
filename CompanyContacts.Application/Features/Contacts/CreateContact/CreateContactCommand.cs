using MediatR;

namespace CompanyContacts.Application.Features.Contacts.CreateContact;

public sealed record CreateContactCommand(string Name, int CompanyId, int CountryId) : IRequest<int>;