using CTI.Common.Utility.Models;
using CTI.TenantSales.Application.Features.TenantSales.Tenant.Commands;
using CTI.TenantSales.Application.Features.TenantSales.Tenant.Queries;
using CTI.TenantSales.Core.TenantSales;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using CTI.Common.API.Controllers;

namespace CTI.TenantSales.API.Controllers.v1;

[ApiVersion("1.0")]
public class TenantController : BaseApiController<TenantController>
{
    [Authorize(Policy = Permission.Tenant.View)]
    [HttpGet]
    public async Task<ActionResult<PagedListResponse<TenantState>>> GetAsync([FromQuery] GetTenantQuery query) =>
        Ok(await Mediator.Send(query));

    [Authorize(Policy = Permission.Tenant.View)]
    [HttpGet("{id}")]
    public async Task<ActionResult<TenantState>> GetAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new GetTenantByIdQuery(id)));

    [Authorize(Policy = Permission.Tenant.Create)]
    [HttpPost]
    public async Task<ActionResult<TenantState>> PostAsync([FromBody] TenantViewModel request) =>
        await ToActionResult(async () => await Mediator.Send(Mapper.Map<AddTenantCommand>(request)));

    [Authorize(Policy = Permission.Tenant.Edit)]
    [HttpPut("{id}")]
    public async Task<ActionResult<TenantState>> PutAsync(string id, [FromBody] TenantViewModel request)
    {
        var command = Mapper.Map<EditTenantCommand>(request);
        return await ToActionResult(async () => await Mediator.Send(command with { Id = id }));
    }

    [Authorize(Policy = Permission.Tenant.Delete)]
    [HttpDelete("{id}")]
    public async Task<ActionResult<TenantState>> DeleteAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new DeleteTenantCommand { Id = id }));
}

public record TenantViewModel
{
    [Required]
	
	public string RentalTypeId { get;set; } = "";
	[Required]
	
	public string ProjectId { get;set; } = "";
	[Required]
	[StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
	public string Name { get;set; } = "";
	[Required]
	[StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
	public string Code { get;set; } = "";
	[StringLength(20, ErrorMessage = "{0} length can't be more than {1}.")]
	public string? FileCode { get;set; }
	[StringLength(20, ErrorMessage = "{0} length can't be more than {1}.")]
	public string? Folder { get;set; }
	[Required]
	public DateTime DateFrom { get;set; } = DateTime.Now.Date;
	[Required]
	public DateTime DateTo { get;set; } = DateTime.Now.Date;
	[Required]
	public DateTime Opening { get;set; } = DateTime.Now.Date;
	
	public string? LevelId { get;set; }
	public bool IsDisabled { get;set; }
	[StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
	public string? BranchContact { get;set; }
	[StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
	public string? HeadOfficeContact { get;set; }
	[StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
	public string? ITSupportContact { get;set; }
	   
}
