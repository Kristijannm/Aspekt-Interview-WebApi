using CompanyContacts.Application.Features.Contacts.CreateContact;
using CompanyContacts.Application.Features.Contacts.DeleteContact;
using CompanyContacts.Application.Features.Contacts.FilterContacts;
using CompanyContacts.Application.Features.Contacts.GetAllContacts;
using CompanyContacts.Application.Features.Contacts.GetContact;
using CompanyContacts.Application.Features.Contacts.UpdateContact;
using CompanyContacts.Shared.DTOs;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CompanyContactsApi.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public sealed class ContactsController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpPost("Create")]
    public async Task<IActionResult> CreateContact([FromBody] CreateContactCommand command)
    {
        var contactId = await _mediator.Send(command);
        return CreatedAtAction(nameof(CreateContact), new { id = contactId });
    }

    [AllowAnonymous]
    [HttpGet("GetAll")]
    public async Task<IActionResult> GetContacts()
    {
        var contacts = await _mediator.Send(new GetAllContactsQuery());
        return Ok(contacts);
    }

    [AllowAnonymous]
    [HttpGet("Get/{id}")]
    public async Task<IActionResult> GetContactById(int id)
    {
        var contact = await _mediator.Send(new GetContactQuery(id));
        return Ok(contact);
    }

    [HttpDelete("Delete/{id}")]
    public async Task<IActionResult> DeleteContact(int id)
    {
        await _mediator.Send(new DeleteContactCommand(id));
        return NoContent();
    }

    [HttpPut("Update/{id}")]
    public async Task<IActionResult> UpdateContact(int id, [FromBody] UpdateContactDto updatedContact)
    {
        var command = new UpdateContactCommand(
            id,
            updatedContact.Name,
            updatedContact.CompanyId,
            updatedContact.CountryId);

        await _mediator.Send(command);
        return Ok();
    }

    [AllowAnonymous]
    [HttpGet("Filter")]
    public async Task<IActionResult> FilterContacts([FromQuery] int? countryId, [FromQuery] int? companyId)
    {
        var command = new FilterContactsCommand(countryId, companyId);
        var result = await _mediator.Send(command);

        if (result == null || result.Count == 0)
        {
            return NotFound("No contacts found matching the criteria.");
        }

        return Ok(result);
    }
}
