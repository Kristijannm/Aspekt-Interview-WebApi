using CompanyContacts.Application.Features.Companies.CreateCompany;
using CompanyContacts.Application.Features.Companies.DeleteCompany;
using CompanyContacts.Application.Features.Companies.GetAllCompanies;
using CompanyContacts.Application.Features.Companies.GetCompany;
using CompanyContacts.Application.Features.Companies.GetCompanyStatisticsByCountryId;
using CompanyContacts.Application.Features.Companies.UpdateCompany;
using CompanyContacts.Shared.DTOs;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CompanyContactsApi.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public sealed class CompanyController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

    [HttpPost("Create")]
    public async Task<IActionResult> CreateCompany([FromBody] CreateCompanyCommand command)
    {
        var companyId = await _mediator.Send(command);
        return CreatedAtAction(nameof(CreateCompany), new { id = companyId });
    }

    [AllowAnonymous]
    [HttpGet("GetAll")]
    public async Task<IActionResult> GetCompanies([FromQuery]int pageNumber = 1, [FromQuery] int pageSize = 10)
    {
        var result = await _mediator.Send(new GetAllCompaniesQuery(pageNumber, pageSize));
        return Ok(result);
    }

    [AllowAnonymous]
    [HttpGet("Get/{id}")]
    public async Task<IActionResult> GetCompanyById(int id)
    {
        var company = await _mediator.Send(new GetCompanyQuery(id));
        return Ok(company);
    }

    [HttpDelete("Delete/{id}")]
    public async Task<IActionResult> DeleteCompany(int id)
    {
        await _mediator.Send(new DeleteCompanyCommand(id));
        return NoContent();
    }

    [HttpPut("Update/{id}")]
    public async Task<IActionResult> UpdateCompany(int id, UpdateCompanyDto updateCompany)
    {
        var command = new UpdateCompanyCommand(id, updateCompany.Name);

        var updatedCompany = await _mediator.Send(command);

        return Ok(updatedCompany);
    }

    [AllowAnonymous]
    [HttpGet("company-statistics/{countryId}")]
    public async Task<IActionResult> GetCompanyStatisticsByCountryId(int countryId)
    {
        var query = new GetCompanyStatisticsByCountryIdQuery(countryId);
        var result = await _mediator.Send(query);
        return Ok(result);
    }
}