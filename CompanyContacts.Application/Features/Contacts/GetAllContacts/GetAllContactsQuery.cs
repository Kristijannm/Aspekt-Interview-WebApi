using CompanyContacts.Domain.Models;
using MediatR;

namespace CompanyContacts.Application.Features.Contacts.GetAllContacts;

public sealed record GetAllContactsQuery() : IRequest<IEnumerable<Contact>>;
