using CompanyContacts.Shared.Common;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace CompanyContacts.Shared.Settings;

public class DatabaseSettingsSetup(IConfiguration configuration) : IConfigureOptions<DatabaseSettings>
{
    public void Configure(DatabaseSettings options)
    {
        var section = configuration.GetSection(AppDefaults.DatabaseSettingsSection);
        if (section.Exists())
        {
            section.Bind(options);
        }
    }
}