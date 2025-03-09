using CompanyContacts.Shared.DTOs;
using MediatR;

namespace CompanyContacts.Application.Features.Users.RegisterUser;

public sealed record RegisterUserCommand(RegisterUserDto RegisterUserDto) : IRequest<int>;