using CompanyContacts.Domain.Models;
using CompanyContacts.Infrastructure.Data;
using CompanyContacts.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CompanyContacts.Infrastructure.Repos;

public sealed class CompanyRepository(ApplicationDbContext context) : ICompanyRepository
{
    private readonly ApplicationDbContext _context = context;

    public async Task<(IEnumerable<Company>, int)> GetAllPagedAsync(int pageNumber, int pageSize)
    {
        var query = _context.Companies.AsQueryable();

        int totalCount = await query.CountAsync();

        var companies = await query
            .OrderBy(c => c.Id)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return (companies, totalCount);
    }

    public async Task<Company?> GetByIdAsync(int id)
    {
        return await _context.Companies.FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task AddAsync(Company company)
    {
        await _context.Companies.AddAsync(company);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Company company)
    {
        _context.Companies.Update(company);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Company company)
    {
        _context.Companies.Remove(company);
        await _context.SaveChangesAsync();
    }

    public async Task<Dictionary<string, int>> GetCompanyStatisticsByCountryIdAsync(int countryId)
    {
        return await _context.Contacts
            .Where(c => c.CountryId == countryId)
            .GroupBy(c => c.Company.Name)
            .Select(g => new
            {
                CompanyName = g.Key,
                ContactCount = g.Count()
            }).ToDictionaryAsync(g => g.CompanyName, g => g.ContactCount);
    }

}
