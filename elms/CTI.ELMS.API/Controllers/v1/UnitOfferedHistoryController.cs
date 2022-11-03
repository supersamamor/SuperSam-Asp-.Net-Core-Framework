using CTI.Common.Utility.Models;
using CTI.ELMS.Application.Features.ELMS.UnitOfferedHistory.Commands;
using CTI.ELMS.Application.Features.ELMS.UnitOfferedHistory.Queries;
using CTI.ELMS.Core.ELMS;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using CTI.Common.API.Controllers;

namespace CTI.ELMS.API.Controllers.v1;

[ApiVersion("1.0")]
public class UnitOfferedHistoryController : BaseApiController<UnitOfferedHistoryController>
{
    [Authorize(Policy = Permission.UnitOfferedHistory.View)]
    [HttpGet]
    public async Task<ActionResult<PagedListResponse<UnitOfferedHistoryState>>> GetAsync([FromQuery] GetUnitOfferedHistoryQuery query) =>
        Ok(await Mediator.Send(query));

    [Authorize(Policy = Permission.UnitOfferedHistory.View)]
    [HttpGet("{id}")]
    public async Task<ActionResult<UnitOfferedHistoryState>> GetAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new GetUnitOfferedHistoryByIdQuery(id)));

    [Authorize(Policy = Permission.UnitOfferedHistory.Create)]
    [HttpPost]
    public async Task<ActionResult<UnitOfferedHistoryState>> PostAsync([FromBody] UnitOfferedHistoryViewModel request) =>
        await ToActionResult(async () => await Mediator.Send(Mapper.Map<AddUnitOfferedHistoryCommand>(request)));

    [Authorize(Policy = Permission.UnitOfferedHistory.Edit)]
    [HttpPut("{id}")]
    public async Task<ActionResult<UnitOfferedHistoryState>> PutAsync(string id, [FromBody] UnitOfferedHistoryViewModel request)
    {
        var command = Mapper.Map<EditUnitOfferedHistoryCommand>(request);
        return await ToActionResult(async () => await Mediator.Send(command with { Id = id }));
    }

    [Authorize(Policy = Permission.UnitOfferedHistory.Delete)]
    [HttpDelete("{id}")]
    public async Task<ActionResult<UnitOfferedHistoryState>> DeleteAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new DeleteUnitOfferedHistoryCommand { Id = id }));
}

public record UnitOfferedHistoryViewModel
{
    
	public string? OfferingID { get;set; }
	
	public string? UnitID { get;set; }
	
	[DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
	public decimal? LotBudget { get;set; }
	
	[DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
	public decimal? LotArea { get;set; }
	
	public string? OfferingHistoryID { get;set; }
	
	[DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
	public decimal? BasicFixedMonthlyRent { get;set; }
	
	[DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
	public decimal? PercentageRent { get;set; }
	
	[DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
	public decimal? MinimumMonthlyRent { get;set; }
	
	[DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
	public decimal? AnnualIncrement { get;set; }
	[Required]
	
	public string AnnualIncrementInformation { get;set; } = "";
	public bool IsFixedMonthlyRent { get;set; }
	   
}
