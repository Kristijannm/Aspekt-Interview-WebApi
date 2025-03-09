using CompanyContacts.Domain.Models;
using MediatR;

namespace CompanyContacts.Application.Features.Contacts.GetContact;

public sealed record GetContactQuery(int Id) : IRequest<Contact>;