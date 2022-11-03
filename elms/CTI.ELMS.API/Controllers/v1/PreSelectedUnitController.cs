using CTI.Common.Utility.Models;
using CTI.ELMS.Application.Features.ELMS.PreSelectedUnit.Commands;
using CTI.ELMS.Application.Features.ELMS.PreSelectedUnit.Queries;
using CTI.ELMS.Core.ELMS;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using CTI.Common.API.Controllers;

namespace CTI.ELMS.API.Controllers.v1;

[ApiVersion("1.0")]
public class PreSelectedUnitController : BaseApiController<PreSelectedUnitController>
{
    [Authorize(Policy = Permission.PreSelectedUnit.View)]
    [HttpGet]
    public async Task<ActionResult<PagedListResponse<PreSelectedUnitState>>> GetAsync([FromQuery] GetPreSelectedUnitQuery query) =>
        Ok(await Mediator.Send(query));

    [Authorize(Policy = Permission.PreSelectedUnit.View)]
    [HttpGet("{id}")]
    public async Task<ActionResult<PreSelectedUnitState>> GetAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new GetPreSelectedUnitByIdQuery(id)));

    [Authorize(Policy = Permission.PreSelectedUnit.Create)]
    [HttpPost]
    public async Task<ActionResult<PreSelectedUnitState>> PostAsync([FromBody] PreSelectedUnitViewModel request) =>
        await ToActionResult(async () => await Mediator.Send(Mapper.Map<AddPreSelectedUnitCommand>(request)));

    [Authorize(Policy = Permission.PreSelectedUnit.Edit)]
    [HttpPut("{id}")]
    public async Task<ActionResult<PreSelectedUnitState>> PutAsync(string id, [FromBody] PreSelectedUnitViewModel request)
    {
        var command = Mapper.Map<EditPreSelectedUnitCommand>(request);
        return await ToActionResult(async () => await Mediator.Send(command with { Id = id }));
    }

    [Authorize(Policy = Permission.PreSelectedUnit.Delete)]
    [HttpDelete("{id}")]
    public async Task<ActionResult<PreSelectedUnitState>> DeleteAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new DeletePreSelectedUnitCommand { Id = id }));
}

public record PreSelectedUnitViewModel
{
    
	public string? OfferingID { get;set; }
	
	public string? UnitID { get;set; }
	
	[DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
	public decimal? LotBudget { get;set; }
	
	[DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
	public decimal? LotArea { get;set; }
	   
}
