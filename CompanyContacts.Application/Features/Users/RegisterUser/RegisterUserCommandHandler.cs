using AutoMapper;
using CompanyContacts.Domain.Models;
using CompanyContacts.Infrastructure.Interfaces;
using MediatR;

namespace CompanyContacts.Application.Features.Users.RegisterUser;

public sealed class RegisterUserCommandHandler(IMapper mapper, IUserRepository userRepository) : IRequestHandler<RegisterUserCommand, int>
{
    private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    private readonly IUserRepository _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));

    public async Task<int> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var domainUser = _mapper.Map<User>(request.RegisterUserDto);
        await _userRepository.AddUserAsync(domainUser);
        return domainUser.Id;
    }
}