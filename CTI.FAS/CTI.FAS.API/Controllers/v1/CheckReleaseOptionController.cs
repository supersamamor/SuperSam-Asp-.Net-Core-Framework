using CTI.Common.Utility.Models;
using CTI.FAS.Application.Features.FAS.CheckReleaseOption.Commands;
using CTI.FAS.Application.Features.FAS.CheckReleaseOption.Queries;
using CTI.FAS.Core.FAS;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using CTI.Common.API.Controllers;

namespace CTI.FAS.API.Controllers.v1;

[ApiVersion("1.0")]
public class CheckReleaseOptionController : BaseApiController<CheckReleaseOptionController>
{
    [Authorize(Policy = Permission.CheckReleaseOption.View)]
    [HttpGet]
    public async Task<ActionResult<PagedListResponse<CheckReleaseOptionState>>> GetAsync([FromQuery] GetCheckReleaseOptionQuery query) =>
        Ok(await Mediator.Send(query));

    [Authorize(Policy = Permission.CheckReleaseOption.View)]
    [HttpGet("{id}")]
    public async Task<ActionResult<CheckReleaseOptionState>> GetAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new GetCheckReleaseOptionByIdQuery(id)));

    [Authorize(Policy = Permission.CheckReleaseOption.Create)]
    [HttpPost]
    public async Task<ActionResult<CheckReleaseOptionState>> PostAsync([FromBody] CheckReleaseOptionViewModel request) =>
        await ToActionResult(async () => await Mediator.Send(Mapper.Map<AddCheckReleaseOptionCommand>(request)));

    [Authorize(Policy = Permission.CheckReleaseOption.Edit)]
    [HttpPut("{id}")]
    public async Task<ActionResult<CheckReleaseOptionState>> PutAsync(string id, [FromBody] CheckReleaseOptionViewModel request)
    {
        var command = Mapper.Map<EditCheckReleaseOptionCommand>(request);
        return await ToActionResult(async () => await Mediator.Send(command with { Id = id }));
    }

    [Authorize(Policy = Permission.CheckReleaseOption.Delete)]
    [HttpDelete("{id}")]
    public async Task<ActionResult<CheckReleaseOptionState>> DeleteAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new DeleteCheckReleaseOptionCommand { Id = id }));
}

public record CheckReleaseOptionViewModel
{
    [Required]
	
	public string CreditorId { get;set; } = "";
	[Required]
	[StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
	public string Name { get;set; } = "";
	   
}
