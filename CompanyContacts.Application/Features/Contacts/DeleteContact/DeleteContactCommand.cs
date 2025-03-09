using MediatR;

namespace CompanyContacts.Application.Features.Contacts.DeleteContact;

public sealed record DeleteContactCommand(int Id) : IRequest;