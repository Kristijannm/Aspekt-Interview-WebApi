using CompanyContacts.Domain.Models;
using CompanyContacts.Infrastructure.Data;
using CompanyContacts.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CompanyContacts.Infrastructure.Repos;

public sealed class CountryRepository(ApplicationDbContext context) : ICountryRepository
{
    private readonly ApplicationDbContext _context = context;

    public async Task<IEnumerable<Country>> GetAllAsync()
    {
        return await _context.Countries.Include(c => c.Contacts).ToListAsync();
    }

    public async Task<Country?> GetByIdAsync(int id)
    {
        return await _context.Countries.Include(x => x.Contacts).FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task AddAsync(Country country)
    {
        await _context.Countries.AddAsync(country);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Country country)
    {
        _context.Countries.Update(country);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Country country)
    {
        _context.Countries.Remove(country);
        await _context.SaveChangesAsync();
    }

    public async Task<Dictionary<string, int>> GetCompanyStatisticsByCountryIdAsync(int countryId)
    {
        var companyStatistics = await _context.Contacts
                                     .Where(c => c.CountryId == countryId)
                                     .GroupBy(c => c.Company.Name)
                                     .Select(group => new
                                     {
                                         CompanyName = group.Key,
                                         ContactCount = group.Count()
                                     }).ToListAsync();

        var statistics = companyStatistics.ToDictionary(x => x.CompanyName, x => x.ContactCount);

        return statistics;
    }

}
