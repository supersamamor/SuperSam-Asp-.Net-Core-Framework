using CelerSoft.Common.Utility.Models;
using CelerSoft.TurboERP.Application.Features.TurboERP.ShoppingCart.Commands;
using CelerSoft.TurboERP.Application.Features.TurboERP.ShoppingCart.Queries;
using CelerSoft.TurboERP.Core.TurboERP;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using CelerSoft.Common.API.Controllers;

namespace CelerSoft.TurboERP.API.Controllers.v1;

[ApiVersion("1.0")]
public class ShoppingCartController : BaseApiController<ShoppingCartController>
{
    [Authorize(Policy = Permission.ShoppingCart.View)]
    [HttpGet]
    public async Task<ActionResult<PagedListResponse<ShoppingCartState>>> GetAsync([FromQuery] GetShoppingCartQuery query) =>
        Ok(await Mediator.Send(query));

    [Authorize(Policy = Permission.ShoppingCart.View)]
    [HttpGet("{id}")]
    public async Task<ActionResult<ShoppingCartState>> GetAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new GetShoppingCartByIdQuery(id)));

    [Authorize(Policy = Permission.ShoppingCart.Create)]
    [HttpPost]
    public async Task<ActionResult<ShoppingCartState>> PostAsync([FromBody] ShoppingCartViewModel request) =>
        await ToActionResult(async () => await Mediator.Send(Mapper.Map<AddShoppingCartCommand>(request)));

    [Authorize(Policy = Permission.ShoppingCart.Edit)]
    [HttpPut("{id}")]
    public async Task<ActionResult<ShoppingCartState>> PutAsync(string id, [FromBody] ShoppingCartViewModel request)
    {
        var command = Mapper.Map<EditShoppingCartCommand>(request);
        return await ToActionResult(async () => await Mediator.Send(command with { Id = id }));
    }

    [Authorize(Policy = Permission.ShoppingCart.Delete)]
    [HttpDelete("{id}")]
    public async Task<ActionResult<ShoppingCartState>> DeleteAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new DeleteShoppingCartCommand { Id = id }));
}

public record ShoppingCartViewModel
{
    
	[DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
	public decimal? Amount { get;set; }
	
	public string? InventoryId { get;set; }
	public bool IsCheckOut { get;set; }
	
	[DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
	public decimal? Quantity { get;set; }
	[Required]
	[StringLength(450, ErrorMessage = "{0} length can't be more than {1}.")]
	public string ShopperUsername { get;set; } = "";
	
	[DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
	public decimal? TotalAmount { get;set; }
	   
}
