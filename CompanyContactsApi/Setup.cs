using CompanyContacts.Shared.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace CompanyContactsApi;

public static class Setup
{
    public static IServiceCollection AddAppAuthentication(this IServiceCollection services)
    {
        var serviceProvider = services.BuildServiceProvider();
        var jwtAuthenticationOptions = serviceProvider.GetService<IOptions<JwtSettings>>()!.Value;

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateLifetime = true,
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtAuthenticationOptions.SecretKey))
            });

        return services;
    }
}