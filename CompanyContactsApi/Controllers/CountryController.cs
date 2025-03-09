using CompanyContacts.Application.Features.Countries.CreateCountry;
using CompanyContacts.Application.Features.Countries.DeleteCountry;
using CompanyContacts.Application.Features.Countries.GetAllCountries;
using CompanyContacts.Application.Features.Countries.GetCountry;
using CompanyContacts.Application.Features.Countries.UpdateCountry;
using CompanyContacts.Shared.DTOs;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CompanyContactsApi.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public sealed class CountryController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

    [HttpPost("Create")]
    public async Task<IActionResult> CreateCountry([FromBody] CreateCountryCommand command)
    {
        var countryId = await _mediator.Send(command);
        return CreatedAtAction(nameof(CreateCountry), new { id = countryId });
    }

    [AllowAnonymous]
    [HttpGet("GetAll")]
    public async Task<IActionResult> GetCountries()
    {
        var countries = await _mediator.Send(new GetAllCountriesQuery());
        return Ok(countries);
    }

    [AllowAnonymous]
    [HttpGet("Get/{id}")]
    public async Task<IActionResult> GetCountryById(int id)
    {
        var country = await _mediator.Send(new GetCountryQuery(id));
        return Ok(country);
    }

    [HttpDelete("Delete/{id}")]
    public async Task<IActionResult> DeleteCountry(int id)
    {
        await _mediator.Send(new DeleteCountryCommand(id));
        return NoContent();
    }

    [HttpPut("Update/{id}")]
    public async Task<IActionResult> UpdateCountry(int id, UpdateCountryDto updateCountry)
    {
        var command = new UpdateCountryCommand(id ,updateCountry.Name);

        var updatedCountry = await _mediator.Send(command);

        return Ok(updatedCountry);
    }
}