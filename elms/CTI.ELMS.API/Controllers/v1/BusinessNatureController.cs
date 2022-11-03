using CTI.Common.Utility.Models;
using CTI.ELMS.Application.Features.ELMS.BusinessNature.Commands;
using CTI.ELMS.Application.Features.ELMS.BusinessNature.Queries;
using CTI.ELMS.Core.ELMS;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using CTI.Common.API.Controllers;

namespace CTI.ELMS.API.Controllers.v1;

[ApiVersion("1.0")]
public class BusinessNatureController : BaseApiController<BusinessNatureController>
{
    [Authorize(Policy = Permission.BusinessNature.View)]
    [HttpGet]
    public async Task<ActionResult<PagedListResponse<BusinessNatureState>>> GetAsync([FromQuery] GetBusinessNatureQuery query) =>
        Ok(await Mediator.Send(query));

    [Authorize(Policy = Permission.BusinessNature.View)]
    [HttpGet("{id}")]
    public async Task<ActionResult<BusinessNatureState>> GetAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new GetBusinessNatureByIdQuery(id)));

    [Authorize(Policy = Permission.BusinessNature.Create)]
    [HttpPost]
    public async Task<ActionResult<BusinessNatureState>> PostAsync([FromBody] BusinessNatureViewModel request) =>
        await ToActionResult(async () => await Mediator.Send(Mapper.Map<AddBusinessNatureCommand>(request)));

    [Authorize(Policy = Permission.BusinessNature.Edit)]
    [HttpPut("{id}")]
    public async Task<ActionResult<BusinessNatureState>> PutAsync(string id, [FromBody] BusinessNatureViewModel request)
    {
        var command = Mapper.Map<EditBusinessNatureCommand>(request);
        return await ToActionResult(async () => await Mediator.Send(command with { Id = id }));
    }

    [Authorize(Policy = Permission.BusinessNature.Delete)]
    [HttpDelete("{id}")]
    public async Task<ActionResult<BusinessNatureState>> DeleteAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new DeleteBusinessNatureCommand { Id = id }));
}

public record BusinessNatureViewModel
{
    
	public string? BusinessNatureName { get;set; }
	
	public string? BusinessNatureCode { get;set; }
	public bool IsDisabled { get;set; }
	   
}
