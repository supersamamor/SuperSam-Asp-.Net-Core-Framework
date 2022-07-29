using CTI.Common.Utility.Models;
using CTI.TenantSales.Application.Features.TenantSales.DatabaseConnectionSetup.Commands;
using CTI.TenantSales.Application.Features.TenantSales.DatabaseConnectionSetup.Queries;
using CTI.TenantSales.Core.TenantSales;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using CTI.Common.API.Controllers;

namespace CTI.TenantSales.API.Controllers.v1;

[ApiVersion("1.0")]
public class DatabaseConnectionSetupController : BaseApiController<DatabaseConnectionSetupController>
{
    [Authorize(Policy = Permission.DatabaseConnectionSetup.View)]
    [HttpGet]
    public async Task<ActionResult<PagedListResponse<DatabaseConnectionSetupState>>> GetAsync([FromQuery] GetDatabaseConnectionSetupQuery query) =>
        Ok(await Mediator.Send(query));

    [Authorize(Policy = Permission.DatabaseConnectionSetup.View)]
    [HttpGet("{id}")]
    public async Task<ActionResult<DatabaseConnectionSetupState>> GetAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new GetDatabaseConnectionSetupByIdQuery(id)));

    [Authorize(Policy = Permission.DatabaseConnectionSetup.Create)]
    [HttpPost]
    public async Task<ActionResult<DatabaseConnectionSetupState>> PostAsync([FromBody] DatabaseConnectionSetupViewModel request) =>
        await ToActionResult(async () => await Mediator.Send(Mapper.Map<AddDatabaseConnectionSetupCommand>(request)));

    [Authorize(Policy = Permission.DatabaseConnectionSetup.Edit)]
    [HttpPut("{id}")]
    public async Task<ActionResult<DatabaseConnectionSetupState>> PutAsync(string id, [FromBody] DatabaseConnectionSetupViewModel request)
    {
        var command = Mapper.Map<EditDatabaseConnectionSetupCommand>(request);
        return await ToActionResult(async () => await Mediator.Send(command with { Id = id }));
    }

    [Authorize(Policy = Permission.DatabaseConnectionSetup.Delete)]
    [HttpDelete("{id}")]
    public async Task<ActionResult<DatabaseConnectionSetupState>> DeleteAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new DeleteDatabaseConnectionSetupCommand { Id = id }));
}

public record DatabaseConnectionSetupViewModel
{
    [Required]
	[StringLength(20, ErrorMessage = "{0} length can't be more than {1}.")]
	public string Code { get;set; } = "";
	[Required]
	[StringLength(100, ErrorMessage = "{0} length can't be more than {1}.")]
	public string Name { get;set; } = "";
	[Required]
	[StringLength(100, ErrorMessage = "{0} length can't be more than {1}.")]
	public string DatabaseAndServerName { get;set; } = "";
	[StringLength(100, ErrorMessage = "{0} length can't be more than {1}.")]
	public string? InhouseDatabaseAndServerName { get;set; }
	[StringLength(1000, ErrorMessage = "{0} length can't be more than {1}.")]
	public string? SystemConnectionString { get;set; }
	[Required]
	
	[DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
	public int SystemSource { get;set; }
	[StringLength(1000, ErrorMessage = "{0} length can't be more than {1}.")]
	public string? ExhibitThemeCodes { get;set; }
	public bool IsDisabled { get;set; }
	   
}
