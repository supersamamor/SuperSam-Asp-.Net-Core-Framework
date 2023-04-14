using CelerSoft.Common.Utility.Models;
using CelerSoft.TurboERP.Application.Features.TurboERP.Product.Commands;
using CelerSoft.TurboERP.Application.Features.TurboERP.Product.Queries;
using CelerSoft.TurboERP.Core.TurboERP;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using CelerSoft.Common.API.Controllers;

namespace CelerSoft.TurboERP.API.Controllers.v1;

[ApiVersion("1.0")]
public class ProductController : BaseApiController<ProductController>
{
    [Authorize(Policy = Permission.Product.View)]
    [HttpGet]
    public async Task<ActionResult<PagedListResponse<ProductState>>> GetAsync([FromQuery] GetProductQuery query) =>
        Ok(await Mediator.Send(query));

    [Authorize(Policy = Permission.Product.View)]
    [HttpGet("{id}")]
    public async Task<ActionResult<ProductState>> GetAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new GetProductByIdQuery(id)));

    [Authorize(Policy = Permission.Product.Create)]
    [HttpPost]
    public async Task<ActionResult<ProductState>> PostAsync([FromBody] ProductViewModel request) =>
        await ToActionResult(async () => await Mediator.Send(Mapper.Map<AddProductCommand>(request)));

    [Authorize(Policy = Permission.Product.Edit)]
    [HttpPut("{id}")]
    public async Task<ActionResult<ProductState>> PutAsync(string id, [FromBody] ProductViewModel request)
    {
        var command = Mapper.Map<EditProductCommand>(request);
        return await ToActionResult(async () => await Mediator.Send(command with { Id = id }));
    }

    [Authorize(Policy = Permission.Product.Delete)]
    [HttpDelete("{id}")]
    public async Task<ActionResult<ProductState>> DeleteAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new DeleteProductCommand { Id = id }));
}

public record ProductViewModel
{
    [Required]
	
	public string ItemId { get;set; } = "";
	
	public string? BrandId { get;set; }
	[StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
	public string? Model { get;set; }
	[StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
	public string? Description { get;set; }
	
	[DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
	public decimal? MinimumQuantity { get;set; }
	[StringLength(450, ErrorMessage = "{0} length can't be more than {1}.")]
	public string? BarcodeNumber { get;set; }
	   
}
