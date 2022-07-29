using CTI.Common.Utility.Models;
using CTI.TenantSales.Application.Features.TenantSales.Level.Commands;
using CTI.TenantSales.Application.Features.TenantSales.Level.Queries;
using CTI.TenantSales.Core.TenantSales;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using CTI.Common.API.Controllers;

namespace CTI.TenantSales.API.Controllers.v1;

[ApiVersion("1.0")]
public class LevelController : BaseApiController<LevelController>
{
    [Authorize(Policy = Permission.Level.View)]
    [HttpGet]
    public async Task<ActionResult<PagedListResponse<LevelState>>> GetAsync([FromQuery] GetLevelQuery query) =>
        Ok(await Mediator.Send(query));

    [Authorize(Policy = Permission.Level.View)]
    [HttpGet("{id}")]
    public async Task<ActionResult<LevelState>> GetAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new GetLevelByIdQuery(id)));

    [Authorize(Policy = Permission.Level.Create)]
    [HttpPost]
    public async Task<ActionResult<LevelState>> PostAsync([FromBody] LevelViewModel request) =>
        await ToActionResult(async () => await Mediator.Send(Mapper.Map<AddLevelCommand>(request)));

    [Authorize(Policy = Permission.Level.Edit)]
    [HttpPut("{id}")]
    public async Task<ActionResult<LevelState>> PutAsync(string id, [FromBody] LevelViewModel request)
    {
        var command = Mapper.Map<EditLevelCommand>(request);
        return await ToActionResult(async () => await Mediator.Send(command with { Id = id }));
    }

    [Authorize(Policy = Permission.Level.Delete)]
    [HttpDelete("{id}")]
    public async Task<ActionResult<LevelState>> DeleteAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new DeleteLevelCommand { Id = id }));
}

public record LevelViewModel
{
    [Required]
	[StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
	public string Name { get;set; } = "";
	[Required]
	
	public string ProjectId { get;set; } = "";
	[Required]
	public bool HasPercentageSalesTenant { get;set; }
	public bool IsDisabled { get;set; }
	   
}
