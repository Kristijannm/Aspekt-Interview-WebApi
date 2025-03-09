using System.Text.Json.Serialization;

namespace CompanyContacts.Domain.Models;

public sealed class Company
{
    public int Id { get; set; }
    public required string Name { get; set; }

    [JsonIgnore]
    public ICollection<Contact> Contacts { get; set; } = [];
}