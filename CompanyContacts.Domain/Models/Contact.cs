namespace CompanyContacts.Domain.Models;

public sealed class Contact
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public int CompanyId { get; set; }
    public Company Company { get; set; }
    public int CountryId { get; set; }
    public Country Country { get; set; }
}
