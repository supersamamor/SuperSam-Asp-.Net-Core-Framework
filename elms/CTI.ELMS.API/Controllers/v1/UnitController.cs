using CTI.Common.Utility.Models;
using CTI.ELMS.Application.Features.ELMS.Unit.Commands;
using CTI.ELMS.Application.Features.ELMS.Unit.Queries;
using CTI.ELMS.Core.ELMS;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using CTI.Common.API.Controllers;

namespace CTI.ELMS.API.Controllers.v1;

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
	[StringLength(20, ErrorMessage = "{0} length can't be more than {1}.")]
	public string UnitNo { get;set; } = "";
	[Required]
	
	public string ProjectID { get;set; } = "";
	[Required]
	
	[DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
	public decimal LotBudget { get;set; }
	[Required]
	
	[DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
	public decimal LotArea { get;set; }
	public DateTime? AvailabilityDate { get;set; } = DateTime.Now.Date;
	public DateTime? CommencementDate { get;set; } = DateTime.Now.Date;
	[StringLength(50, ErrorMessage = "{0} length can't be more than {1}.")]
	public string? CurrentTenantContractNo { get;set; }
	   
}
