using CTI.Common.Utility.Models;
using CTI.TenantSales.Application.Features.TenantSales.SalesCategory.Commands;
using CTI.TenantSales.Application.Features.TenantSales.SalesCategory.Queries;
using CTI.TenantSales.Core.TenantSales;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using CTI.Common.API.Controllers;

namespace CTI.TenantSales.API.Controllers.v1;

[ApiVersion("1.0")]
public class SalesCategoryController : BaseApiController<SalesCategoryController>
{
    [Authorize(Policy = Permission.SalesCategory.View)]
    [HttpGet]
    public async Task<ActionResult<PagedListResponse<SalesCategoryState>>> GetAsync([FromQuery] GetSalesCategoryQuery query) =>
        Ok(await Mediator.Send(query));

    [Authorize(Policy = Permission.SalesCategory.View)]
    [HttpGet("{id}")]
    public async Task<ActionResult<SalesCategoryState>> GetAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new GetSalesCategoryByIdQuery(id)));

    [Authorize(Policy = Permission.SalesCategory.Create)]
    [HttpPost]
    public async Task<ActionResult<SalesCategoryState>> PostAsync([FromBody] SalesCategoryViewModel request) =>
        await ToActionResult(async () => await Mediator.Send(Mapper.Map<AddSalesCategoryCommand>(request)));

    [Authorize(Policy = Permission.SalesCategory.Edit)]
    [HttpPut("{id}")]
    public async Task<ActionResult<SalesCategoryState>> PutAsync(string id, [FromBody] SalesCategoryViewModel request)
    {
        var command = Mapper.Map<EditSalesCategoryCommand>(request);
        return await ToActionResult(async () => await Mediator.Send(command with { Id = id }));
    }

    [Authorize(Policy = Permission.SalesCategory.Delete)]
    [HttpDelete("{id}")]
    public async Task<ActionResult<SalesCategoryState>> DeleteAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new DeleteSalesCategoryCommand { Id = id }));
}

public record SalesCategoryViewModel
{
    [Required]
	[StringLength(20, ErrorMessage = "{0} length can't be more than {1}.")]
	public string Code { get;set; } = "";
	[Required]
	[StringLength(100, ErrorMessage = "{0} length can't be more than {1}.")]
	public string Name { get;set; } = "";
	[Required]
	
	[DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
	public decimal Rate { get;set; }
	[Required]
	
	public string TenantId { get;set; } = "";
	public bool IsDisabled { get;set; }
	   
}
