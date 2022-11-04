using CTI.Common.Utility.Models;
using CTI.ELMS.Application.Features.ELMS.OfferingHistory.Commands;
using CTI.ELMS.Application.Features.ELMS.OfferingHistory.Queries;
using CTI.ELMS.Core.ELMS;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using CTI.Common.API.Controllers;

namespace CTI.ELMS.API.Controllers.v1;

[ApiVersion("1.0")]
public class OfferingHistoryController : BaseApiController<OfferingHistoryController>
{
    [Authorize(Policy = Permission.OfferingHistory.View)]
    [HttpGet]
    public async Task<ActionResult<PagedListResponse<OfferingHistoryState>>> GetAsync([FromQuery] GetOfferingHistoryQuery query) =>
        Ok(await Mediator.Send(query));

    [Authorize(Policy = Permission.OfferingHistory.View)]
    [HttpGet("{id}")]
    public async Task<ActionResult<OfferingHistoryState>> GetAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new GetOfferingHistoryByIdQuery(id)));

    [Authorize(Policy = Permission.OfferingHistory.Create)]
    [HttpPost]
    public async Task<ActionResult<OfferingHistoryState>> PostAsync([FromBody] OfferingHistoryViewModel request) =>
        await ToActionResult(async () => await Mediator.Send(Mapper.Map<AddOfferingHistoryCommand>(request)));

    [Authorize(Policy = Permission.OfferingHistory.Edit)]
    [HttpPut("{id}")]
    public async Task<ActionResult<OfferingHistoryState>> PutAsync(string id, [FromBody] OfferingHistoryViewModel request)
    {
        var command = Mapper.Map<EditOfferingHistoryCommand>(request);
        return await ToActionResult(async () => await Mediator.Send(command with { Id = id }));
    }

    [Authorize(Policy = Permission.OfferingHistory.Delete)]
    [HttpDelete("{id}")]
    public async Task<ActionResult<OfferingHistoryState>> DeleteAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new DeleteOfferingHistoryCommand { Id = id }));
}

public record OfferingHistoryViewModel
{
    
	public string? OfferingID { get;set; }
	[StringLength(35, ErrorMessage = "{0} length can't be more than {1}.")]
	public string? Status { get;set; }
	public DateTime? CommencementDate { get;set; } = DateTime.Now.Date;
	public DateTime? TerminationDate { get;set; } = DateTime.Now.Date;
	public int? Year { get;set; }
	public int? Month { get;set; }
	public int? Day { get;set; }
	public int? SecMonths { get;set; }
	public int? ConstructionMonths { get;set; }
	
	[DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
	public decimal? BoardUp { get;set; }
	
	[DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
	public decimal? OtherChargesAircon { get;set; }
	
	[DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
	public decimal? ConstructionCAMC { get;set; }
	
	[DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
	public decimal? CommencementCAMC { get;set; }
	
	public string? Concession { get;set; }
	
	public string? OffersheetRemarks { get;set; }
	[Required]
	
	public string UnitsInformation { get;set; } = "";
	[StringLength(100, ErrorMessage = "{0} length can't be more than {1}.")]
	public string? ANType { get;set; }
	
	public string? LeadID { get;set; }
	
	public string? ProjectID { get;set; }
	
	[DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
	public decimal? TotalUnitArea { get;set; }
	
	[DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
	public decimal? TotalSecurityDeposit { get;set; }
	
	[DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
	public decimal? AnnualAdvertisingFee { get;set; }
	
	[DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
	public decimal? CAMCConstructionTotalUnitArea { get;set; }
	public int? ConstructionCAMCMonths { get;set; }
	
	[DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
	public decimal? ConstructionCAMCRate { get;set; }
	public bool HasBoardUpFee { get;set; }
	public int? SecurityDepositPayableWithinMonths { get;set; }
	
	[DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
	public decimal? TotalConstructionBond { get;set; }
	public int? ConstructionPayableWithinMonths { get;set; }
	public bool IsPOS { get;set; }
	[Required]
	
	public string Location { get;set; } = "";
	public int? OfferingVersion { get;set; }
	
	[DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
	public decimal? TotalBasicFixedMonthlyRent { get;set; }
	
	[DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
	public decimal? TotalMinimumMonthlyRent { get;set; }
	
	[DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
	public decimal? TotalLotBudget { get;set; }
	
	[DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
	public decimal? TotalPercentageRent { get;set; }
	public bool AutoComputeTotalConstructionBond { get;set; }
	public bool AutoComputeTotalSecurityDeposit { get;set; }
	
	[DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
	public decimal? MinimumSalesQuota { get;set; }
	[Required]
	
	public string UnitType { get;set; } = "";
	
	public string? Provision { get;set; }
	public int? FitOutPeriod { get;set; }
	[Required]
	public DateTime TurnOverDate { get;set; } = DateTime.Now.Date;
	public bool AutoComputeAnnualAdvertisingFee { get;set; }
	[Required]
	public DateTime BookingDate { get;set; } = DateTime.Now.Date;
	   
}
