using CompanyContacts.Domain.Models;

namespace CompanyContacts.Infrastructure.Interfaces;

public interface ICountryRepository
{
    Task<IEnumerable<Country>> GetAllAsync();
    Task<Country?> GetByIdAsync(int id);
    Task AddAsync(Country country);
    Task UpdateAsync(Country country);
    Task DeleteAsync(Country country);
    Task<Dictionary<string, int>> GetCompanyStatisticsByCountryIdAsync(int countryId);
}
