namespace CompanyContacts.Domain.Models;

public sealed class User
{
    public int Id { get; set; }

    public required string Username { get; set; }

    public required string Email { get; set; }

    public required string Password { get; set; }
}