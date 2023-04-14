using CelerSoft.Common.Utility.Models;
using CelerSoft.TurboERP.Application.Features.TurboERP.Unit.Commands;
using CelerSoft.TurboERP.Application.Features.TurboERP.Unit.Queries;
using CelerSoft.TurboERP.Core.TurboERP;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using CelerSoft.Common.API.Controllers;

namespace CelerSoft.TurboERP.API.Controllers.v1;

[ApiVersion("1.0")]
public class UnitController : BaseApiController<UnitController>
{
    [Authorize(Policy = Permission.Unit.View)]
    [HttpGet]
    public async Task<ActionResult<PagedListResponse<UnitState>>> GetAsync([FromQuery] GetUnitQuery query) =>
        Ok(await Mediator.Send(query));

    [Authorize(Policy = Permission.Unit.View)]
    [HttpGet("{id}")]
    public async Task<ActionResult<UnitState>> GetAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new GetUnitByIdQuery(id)));

    [Authorize(Policy = Permission.Unit.Create)]
    [HttpPost]
    public async Task<ActionResult<UnitState>> PostAsync([FromBody] UnitViewModel request) =>
        await ToActionResult(async () => await Mediator.Send(Mapper.Map<AddUnitCommand>(request)));

    [Authorize(Policy = Permission.Unit.Edit)]
    [HttpPut("{id}")]
    public async Task<ActionResult<UnitState>> PutAsync(string id, [FromBody] UnitViewModel request)
    {
        var command = Mapper.Map<EditUnitCommand>(request);
        return await ToActionResult(async () => await Mediator.Send(command with { Id = id }));
    }

    [Authorize(Policy = Permission.Unit.Delete)]
    [HttpDelete("{id}")]
    public async Task<ActionResult<UnitState>> DeleteAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new DeleteUnitCommand { Id = id }));
}

public record UnitViewModel
{
    [Required]
	[StringLength(5, ErrorMessage = "{0} length can't be more than {1}.")]
	public string Abbreviations { get;set; } = "";
	[Required]
	[StringLength(100, ErrorMessage = "{0} length can't be more than {1}.")]
	public string Name { get;set; } = "";
	   
}
