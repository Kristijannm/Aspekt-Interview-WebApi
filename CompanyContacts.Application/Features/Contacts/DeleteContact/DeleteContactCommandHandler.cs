using CompanyContacts.Application.Exceptions;
using CompanyContacts.Infrastructure.Interfaces;
using MediatR;

namespace CompanyContacts.Application.Features.Contacts.DeleteContact;

public sealed class DeleteContactCommandHandler(IContactRepository contactRepository) : IRequestHandler<DeleteContactCommand>
{
    public readonly IContactRepository _contactRepository = contactRepository;

    public async Task Handle(DeleteContactCommand request, CancellationToken cancellationToken)
    {
        _ = await _contactRepository.GetContactByIdAsync(request.Id) ?? 
            throw new NotFoundException("Contact not found");

        await _contactRepository.DeleteContactAsync(request.Id);
    }
}
