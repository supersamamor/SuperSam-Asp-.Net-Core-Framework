using CTI.Common.Utility.Models;
using CTI.ELMS.Application.Features.ELMS.IFCAUnitInformation.Commands;
using CTI.ELMS.Application.Features.ELMS.IFCAUnitInformation.Queries;
using CTI.ELMS.Core.ELMS;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using CTI.Common.API.Controllers;

namespace CTI.ELMS.API.Controllers.v1;

[ApiVersion("1.0")]
public class IFCAUnitInformationController : BaseApiController<IFCAUnitInformationController>
{
    [Authorize(Policy = Permission.IFCAUnitInformation.View)]
    [HttpGet]
    public async Task<ActionResult<PagedListResponse<IFCAUnitInformationState>>> GetAsync([FromQuery] GetIFCAUnitInformationQuery query) =>
        Ok(await Mediator.Send(query));

    [Authorize(Policy = Permission.IFCAUnitInformation.View)]
    [HttpGet("{id}")]
    public async Task<ActionResult<IFCAUnitInformationState>> GetAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new GetIFCAUnitInformationByIdQuery(id)));

    [Authorize(Policy = Permission.IFCAUnitInformation.Create)]
    [HttpPost]
    public async Task<ActionResult<IFCAUnitInformationState>> PostAsync([FromBody] IFCAUnitInformationViewModel request) =>
        await ToActionResult(async () => await Mediator.Send(Mapper.Map<AddIFCAUnitInformationCommand>(request)));

    [Authorize(Policy = Permission.IFCAUnitInformation.Edit)]
    [HttpPut("{id}")]
    public async Task<ActionResult<IFCAUnitInformationState>> PutAsync(string id, [FromBody] IFCAUnitInformationViewModel request)
    {
        var command = Mapper.Map<EditIFCAUnitInformationCommand>(request);
        return await ToActionResult(async () => await Mediator.Send(command with { Id = id }));
    }

    [Authorize(Policy = Permission.IFCAUnitInformation.Delete)]
    [HttpDelete("{id}")]
    public async Task<ActionResult<IFCAUnitInformationState>> DeleteAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new DeleteIFCAUnitInformationCommand { Id = id }));
}

public record IFCAUnitInformationViewModel
{
    
	public string? UnitID { get;set; }
	
	public string? IFCATenantInformationID { get;set; }
	
	[DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
	public decimal? RentalRate { get;set; }
	
	[DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
	public decimal? BudgetedAmount { get;set; }
	public DateTime? StartDate { get;set; } = DateTime.Now.Date;
	public DateTime? EndDate { get;set; } = DateTime.Now.Date;
	
	[DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
	public decimal? BasicFixedMonthlyRent { get;set; }
	   
}
