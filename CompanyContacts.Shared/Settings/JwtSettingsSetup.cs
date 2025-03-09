using CompanyContacts.Shared.Common;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace CompanyContacts.Shared.Settings;

public class JwtSettingsSetup(IConfiguration configuration) : IConfigureOptions<JwtSettings>
{
    public void Configure(JwtSettings options)
    {
        var section = configuration.GetSection(AppDefaults.JwtSettingsSection);
        if (section.Exists())
        {
            section.Bind(options);
        }
    }
}