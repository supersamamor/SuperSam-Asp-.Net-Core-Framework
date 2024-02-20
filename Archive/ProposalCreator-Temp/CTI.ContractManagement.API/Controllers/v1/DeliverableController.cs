using CTI.Common.Utility.Models;
using CTI.ContractManagement.Application.Features.ContractManagement.Deliverable.Commands;
using CTI.ContractManagement.Application.Features.ContractManagement.Deliverable.Queries;
using CTI.ContractManagement.Core.ContractManagement;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using CTI.Common.API.Controllers;

namespace CTI.ContractManagement.API.Controllers.v1;

[ApiVersion("1.0")]
public class DeliverableController : BaseApiController<DeliverableController>
{
    [Authorize(Policy = Permission.Deliverable.View)]
    [HttpGet]
    public async Task<ActionResult<PagedListResponse<DeliverableState>>> GetAsync([FromQuery] GetDeliverableQuery query) =>
        Ok(await Mediator.Send(query));

    [Authorize(Policy = Permission.Deliverable.View)]
    [HttpGet("{id}")]
    public async Task<ActionResult<DeliverableState>> GetAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new GetDeliverableByIdQuery(id)));

    [Authorize(Policy = Permission.Deliverable.Create)]
    [HttpPost]
    public async Task<ActionResult<DeliverableState>> PostAsync([FromBody] DeliverableViewModel request) =>
        await ToActionResult(async () => await Mediator.Send(Mapper.Map<AddDeliverableCommand>(request)));

    [Authorize(Policy = Permission.Deliverable.Edit)]
    [HttpPut("{id}")]
    public async Task<ActionResult<DeliverableState>> PutAsync(string id, [FromBody] DeliverableViewModel request)
    {
        var command = Mapper.Map<EditDeliverableCommand>(request);
        return await ToActionResult(async () => await Mediator.Send(command with { Id = id }));
    }

    [Authorize(Policy = Permission.Deliverable.Delete)]
    [HttpDelete("{id}")]
    public async Task<ActionResult<DeliverableState>> DeleteAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new DeleteDeliverableCommand { Id = id }));
}

public record DeliverableViewModel
{
    [Required]
	
	public string ProjectCategoryId { get;set; } = "";
	[Required]
	[StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
	public string Name { get;set; } = "";
	   
}
