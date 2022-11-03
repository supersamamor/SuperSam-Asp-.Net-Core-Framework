using CTI.Common.Utility.Models;
using CTI.ELMS.Application.Features.ELMS.AnnualIncrementHistory.Commands;
using CTI.ELMS.Application.Features.ELMS.AnnualIncrementHistory.Queries;
using CTI.ELMS.Core.ELMS;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using CTI.Common.API.Controllers;

namespace CTI.ELMS.API.Controllers.v1;

[ApiVersion("1.0")]
public class AnnualIncrementHistoryController : BaseApiController<AnnualIncrementHistoryController>
{
    [Authorize(Policy = Permission.AnnualIncrementHistory.View)]
    [HttpGet]
    public async Task<ActionResult<PagedListResponse<AnnualIncrementHistoryState>>> GetAsync([FromQuery] GetAnnualIncrementHistoryQuery query) =>
        Ok(await Mediator.Send(query));

    [Authorize(Policy = Permission.AnnualIncrementHistory.View)]
    [HttpGet("{id}")]
    public async Task<ActionResult<AnnualIncrementHistoryState>> GetAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new GetAnnualIncrementHistoryByIdQuery(id)));

    [Authorize(Policy = Permission.AnnualIncrementHistory.Create)]
    [HttpPost]
    public async Task<ActionResult<AnnualIncrementHistoryState>> PostAsync([FromBody] AnnualIncrementHistoryViewModel request) =>
        await ToActionResult(async () => await Mediator.Send(Mapper.Map<AddAnnualIncrementHistoryCommand>(request)));

    [Authorize(Policy = Permission.AnnualIncrementHistory.Edit)]
    [HttpPut("{id}")]
    public async Task<ActionResult<AnnualIncrementHistoryState>> PutAsync(string id, [FromBody] AnnualIncrementHistoryViewModel request)
    {
        var command = Mapper.Map<EditAnnualIncrementHistoryCommand>(request);
        return await ToActionResult(async () => await Mediator.Send(command with { Id = id }));
    }

    [Authorize(Policy = Permission.AnnualIncrementHistory.Delete)]
    [HttpDelete("{id}")]
    public async Task<ActionResult<AnnualIncrementHistoryState>> DeleteAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new DeleteAnnualIncrementHistoryCommand { Id = id }));
}

public record AnnualIncrementHistoryViewModel
{
    
	public string? UnitOfferedHistoryID { get;set; }
	public int? Year { get;set; }
	
	[DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
	public decimal? BasicFixedMonthlyRent { get;set; }
	
	[DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
	public decimal? PercentageRent { get;set; }
	
	[DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
	public decimal? MinimumMonthlyRent { get;set; }
	public int? ContractGroupingCount { get;set; }
	   
}
