using CelerSoft.Common.Utility.Models;
using CelerSoft.TurboERP.Application.Features.TurboERP.ItemType.Commands;
using CelerSoft.TurboERP.Application.Features.TurboERP.ItemType.Queries;
using CelerSoft.TurboERP.Core.TurboERP;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using CelerSoft.Common.API.Controllers;

namespace CelerSoft.TurboERP.API.Controllers.v1;

[ApiVersion("1.0")]
public class ItemTypeController : BaseApiController<ItemTypeController>
{
    [Authorize(Policy = Permission.ItemType.View)]
    [HttpGet]
    public async Task<ActionResult<PagedListResponse<ItemTypeState>>> GetAsync([FromQuery] GetItemTypeQuery query) =>
        Ok(await Mediator.Send(query));

    [Authorize(Policy = Permission.ItemType.View)]
    [HttpGet("{id}")]
    public async Task<ActionResult<ItemTypeState>> GetAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new GetItemTypeByIdQuery(id)));

    [Authorize(Policy = Permission.ItemType.Create)]
    [HttpPost]
    public async Task<ActionResult<ItemTypeState>> PostAsync([FromBody] ItemTypeViewModel request) =>
        await ToActionResult(async () => await Mediator.Send(Mapper.Map<AddItemTypeCommand>(request)));

    [Authorize(Policy = Permission.ItemType.Edit)]
    [HttpPut("{id}")]
    public async Task<ActionResult<ItemTypeState>> PutAsync(string id, [FromBody] ItemTypeViewModel request)
    {
        var command = Mapper.Map<EditItemTypeCommand>(request);
        return await ToActionResult(async () => await Mediator.Send(command with { Id = id }));
    }

    [Authorize(Policy = Permission.ItemType.Delete)]
    [HttpDelete("{id}")]
    public async Task<ActionResult<ItemTypeState>> DeleteAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new DeleteItemTypeCommand { Id = id }));
}

public record ItemTypeViewModel
{
    [Required]
	[StringLength(100, ErrorMessage = "{0} length can't be more than {1}.")]
	public string Name { get;set; } = "";
	   
}
