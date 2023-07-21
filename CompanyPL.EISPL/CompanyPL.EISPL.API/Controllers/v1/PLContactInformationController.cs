using CompanyPL.Common.Utility.Models;
using CompanyPL.EISPL.Application.Features.EISPL.PLContactInformation.Commands;
using CompanyPL.EISPL.Application.Features.EISPL.PLContactInformation.Queries;
using CompanyPL.EISPL.Core.EISPL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using CompanyPL.Common.API.Controllers;

namespace CompanyPL.EISPL.API.Controllers.v1;

[ApiVersion("1.0")]
public class PLContactInformationController : BaseApiController<PLContactInformationController>
{
    [Authorize(Policy = Permission.PLContactInformation.View)]
    [HttpGet]
    public async Task<ActionResult<PagedListResponse<PLContactInformationState>>> GetAsync([FromQuery] GetPLContactInformationQuery query) =>
        Ok(await Mediator.Send(query));

    [Authorize(Policy = Permission.PLContactInformation.View)]
    [HttpGet("{id}")]
    public async Task<ActionResult<PLContactInformationState>> GetAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new GetPLContactInformationByIdQuery(id)));

    [Authorize(Policy = Permission.PLContactInformation.Create)]
    [HttpPost]
    public async Task<ActionResult<PLContactInformationState>> PostAsync([FromBody] PLContactInformationViewModel request) =>
        await ToActionResult(async () => await Mediator.Send(Mapper.Map<AddPLContactInformationCommand>(request)));

    [Authorize(Policy = Permission.PLContactInformation.Edit)]
    [HttpPut("{id}")]
    public async Task<ActionResult<PLContactInformationState>> PutAsync(string id, [FromBody] PLContactInformationViewModel request)
    {
        var command = Mapper.Map<EditPLContactInformationCommand>(request);
        return await ToActionResult(async () => await Mediator.Send(command with { Id = id }));
    }

    [Authorize(Policy = Permission.PLContactInformation.Delete)]
    [HttpDelete("{id}")]
    public async Task<ActionResult<PLContactInformationState>> DeleteAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new DeletePLContactInformationCommand { Id = id }));
}

public record PLContactInformationViewModel
{
    [Required]
	[StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
	public string PLContactDetails { get;set; } = "";
	[Required]
	
	public string PLEmployeeId { get;set; } = "";
	   
}
