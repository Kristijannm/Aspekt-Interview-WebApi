using CompanyContacts.Shared.DTOs;
using MediatR;

namespace CompanyContacts.Application.Features.Users.LoginUserCommand;

public sealed record LoginUserCommand(LoginUserDto LoginUserDto) : IRequest<string>;