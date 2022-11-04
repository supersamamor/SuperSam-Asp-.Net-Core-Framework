using CTI.Common.Utility.Models;
using CTI.ELMS.Application.Features.ELMS.UnitBudget.Commands;
using CTI.ELMS.Application.Features.ELMS.UnitBudget.Queries;
using CTI.ELMS.Core.ELMS;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using CTI.Common.API.Controllers;

namespace CTI.ELMS.API.Controllers.v1;

[ApiVersion("1.0")]
public class UnitBudgetController : BaseApiController<UnitBudgetController>
{
    [Authorize(Policy = Permission.UnitBudget.View)]
    [HttpGet]
    public async Task<ActionResult<PagedListResponse<UnitBudgetState>>> GetAsync([FromQuery] GetUnitBudgetQuery query) =>
        Ok(await Mediator.Send(query));

    [Authorize(Policy = Permission.UnitBudget.View)]
    [HttpGet("{id}")]
    public async Task<ActionResult<UnitBudgetState>> GetAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new GetUnitBudgetByIdQuery(id)));

    [Authorize(Policy = Permission.UnitBudget.Create)]
    [HttpPost]
    public async Task<ActionResult<UnitBudgetState>> PostAsync([FromBody] UnitBudgetViewModel request) =>
        await ToActionResult(async () => await Mediator.Send(Mapper.Map<AddUnitBudgetCommand>(request)));

    [Authorize(Policy = Permission.UnitBudget.Edit)]
    [HttpPut("{id}")]
    public async Task<ActionResult<UnitBudgetState>> PutAsync(string id, [FromBody] UnitBudgetViewModel request)
    {
        var command = Mapper.Map<EditUnitBudgetCommand>(request);
        return await ToActionResult(async () => await Mediator.Send(command with { Id = id }));
    }

    [Authorize(Policy = Permission.UnitBudget.Delete)]
    [HttpDelete("{id}")]
    public async Task<ActionResult<UnitBudgetState>> DeleteAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new DeleteUnitBudgetCommand { Id = id }));
}

public record UnitBudgetViewModel
{
    public int? Year { get;set; }
	
	public string? ProjectID { get;set; }
	
	public string? UnitID { get;set; }
	
	[DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
	public decimal? January { get;set; }
	
	[DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
	public decimal? February { get;set; }
	
	[DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
	public decimal? March { get;set; }
	
	[DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
	public decimal? April { get;set; }
	
	[DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
	public decimal? May { get;set; }
	
	[DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
	public decimal? June { get;set; }
	
	[DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
	public decimal? July { get;set; }
	
	[DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
	public decimal? August { get;set; }
	
	[DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
	public decimal? September { get;set; }
	
	[DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
	public decimal? October { get;set; }
	
	[DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
	public decimal? November { get;set; }
	
	[DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
	public decimal? December { get;set; }
	
	[DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
	public decimal? LotArea { get;set; }
	public bool IsOriginalBudgeted { get;set; }
	
	public string? ParentUnitBudgetID { get;set; }
	   
}
