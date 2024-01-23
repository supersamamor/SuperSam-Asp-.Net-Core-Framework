using CompanyPL.Common.Utility.Models;
using CompanyPL.ProjectPL.Application.Features.ProjectPL.ContactInformation.Commands;
using CompanyPL.ProjectPL.Application.Features.ProjectPL.ContactInformation.Queries;
using CompanyPL.ProjectPL.Core.ProjectPL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using CompanyPL.Common.API.Controllers;

namespace CompanyPL.ProjectPL.API.Controllers.v1;

[ApiVersion("1.0")]
public class ContactInformationController : BaseApiController<ContactInformationController>
{
    [Authorize(Policy = Permission.ContactInformation.View)]
    [HttpGet]
    public async Task<ActionResult<PagedListResponse<ContactInformationState>>> GetAsync([FromQuery] GetContactInformationQuery query) =>
        Ok(await Mediator.Send(query));

    [Authorize(Policy = Permission.ContactInformation.View)]
    [HttpGet("{id}")]
    public async Task<ActionResult<ContactInformationState>> GetAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new GetContactInformationByIdQuery(id)));

    [Authorize(Policy = Permission.ContactInformation.Create)]
    [HttpPost]
    public async Task<ActionResult<ContactInformationState>> PostAsync([FromBody] ContactInformationViewModel request) =>
        await ToActionResult(async () => await Mediator.Send(Mapper.Map<AddContactInformationCommand>(request)));

    [Authorize(Policy = Permission.ContactInformation.Edit)]
    [HttpPut("{id}")]
    public async Task<ActionResult<ContactInformationState>> PutAsync(string id, [FromBody] ContactInformationViewModel request)
    {
        var command = Mapper.Map<EditContactInformationCommand>(request);
        return await ToActionResult(async () => await Mediator.Send(command with { Id = id }));
    }

    [Authorize(Policy = Permission.ContactInformation.Delete)]
    [HttpDelete("{id}")]
    public async Task<ActionResult<ContactInformationState>> DeleteAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new DeleteContactInformationCommand { Id = id }));
}

public record ContactInformationViewModel
{
    [Required]
	
	public string EmployeeId { get;set; } = "";
	[Required]
	[StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
	public string ContactDetails { get;set; } = "";
	   
}
