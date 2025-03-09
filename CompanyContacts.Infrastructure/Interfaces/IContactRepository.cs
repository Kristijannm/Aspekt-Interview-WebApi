using CompanyContacts.Domain.Models;

namespace CompanyContacts.Infrastructure.Interfaces;

public interface IContactRepository
{
    Task<Contact?> GetContactByIdAsync(int id);
    Task<IEnumerable<Contact>> GetAllContactsAsync();
    Task AddContactAsync(Contact contact);
    Task UpdateContactAsync(Contact contact);
    Task DeleteContactAsync(int id);
    Task<List<Contact>> FilterContactsAsync(int? countryId, int? companyId);
}
