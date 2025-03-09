using CompanyContacts.Application.Exceptions;
using CompanyContacts.Domain.Models;
using CompanyContacts.Infrastructure.Interfaces;
using MediatR;

namespace CompanyContacts.Application.Features.Contacts.GetContact;

public sealed class GetContactQueryHandler(IContactRepository contactRepo) : IRequestHandler<GetContactQuery, Contact>
{
    private readonly IContactRepository _contactRepo = contactRepo;

    public async Task<Contact> Handle(GetContactQuery request, CancellationToken cancellationToken)
    {
        return await _contactRepo.GetContactByIdAsync(request.Id) ?? 
            throw new NotFoundException("Contact not found");
    }
}