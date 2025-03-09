using CompanyContacts.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace CompanyContacts.Infrastructure.Data;

public sealed class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<Company> Companies { get; set; }
    public DbSet<Contact> Contacts { get; set; }
    public DbSet<Country> Countries { get; set; }
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Company>()
            .HasMany(c => c.Contacts)
            .WithOne(c => c.Company)
            .HasForeignKey(c => c.CompanyId);

        modelBuilder.Entity<Country>()
            .HasMany(c => c.Contacts)
            .WithOne(c => c.Country)
            .HasForeignKey(c => c.CountryId);
    }
}