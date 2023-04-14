using CelerSoft.Common.Utility.Models;
using CelerSoft.TurboERP.Application.Features.TurboERP.Order.Commands;
using CelerSoft.TurboERP.Application.Features.TurboERP.Order.Queries;
using CelerSoft.TurboERP.Core.TurboERP;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using CelerSoft.Common.API.Controllers;

namespace CelerSoft.TurboERP.API.Controllers.v1;

[ApiVersion("1.0")]
public class OrderController : BaseApiController<OrderController>
{
    [Authorize(Policy = Permission.Order.View)]
    [HttpGet]
    public async Task<ActionResult<PagedListResponse<OrderState>>> GetAsync([FromQuery] GetOrderQuery query) =>
        Ok(await Mediator.Send(query));

    [Authorize(Policy = Permission.Order.View)]
    [HttpGet("{id}")]
    public async Task<ActionResult<OrderState>> GetAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new GetOrderByIdQuery(id)));

    [Authorize(Policy = Permission.Order.Create)]
    [HttpPost]
    public async Task<ActionResult<OrderState>> PostAsync([FromBody] OrderViewModel request) =>
        await ToActionResult(async () => await Mediator.Send(Mapper.Map<AddOrderCommand>(request)));

    [Authorize(Policy = Permission.Order.Edit)]
    [HttpPut("{id}")]
    public async Task<ActionResult<OrderState>> PutAsync(string id, [FromBody] OrderViewModel request)
    {
        var command = Mapper.Map<EditOrderCommand>(request);
        return await ToActionResult(async () => await Mediator.Send(command with { Id = id }));
    }

    [Authorize(Policy = Permission.Order.Delete)]
    [HttpDelete("{id}")]
    public async Task<ActionResult<OrderState>> DeleteAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new DeleteOrderCommand { Id = id }));
}

public record OrderViewModel
{
    [Required]
	[StringLength(400, ErrorMessage = "{0} length can't be more than {1}.")]
	public string CheckedByFullName { get;set; } = "";
	[StringLength(20, ErrorMessage = "{0} length can't be more than {1}.")]
	public string? Code { get;set; }
	[Required]
	
	public string CustomerId { get;set; } = "";
	[Required]
	[StringLength(500, ErrorMessage = "{0} length can't be more than {1}.")]
	public string Remarks { get;set; } = "";
	[Required]
	[StringLength(450, ErrorMessage = "{0} length can't be more than {1}.")]
	public string ShopperUsername { get;set; } = "";
	[StringLength(15, ErrorMessage = "{0} length can't be more than {1}.")]
	public string? Status { get;set; }
	   
}
