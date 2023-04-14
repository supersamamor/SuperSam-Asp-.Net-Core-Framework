using CelerSoft.Common.Utility.Models;
using CelerSoft.TurboERP.Application.Features.TurboERP.ProductImage.Commands;
using CelerSoft.TurboERP.Application.Features.TurboERP.ProductImage.Queries;
using CelerSoft.TurboERP.Core.TurboERP;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using CelerSoft.Common.API.Controllers;

namespace CelerSoft.TurboERP.API.Controllers.v1;

[ApiVersion("1.0")]
public class ProductImageController : BaseApiController<ProductImageController>
{
    [Authorize(Policy = Permission.ProductImage.View)]
    [HttpGet]
    public async Task<ActionResult<PagedListResponse<ProductImageState>>> GetAsync([FromQuery] GetProductImageQuery query) =>
        Ok(await Mediator.Send(query));

    [Authorize(Policy = Permission.ProductImage.View)]
    [HttpGet("{id}")]
    public async Task<ActionResult<ProductImageState>> GetAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new GetProductImageByIdQuery(id)));

    [Authorize(Policy = Permission.ProductImage.Create)]
    [HttpPost]
    public async Task<ActionResult<ProductImageState>> PostAsync([FromBody] ProductImageViewModel request) =>
        await ToActionResult(async () => await Mediator.Send(Mapper.Map<AddProductImageCommand>(request)));

    [Authorize(Policy = Permission.ProductImage.Edit)]
    [HttpPut("{id}")]
    public async Task<ActionResult<ProductImageState>> PutAsync(string id, [FromBody] ProductImageViewModel request)
    {
        var command = Mapper.Map<EditProductImageCommand>(request);
        return await ToActionResult(async () => await Mediator.Send(command with { Id = id }));
    }

    [Authorize(Policy = Permission.ProductImage.Delete)]
    [HttpDelete("{id}")]
    public async Task<ActionResult<ProductImageState>> DeleteAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new DeleteProductImageCommand { Id = id }));
}

public record ProductImageViewModel
{
    
	public string? ProductId { get;set; }
	[Required]
	public string? Path { get;set; } = "";
	   
}
