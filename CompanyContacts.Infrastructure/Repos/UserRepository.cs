using CompanyContacts.Domain.Models;
using CompanyContacts.Infrastructure.Data;
using CompanyContacts.Infrastructure.Interfaces;
using CompanyContacts.Shared.Helpers;
using Microsoft.EntityFrameworkCore;

namespace CompanyContacts.Infrastructure.Repos;

public sealed class UserRepository(ApplicationDbContext context) : IUserRepository
{
    private readonly ApplicationDbContext _context = context;

    public async Task AddUserAsync(User user)
    {
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
    }

    public async Task<User?> GetUserByUsernameAndPasswordAsync(string username, string password)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
        var isValid = HashingHelper.VerifyPassword(password, user.Password);
        return isValid ? user : null;
    }
}