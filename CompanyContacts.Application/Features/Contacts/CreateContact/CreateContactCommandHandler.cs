using CompanyContacts.Domain.Models;
using CompanyContacts.Infrastructure.Data;
using MediatR;

namespace CompanyContacts.Application.Features.Contacts.CreateContact;

public sealed class CreateContactCommandHandler(ApplicationDbContext context) : IRequestHandler<CreateContactCommand, int>
{
    private readonly ApplicationDbContext _context = context;

    public async Task<int> Handle(CreateContactCommand request, CancellationToken cancellationToken)
    {
        var contact = new Contact
        {
            Name = request.Name,
            CompanyId = request.CompanyId,
            CountryId = request.CountryId,
        };

        _context.Contacts.Add(contact);
        await _context.SaveChangesAsync();
        return contact.Id;
    }
}
