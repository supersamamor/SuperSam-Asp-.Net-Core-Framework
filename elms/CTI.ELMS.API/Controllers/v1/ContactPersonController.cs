using CTI.Common.Utility.Models;
using CTI.ELMS.Application.Features.ELMS.ContactPerson.Commands;
using CTI.ELMS.Application.Features.ELMS.ContactPerson.Queries;
using CTI.ELMS.Core.ELMS;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using CTI.Common.API.Controllers;

namespace CTI.ELMS.API.Controllers.v1;

[ApiVersion("1.0")]
public class ContactPersonController : BaseApiController<ContactPersonController>
{
    [Authorize(Policy = Permission.ContactPerson.View)]
    [HttpGet]
    public async Task<ActionResult<PagedListResponse<ContactPersonState>>> GetAsync([FromQuery] GetContactPersonQuery query) =>
        Ok(await Mediator.Send(query));

    [Authorize(Policy = Permission.ContactPerson.View)]
    [HttpGet("{id}")]
    public async Task<ActionResult<ContactPersonState>> GetAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new GetContactPersonByIdQuery(id)));

    [Authorize(Policy = Permission.ContactPerson.Create)]
    [HttpPost]
    public async Task<ActionResult<ContactPersonState>> PostAsync([FromBody] ContactPersonViewModel request) =>
        await ToActionResult(async () => await Mediator.Send(Mapper.Map<AddContactPersonCommand>(request)));

    [Authorize(Policy = Permission.ContactPerson.Edit)]
    [HttpPut("{id}")]
    public async Task<ActionResult<ContactPersonState>> PutAsync(string id, [FromBody] ContactPersonViewModel request)
    {
        var command = Mapper.Map<EditContactPersonCommand>(request);
        return await ToActionResult(async () => await Mediator.Send(command with { Id = id }));
    }

    [Authorize(Policy = Permission.ContactPerson.Delete)]
    [HttpDelete("{id}")]
    public async Task<ActionResult<ContactPersonState>> DeleteAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new DeleteContactPersonCommand { Id = id }));
}

public record ContactPersonViewModel
{
    [Required]
	
	public string LeadId { get;set; } = "";
	
	public string? SalutationID { get;set; }
	[StringLength(35, ErrorMessage = "{0} length can't be more than {1}.")]
	public string? FirstName { get;set; }
	[StringLength(30, ErrorMessage = "{0} length can't be more than {1}.")]
	public string? MiddleName { get;set; }
	[StringLength(70, ErrorMessage = "{0} length can't be more than {1}.")]
	public string? LastName { get;set; }
	[StringLength(120, ErrorMessage = "{0} length can't be more than {1}.")]
	public string? Position { get;set; }
	public bool IsSOARecipient { get;set; }
	public bool IsANSignatory { get;set; }
	public bool IsCOLSignatory { get;set; }
	   
}
