using CompanyContacts.Domain.Models;
using CompanyContacts.Infrastructure.Interfaces;
using MediatR;

namespace CompanyContacts.Application.Features.Contacts.GetAllContacts;

public sealed class GetAllContactsQueryHandler(IContactRepository contactRepo) : IRequestHandler<GetAllContactsQuery, IEnumerable<Contact>>
{
    private readonly IContactRepository _contactRepo = contactRepo;

    public async Task<IEnumerable<Contact>> Handle(GetAllContactsQuery request, CancellationToken cancellationToken)
    {
        return await _contactRepo.GetAllContactsAsync();
    }
}
