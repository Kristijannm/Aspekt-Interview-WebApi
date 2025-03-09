using CompanyContacts.Domain.Models;
using CompanyContacts.Infrastructure.Data;
using CompanyContacts.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CompanyContacts.Infrastructure.Repos;

public sealed class ContactRepository(ApplicationDbContext context) : IContactRepository
{
    private readonly ApplicationDbContext _context = context;

    public async Task AddContactAsync(Contact contact)
    {
        await _context.Contacts.AddAsync(contact);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteContactAsync(int id)
    {
        var contact = await _context.Contacts.FindAsync(id);
        if (contact != null)
        {
            _context.Contacts.Remove(contact);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<List<Contact>> FilterContactsAsync(int? countryId, int? companyId)
    {
        var query = _context.Contacts.AsQueryable();

        if (countryId.HasValue)
        {
            query = query.Where(c => c.CountryId == countryId.Value);
        }

        if (companyId.HasValue)
        {
            query = query.Where(c => c.CompanyId == companyId.Value);
        }

        query = query.Include(c => c.Company).Include(c => c.Country);

        return await query.ToListAsync();
    }

    public async Task<IEnumerable<Contact>> GetAllContactsAsync()
    {
        return await _context.Contacts.Include(c => c.Company)
                                      .Include(c => c.Country)
                                      .OrderBy(o => o.Id)
                                      .ToListAsync();
    }

    public async Task<Contact?> GetContactByIdAsync(int id)
    {
        return await _context.Contacts
                     .Include(c => c.Company)
                     .Include(c => c.Country)
                     .FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task UpdateContactAsync(Contact contact)
    {
        _context.Contacts.Update(contact);
        await _context.SaveChangesAsync();
    }
}