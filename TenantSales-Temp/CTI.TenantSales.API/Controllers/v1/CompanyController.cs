using CTI.Common.Utility.Models;
using CTI.TenantSales.Application.Features.TenantSales.Company.Commands;
using CTI.TenantSales.Application.Features.TenantSales.Company.Queries;
using CTI.TenantSales.Core.TenantSales;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using CTI.Common.API.Controllers;

namespace CTI.TenantSales.API.Controllers.v1;

[ApiVersion("1.0")]
public class CompanyController : BaseApiController<CompanyController>
{
    [Authorize(Policy = Permission.Company.View)]
    [HttpGet]
    public async Task<ActionResult<PagedListResponse<CompanyState>>> GetAsync([FromQuery] GetCompanyQuery query) =>
        Ok(await Mediator.Send(query));

    [Authorize(Policy = Permission.Company.View)]
    [HttpGet("{id}")]
    public async Task<ActionResult<CompanyState>> GetAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new GetCompanyByIdQuery(id)));

    [Authorize(Policy = Permission.Company.Create)]
    [HttpPost]
    public async Task<ActionResult<CompanyState>> PostAsync([FromBody] CompanyViewModel request) =>
        await ToActionResult(async () => await Mediator.Send(Mapper.Map<AddCompanyCommand>(request)));

    [Authorize(Policy = Permission.Company.Edit)]
    [HttpPut("{id}")]
    public async Task<ActionResult<CompanyState>> PutAsync(string id, [FromBody] CompanyViewModel request)
    {
        var command = Mapper.Map<EditCompanyCommand>(request);
        return await ToActionResult(async () => await Mediator.Send(command with { Id = id }));
    }

    [Authorize(Policy = Permission.Company.Delete)]
    [HttpDelete("{id}")]
    public async Task<ActionResult<CompanyState>> DeleteAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new DeleteCompanyCommand { Id = id }));
}

public record CompanyViewModel
{
    [Required]
	[StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
	public string Name { get;set; } = "";
	[Required]
	[StringLength(5, ErrorMessage = "{0} length can't be more than {1}.")]
	public string Code { get;set; } = "";
	[StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
	public string? EntityAddress { get;set; }
	[StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
	public string? EntityAddressSecondLine { get;set; }
	[StringLength(100, ErrorMessage = "{0} length can't be more than {1}.")]
	public string? EntityDescription { get;set; }
	[StringLength(20, ErrorMessage = "{0} length can't be more than {1}.")]
	public string? EntityShortName { get;set; }
	public bool IsDisabled { get;set; }
	[StringLength(17, ErrorMessage = "{0} length can't be more than {1}.")]
	public string? TINNo { get;set; }
	[Required]
	
	public string DatabaseConnectionSetupId { get;set; } = "";
	   
}
