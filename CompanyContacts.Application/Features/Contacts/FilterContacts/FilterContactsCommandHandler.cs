using CompanyContacts.Domain.Models;
using CompanyContacts.Infrastructure.Interfaces;
using MediatR;

namespace CompanyContacts.Application.Features.Contacts.FilterContacts;

public  class FilterContactsCommandHandler(IContactRepository contactRepo) : IRequestHandler<FilterContactsCommand, List<Contact>>
{
    private readonly IContactRepository _contactRepo = contactRepo;

    public async Task<List<Contact>> Handle(FilterContactsCommand request, CancellationToken cancellationToken)
    {
        return await _contactRepo.FilterContactsAsync(request.CountryId, request.CompanyId);
    }
}