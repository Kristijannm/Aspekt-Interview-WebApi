using CompanyContacts.Application.Features.Users.LoginUserCommand;
using CompanyContacts.Application.Features.Users.RegisterUser;
using CompanyContacts.Shared.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CompanyContactsApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

    [HttpPost("Register")]
    public async Task<IActionResult> Register([FromBody] RegisterUserDto request)
    {
        var userId = await _mediator.Send(new RegisterUserCommand(request));
        return CreatedAtAction(nameof(Register), new { id = userId });
    }

    [HttpPost("Login")]
    public async Task<IActionResult> Login([FromBody] LoginUserDto request)
    {
        var token = await _mediator.Send(new LoginUserCommand(request));
        return Ok(token);
    }
}