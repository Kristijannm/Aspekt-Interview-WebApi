using AutoMapper;
using CompanyContacts.Infrastructure.Data;
using CompanyContacts.Infrastructure.Interfaces;
using CompanyContacts.Infrastructure.Mappers;
using CompanyContacts.Infrastructure.Repos;
using CompanyContacts.Shared.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace CompanyContacts.Infrastructure.DependencyInjection;

public static class InfrastructureServiceRegistration
{
    public static IServiceCollection BindOptions(this IServiceCollection services)
    {
        //Binding configurations to DatabaseSettings
        services.ConfigureOptions<DatabaseSettingsSetup>();
        services.ConfigureOptions<JwtSettingsSetup>();
        return services;
    }

    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        //Access DatabaseSettings from configuration
        services.AddDbContext<ApplicationDbContext>((serviceProvider, options) =>
        {
            var databaseSettings = serviceProvider.GetRequiredService<IOptions<DatabaseSettings>>().Value;
            options.UseNpgsql(databaseSettings.ConnectionString);
        });

        // Register Repositories
        services.AddScoped<IContactRepository, ContactRepository>();
        services.AddScoped<ICompanyRepository, CompanyRepository>();
        services.AddScoped<ICountryRepository, CountryRepository>();
        services.AddScoped<IUserRepository, UserRepository>();

        // AutoMapper
        services.AddSingleton(sp => AutoMapperConfiguration.GetConfiguration());
        services.AddScoped(sp =>
        {
            MapperConfiguration config = sp.GetRequiredService<MapperConfiguration>();
            return config.CreateMapper();
        });

        return services;
    }
}