using CTI.Common.Utility.Models;
using CTI.ELMS.Application.Features.ELMS.BusinessNatureSubItem.Commands;
using CTI.ELMS.Application.Features.ELMS.BusinessNatureSubItem.Queries;
using CTI.ELMS.Core.ELMS;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using CTI.Common.API.Controllers;

namespace CTI.ELMS.API.Controllers.v1;

[ApiVersion("1.0")]
public class BusinessNatureSubItemController : BaseApiController<BusinessNatureSubItemController>
{
    [Authorize(Policy = Permission.BusinessNatureSubItem.View)]
    [HttpGet]
    public async Task<ActionResult<PagedListResponse<BusinessNatureSubItemState>>> GetAsync([FromQuery] GetBusinessNatureSubItemQuery query) =>
        Ok(await Mediator.Send(query));

    [Authorize(Policy = Permission.BusinessNatureSubItem.View)]
    [HttpGet("{id}")]
    public async Task<ActionResult<BusinessNatureSubItemState>> GetAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new GetBusinessNatureSubItemByIdQuery(id)));

    [Authorize(Policy = Permission.BusinessNatureSubItem.Create)]
    [HttpPost]
    public async Task<ActionResult<BusinessNatureSubItemState>> PostAsync([FromBody] BusinessNatureSubItemViewModel request) =>
        await ToActionResult(async () => await Mediator.Send(Mapper.Map<AddBusinessNatureSubItemCommand>(request)));

    [Authorize(Policy = Permission.BusinessNatureSubItem.Edit)]
    [HttpPut("{id}")]
    public async Task<ActionResult<BusinessNatureSubItemState>> PutAsync(string id, [FromBody] BusinessNatureSubItemViewModel request)
    {
        var command = Mapper.Map<EditBusinessNatureSubItemCommand>(request);
        return await ToActionResult(async () => await Mediator.Send(command with { Id = id }));
    }

    [Authorize(Policy = Permission.BusinessNatureSubItem.Delete)]
    [HttpDelete("{id}")]
    public async Task<ActionResult<BusinessNatureSubItemState>> DeleteAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new DeleteBusinessNatureSubItemCommand { Id = id }));
}

public record BusinessNatureSubItemViewModel
{
    [Required]
	
	public string BusinessNatureSubItemName { get;set; } = "";
	[Required]
	
	public string BusinessNatureID { get;set; } = "";
	public bool IsDisabled { get;set; }
	[Required]
	
	public string BusinessNatureSubItemCode { get;set; } = "";
	   
}
