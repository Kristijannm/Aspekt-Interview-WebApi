using CompanyContacts.Infrastructure.Interfaces;
using CompanyContacts.Shared.Settings;
using MediatR;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CompanyContacts.Application.Features.Users.LoginUserCommand;

public sealed class LoginUserCommandHandler(IUserRepository repository, IOptions<JwtSettings> jwtOptions) :
    IRequestHandler<LoginUserCommand, string>
{
    public async Task<string> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        var user = await repository.GetUserByUsernameAndPasswordAsync(request.LoginUserDto.Username, request.LoginUserDto.Password) ??
            throw new UnauthorizedAccessException("Invalid username or password.");

        var secret = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.Value.SecretKey));
        var credentials = new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
        var claims = new List<Claim>()
        {
            new ("Username", user.Username),
            new (ClaimTypes.Email, user.Email),
        };

        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.Now.AddHours(1),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}