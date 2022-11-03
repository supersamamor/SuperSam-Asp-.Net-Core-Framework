using CTI.Common.Utility.Models;
using CTI.ELMS.Application.Features.ELMS.UnitGroup.Commands;
using CTI.ELMS.Application.Features.ELMS.UnitGroup.Queries;
using CTI.ELMS.Core.ELMS;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using CTI.Common.API.Controllers;

namespace CTI.ELMS.API.Controllers.v1;

[ApiVersion("1.0")]
public class UnitGroupController : BaseApiController<UnitGroupController>
{
    [Authorize(Policy = Permission.UnitGroup.View)]
    [HttpGet]
    public async Task<ActionResult<PagedListResponse<UnitGroupState>>> GetAsync([FromQuery] GetUnitGroupQuery query) =>
        Ok(await Mediator.Send(query));

    [Authorize(Policy = Permission.UnitGroup.View)]
    [HttpGet("{id}")]
    public async Task<ActionResult<UnitGroupState>> GetAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new GetUnitGroupByIdQuery(id)));

    [Authorize(Policy = Permission.UnitGroup.Create)]
    [HttpPost]
    public async Task<ActionResult<UnitGroupState>> PostAsync([FromBody] UnitGroupViewModel request) =>
        await ToActionResult(async () => await Mediator.Send(Mapper.Map<AddUnitGroupCommand>(request)));

    [Authorize(Policy = Permission.UnitGroup.Edit)]
    [HttpPut("{id}")]
    public async Task<ActionResult<UnitGroupState>> PutAsync(string id, [FromBody] UnitGroupViewModel request)
    {
        var command = Mapper.Map<EditUnitGroupCommand>(request);
        return await ToActionResult(async () => await Mediator.Send(command with { Id = id }));
    }

    [Authorize(Policy = Permission.UnitGroup.Delete)]
    [HttpDelete("{id}")]
    public async Task<ActionResult<UnitGroupState>> DeleteAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new DeleteUnitGroupCommand { Id = id }));
}

public record UnitGroupViewModel
{
    [Required]
	
	public string UnitsInformation { get;set; } = "";
	
	[DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
	public decimal? LotArea { get;set; }
	
	[DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
	public decimal? BasicFixedMonthlyRent { get;set; }
	
	public string? OfferingHistoryID { get;set; }
	public int? UnitOfferedHistoryID { get;set; }
	public bool IsFixedMonthlyRent { get;set; }
	[Required]
	[StringLength(50, ErrorMessage = "{0} length can't be more than {1}.")]
	public string AreaTypeDescription { get;set; } = "";
	   
}
