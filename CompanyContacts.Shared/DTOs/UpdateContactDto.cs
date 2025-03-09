namespace CompanyContacts.Shared.DTOs;

public sealed class UpdateContactDto
{
    public required string Name { get; set; }
    public int CompanyId { get; set; }
    public int CountryId { get; set; }
}