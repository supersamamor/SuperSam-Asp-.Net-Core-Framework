using CTI.Common.Utility.Models;
using CTI.ELMS.Application.Features.ELMS.UnitOffered.Commands;
using CTI.ELMS.Application.Features.ELMS.UnitOffered.Queries;
using CTI.ELMS.Core.ELMS;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using CTI.Common.API.Controllers;

namespace CTI.ELMS.API.Controllers.v1;

[ApiVersion("1.0")]
public class UnitOfferedController : BaseApiController<UnitOfferedController>
{
    [Authorize(Policy = Permission.UnitOffered.View)]
    [HttpGet]
    public async Task<ActionResult<PagedListResponse<UnitOfferedState>>> GetAsync([FromQuery] GetUnitOfferedQuery query) =>
        Ok(await Mediator.Send(query));

    [Authorize(Policy = Permission.UnitOffered.View)]
    [HttpGet("{id}")]
    public async Task<ActionResult<UnitOfferedState>> GetAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new GetUnitOfferedByIdQuery(id)));

    [Authorize(Policy = Permission.UnitOffered.Create)]
    [HttpPost]
    public async Task<ActionResult<UnitOfferedState>> PostAsync([FromBody] UnitOfferedViewModel request) =>
        await ToActionResult(async () => await Mediator.Send(Mapper.Map<AddUnitOfferedCommand>(request)));

    [Authorize(Policy = Permission.UnitOffered.Edit)]
    [HttpPut("{id}")]
    public async Task<ActionResult<UnitOfferedState>> PutAsync(string id, [FromBody] UnitOfferedViewModel request)
    {
        var command = Mapper.Map<EditUnitOfferedCommand>(request);
        return await ToActionResult(async () => await Mediator.Send(command with { Id = id }));
    }

    [Authorize(Policy = Permission.UnitOffered.Delete)]
    [HttpDelete("{id}")]
    public async Task<ActionResult<UnitOfferedState>> DeleteAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new DeleteUnitOfferedCommand { Id = id }));
}

public record UnitOfferedViewModel
{
    
	[DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
	public decimal? LotBudget { get;set; }
	
	[DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
	public decimal? LotArea { get;set; }
	
	public string? OfferingID { get;set; }
	
	public string? UnitID { get;set; }
	
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
