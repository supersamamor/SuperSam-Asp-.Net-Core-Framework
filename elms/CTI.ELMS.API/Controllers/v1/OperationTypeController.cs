using CTI.Common.Utility.Models;
using CTI.ELMS.Application.Features.ELMS.OperationType.Commands;
using CTI.ELMS.Application.Features.ELMS.OperationType.Queries;
using CTI.ELMS.Core.ELMS;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using CTI.Common.API.Controllers;

namespace CTI.ELMS.API.Controllers.v1;

[ApiVersion("1.0")]
public class OperationTypeController : BaseApiController<OperationTypeController>
{
    [Authorize(Policy = Permission.OperationType.View)]
    [HttpGet]
    public async Task<ActionResult<PagedListResponse<OperationTypeState>>> GetAsync([FromQuery] GetOperationTypeQuery query) =>
        Ok(await Mediator.Send(query));

    [Authorize(Policy = Permission.OperationType.View)]
    [HttpGet("{id}")]
    public async Task<ActionResult<OperationTypeState>> GetAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new GetOperationTypeByIdQuery(id)));

    [Authorize(Policy = Permission.OperationType.Create)]
    [HttpPost]
    public async Task<ActionResult<OperationTypeState>> PostAsync([FromBody] OperationTypeViewModel request) =>
        await ToActionResult(async () => await Mediator.Send(Mapper.Map<AddOperationTypeCommand>(request)));

    [Authorize(Policy = Permission.OperationType.Edit)]
    [HttpPut("{id}")]
    public async Task<ActionResult<OperationTypeState>> PutAsync(string id, [FromBody] OperationTypeViewModel request)
    {
        var command = Mapper.Map<EditOperationTypeCommand>(request);
        return await ToActionResult(async () => await Mediator.Send(command with { Id = id }));
    }

    [Authorize(Policy = Permission.OperationType.Delete)]
    [HttpDelete("{id}")]
    public async Task<ActionResult<OperationTypeState>> DeleteAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new DeleteOperationTypeCommand { Id = id }));
}

public record OperationTypeViewModel
{
    [Required]
	
	public string OperationTypeName { get;set; } = "";
	   
}
