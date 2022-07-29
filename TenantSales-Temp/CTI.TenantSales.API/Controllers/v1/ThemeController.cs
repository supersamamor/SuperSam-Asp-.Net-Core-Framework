using CTI.Common.Utility.Models;
using CTI.TenantSales.Application.Features.TenantSales.Theme.Commands;
using CTI.TenantSales.Application.Features.TenantSales.Theme.Queries;
using CTI.TenantSales.Core.TenantSales;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using CTI.Common.API.Controllers;

namespace CTI.TenantSales.API.Controllers.v1;

[ApiVersion("1.0")]
public class ThemeController : BaseApiController<ThemeController>
{
    [Authorize(Policy = Permission.Theme.View)]
    [HttpGet]
    public async Task<ActionResult<PagedListResponse<ThemeState>>> GetAsync([FromQuery] GetThemeQuery query) =>
        Ok(await Mediator.Send(query));

    [Authorize(Policy = Permission.Theme.View)]
    [HttpGet("{id}")]
    public async Task<ActionResult<ThemeState>> GetAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new GetThemeByIdQuery(id)));

    [Authorize(Policy = Permission.Theme.Create)]
    [HttpPost]
    public async Task<ActionResult<ThemeState>> PostAsync([FromBody] ThemeViewModel request) =>
        await ToActionResult(async () => await Mediator.Send(Mapper.Map<AddThemeCommand>(request)));

    [Authorize(Policy = Permission.Theme.Edit)]
    [HttpPut("{id}")]
    public async Task<ActionResult<ThemeState>> PutAsync(string id, [FromBody] ThemeViewModel request)
    {
        var command = Mapper.Map<EditThemeCommand>(request);
        return await ToActionResult(async () => await Mediator.Send(command with { Id = id }));
    }

    [Authorize(Policy = Permission.Theme.Delete)]
    [HttpDelete("{id}")]
    public async Task<ActionResult<ThemeState>> DeleteAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new DeleteThemeCommand { Id = id }));
}

public record ThemeViewModel
{
    [Required]
	[StringLength(40, ErrorMessage = "{0} length can't be more than {1}.")]
	public string Name { get;set; } = "";
	[Required]
	[StringLength(15, ErrorMessage = "{0} length can't be more than {1}.")]
	public string Code { get;set; } = "";
	   
}
