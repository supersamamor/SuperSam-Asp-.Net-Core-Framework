using CelerSoft.Common.Utility.Models;
using CelerSoft.TurboERP.Application.Features.TurboERP.InventoryHistory.Commands;
using CelerSoft.TurboERP.Application.Features.TurboERP.InventoryHistory.Queries;
using CelerSoft.TurboERP.Core.TurboERP;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using CelerSoft.Common.API.Controllers;

namespace CelerSoft.TurboERP.API.Controllers.v1;

[ApiVersion("1.0")]
public class InventoryHistoryController : BaseApiController<InventoryHistoryController>
{
    [Authorize(Policy = Permission.InventoryHistory.View)]
    [HttpGet]
    public async Task<ActionResult<PagedListResponse<InventoryHistoryState>>> GetAsync([FromQuery] GetInventoryHistoryQuery query) =>
        Ok(await Mediator.Send(query));

    [Authorize(Policy = Permission.InventoryHistory.View)]
    [HttpGet("{id}")]
    public async Task<ActionResult<InventoryHistoryState>> GetAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new GetInventoryHistoryByIdQuery(id)));

    [Authorize(Policy = Permission.InventoryHistory.Create)]
    [HttpPost]
    public async Task<ActionResult<InventoryHistoryState>> PostAsync([FromBody] InventoryHistoryViewModel request) =>
        await ToActionResult(async () => await Mediator.Send(Mapper.Map<AddInventoryHistoryCommand>(request)));

    [Authorize(Policy = Permission.InventoryHistory.Edit)]
    [HttpPut("{id}")]
    public async Task<ActionResult<InventoryHistoryState>> PutAsync(string id, [FromBody] InventoryHistoryViewModel request)
    {
        var command = Mapper.Map<EditInventoryHistoryCommand>(request);
        return await ToActionResult(async () => await Mediator.Send(command with { Id = id }));
    }

    [Authorize(Policy = Permission.InventoryHistory.Delete)]
    [HttpDelete("{id}")]
    public async Task<ActionResult<InventoryHistoryState>> DeleteAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new DeleteInventoryHistoryCommand { Id = id }));
}

public record InventoryHistoryViewModel
{
    [Required]
	[StringLength(400, ErrorMessage = "{0} length can't be more than {1}.")]
	public string Activity { get;set; } = "";
	
	public string? InventoryId { get;set; }
	
	[DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
	public decimal? Quantity { get;set; }
	   
}
