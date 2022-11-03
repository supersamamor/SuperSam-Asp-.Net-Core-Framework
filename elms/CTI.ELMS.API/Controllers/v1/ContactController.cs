using CTI.Common.Utility.Models;
using CTI.ELMS.Application.Features.ELMS.Contact.Commands;
using CTI.ELMS.Application.Features.ELMS.Contact.Queries;
using CTI.ELMS.Core.ELMS;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using CTI.Common.API.Controllers;

namespace CTI.ELMS.API.Controllers.v1;

[ApiVersion("1.0")]
public class ContactController : BaseApiController<ContactController>
{
    [Authorize(Policy = Permission.Contact.View)]
    [HttpGet]
    public async Task<ActionResult<PagedListResponse<ContactState>>> GetAsync([FromQuery] GetContactQuery query) =>
        Ok(await Mediator.Send(query));

    [Authorize(Policy = Permission.Contact.View)]
    [HttpGet("{id}")]
    public async Task<ActionResult<ContactState>> GetAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new GetContactByIdQuery(id)));

    [Authorize(Policy = Permission.Contact.Create)]
    [HttpPost]
    public async Task<ActionResult<ContactState>> PostAsync([FromBody] ContactViewModel request) =>
        await ToActionResult(async () => await Mediator.Send(Mapper.Map<AddContactCommand>(request)));

    [Authorize(Policy = Permission.Contact.Edit)]
    [HttpPut("{id}")]
    public async Task<ActionResult<ContactState>> PutAsync(string id, [FromBody] ContactViewModel request)
    {
        var command = Mapper.Map<EditContactCommand>(request);
        return await ToActionResult(async () => await Mediator.Send(command with { Id = id }));
    }

    [Authorize(Policy = Permission.Contact.Delete)]
    [HttpDelete("{id}")]
    public async Task<ActionResult<ContactState>> DeleteAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new DeleteContactCommand { Id = id }));
}

public record ContactViewModel
{
    
	public string? LeadID { get;set; }
	public int? ContactType { get;set; }
	[Required]
	[StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
	public string ContactDetails { get;set; } = "";
	   
}
