using CTI.Common.Utility.Models;
using CTI.ELMS.Application.Features.ELMS.AnnualIncrement.Commands;
using CTI.ELMS.Application.Features.ELMS.AnnualIncrement.Queries;
using CTI.ELMS.Core.ELMS;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using CTI.Common.API.Controllers;

namespace CTI.ELMS.API.Controllers.v1;

[ApiVersion("1.0")]
public class AnnualIncrementController : BaseApiController<AnnualIncrementController>
{
    [Authorize(Policy = Permission.AnnualIncrement.View)]
    [HttpGet]
    public async Task<ActionResult<PagedListResponse<AnnualIncrementState>>> GetAsync([FromQuery] GetAnnualIncrementQuery query) =>
        Ok(await Mediator.Send(query));

    [Authorize(Policy = Permission.AnnualIncrement.View)]
    [HttpGet("{id}")]
    public async Task<ActionResult<AnnualIncrementState>> GetAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new GetAnnualIncrementByIdQuery(id)));

    [Authorize(Policy = Permission.AnnualIncrement.Create)]
    [HttpPost]
    public async Task<ActionResult<AnnualIncrementState>> PostAsync([FromBody] AnnualIncrementViewModel request) =>
        await ToActionResult(async () => await Mediator.Send(Mapper.Map<AddAnnualIncrementCommand>(request)));

    [Authorize(Policy = Permission.AnnualIncrement.Edit)]
    [HttpPut("{id}")]
    public async Task<ActionResult<AnnualIncrementState>> PutAsync(string id, [FromBody] AnnualIncrementViewModel request)
    {
        var command = Mapper.Map<EditAnnualIncrementCommand>(request);
        return await ToActionResult(async () => await Mediator.Send(command with { Id = id }));
    }

    [Authorize(Policy = Permission.AnnualIncrement.Delete)]
    [HttpDelete("{id}")]
    public async Task<ActionResult<AnnualIncrementState>> DeleteAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new DeleteAnnualIncrementCommand { Id = id }));
}

public record AnnualIncrementViewModel
{
    
	public string? UnitOfferedID { get;set; }
	public int? Year { get;set; }
	
	[DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
	public decimal? BasicFixedMonthlyRent { get;set; }
	
	[DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
	public decimal? PercentageRent { get;set; }
	
	[DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
	public decimal? MinimumMonthlyRent { get;set; }
	   
}
