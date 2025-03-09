using CompanyContacts.Domain.Models;
using MediatR;

namespace CompanyContacts.Application.Features.Contacts.FilterContacts;

public sealed record FilterContactsCommand(int? CountryId, int? CompanyId) : IRequest<List<Contact>>;