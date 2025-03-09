using CompanyContacts.Application.Exceptions;
using CompanyContacts.Infrastructure.Interfaces;
using MediatR;

namespace CompanyContacts.Application.Features.Contacts.UpdateContact;

public sealed class UpdateContactCommandHandler(IContactRepository contactRepository) : IRequestHandler<UpdateContactCommand>
{
    private readonly IContactRepository _contactRepository = contactRepository ?? 
        throw new ArgumentNullException(nameof(contactRepository));

    public async Task Handle(UpdateContactCommand request, CancellationToken cancellationToken)
    {
        var contact = await _contactRepository.GetContactByIdAsync(request.Id) ?? 
            throw new NotFoundException("Contact not found");
        
        contact.Name = request.Name;
        contact.CountryId = request.CountryId;
        contact.CompanyId = request.CompanyId;

        await _contactRepository.UpdateContactAsync(contact);
    }
}