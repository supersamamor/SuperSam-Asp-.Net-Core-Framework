using CTI.Common.Utility.Models;
using CTI.TenantSales.Application.Features.TenantSales.Category.Commands;
using CTI.TenantSales.Application.Features.TenantSales.Category.Queries;
using CTI.TenantSales.Core.TenantSales;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using CTI.Common.API.Controllers;

namespace CTI.TenantSales.API.Controllers.v1;

[ApiVersion("1.0")]
public class CategoryController : BaseApiController<CategoryController>
{
    [Authorize(Policy = Permission.Category.View)]
    [HttpGet]
    public async Task<ActionResult<PagedListResponse<CategoryState>>> GetAsync([FromQuery] GetCategoryQuery query) =>
        Ok(await Mediator.Send(query));

    [Authorize(Policy = Permission.Category.View)]
    [HttpGet("{id}")]
    public async Task<ActionResult<CategoryState>> GetAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new GetCategoryByIdQuery(id)));

    [Authorize(Policy = Permission.Category.Create)]
    [HttpPost]
    public async Task<ActionResult<CategoryState>> PostAsync([FromBody] CategoryViewModel request) =>
        await ToActionResult(async () => await Mediator.Send(Mapper.Map<AddCategoryCommand>(request)));

    [Authorize(Policy = Permission.Category.Edit)]
    [HttpPut("{id}")]
    public async Task<ActionResult<CategoryState>> PutAsync(string id, [FromBody] CategoryViewModel request)
    {
        var command = Mapper.Map<EditCategoryCommand>(request);
        return await ToActionResult(async () => await Mediator.Send(command with { Id = id }));
    }

    [Authorize(Policy = Permission.Category.Delete)]
    [HttpDelete("{id}")]
    public async Task<ActionResult<CategoryState>> DeleteAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new DeleteCategoryCommand { Id = id }));
}

public record CategoryViewModel
{
    [Required]
	[StringLength(80, ErrorMessage = "{0} length can't be more than {1}.")]
	public string Name { get;set; } = "";
	[Required]
	
	public string ClassificationId { get;set; } = "";
	[Required]
	[StringLength(15, ErrorMessage = "{0} length can't be more than {1}.")]
	public string Code { get;set; } = "";
	   
}
