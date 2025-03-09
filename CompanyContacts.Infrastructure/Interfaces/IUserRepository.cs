using CompanyContacts.Domain.Models;

namespace CompanyContacts.Infrastructure.Interfaces;

public interface IUserRepository
{
    Task AddUserAsync(User user);
    Task<User?> GetUserByUsernameAndPasswordAsync(string username, string password);
}