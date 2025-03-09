using CompanyContacts.Domain.Models;

namespace CompanyContacts.Infrastructure.Interfaces;

public interface ICompanyRepository
{
    Task<(IEnumerable<Company> companies, int TotalCount)> GetAllPagedAsync(int pageNumber, int pageSize);
    Task<Company?> GetByIdAsync(int id);
    Task AddAsync(Company company);
    Task UpdateAsync(Company company);
    Task DeleteAsync(Company company);
    Task<Dictionary<string, int>> GetCompanyStatisticsByCountryIdAsync(int countryId);
}
