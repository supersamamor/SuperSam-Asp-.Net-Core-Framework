using CelerSoft.Common.Utility.Models;
using CelerSoft.TurboERP.Application.Features.TurboERP.Item.Commands;
using CelerSoft.TurboERP.Application.Features.TurboERP.Item.Queries;
using CelerSoft.TurboERP.Core.TurboERP;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using CelerSoft.Common.API.Controllers;

namespace CelerSoft.TurboERP.API.Controllers.v1;

[ApiVersion("1.0")]
public class ItemController : BaseApiController<ItemController>
{
    [Authorize(Policy = Permission.Item.View)]
    [HttpGet]
    public async Task<ActionResult<PagedListResponse<ItemState>>> GetAsync([FromQuery] GetItemQuery query) =>
        Ok(await Mediator.Send(query));

    [Authorize(Policy = Permission.Item.View)]
    [HttpGet("{id}")]
    public async Task<ActionResult<ItemState>> GetAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new GetItemByIdQuery(id)));

    [Authorize(Policy = Permission.Item.Create)]
    [HttpPost]
    public async Task<ActionResult<ItemState>> PostAsync([FromBody] ItemViewModel request) =>
        await ToActionResult(async () => await Mediator.Send(Mapper.Map<AddItemCommand>(request)));

    [Authorize(Policy = Permission.Item.Edit)]
    [HttpPut("{id}")]
    public async Task<ActionResult<ItemState>> PutAsync(string id, [FromBody] ItemViewModel request)
    {
        var command = Mapper.Map<EditItemCommand>(request);
        return await ToActionResult(async () => await Mediator.Send(command with { Id = id }));
    }

    [Authorize(Policy = Permission.Item.Delete)]
    [HttpDelete("{id}")]
    public async Task<ActionResult<ItemState>> DeleteAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new DeleteItemCommand { Id = id }));
}

public record ItemViewModel
{
    [Required]
	
	public string ItemTypeId { get;set; } = "";
	[StringLength(20, ErrorMessage = "{0} length can't be more than {1}.")]
	public string? Code { get;set; }
	[Required]
	[StringLength(500, ErrorMessage = "{0} length can't be more than {1}.")]
	public string Name { get;set; } = "";
	[Required]
	
	public string UnitId { get;set; } = "";
	
	[DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
	public decimal? AveragePrice { get;set; }
	
	[DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
	public decimal? LastPurchasedPrice { get;set; }
	   
}
