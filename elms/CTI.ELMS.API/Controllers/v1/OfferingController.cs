using CTI.Common.Utility.Models;
using CTI.ELMS.Application.Features.ELMS.Offering.Commands;
using CTI.ELMS.Application.Features.ELMS.Offering.Queries;
using CTI.ELMS.Core.ELMS;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using CTI.Common.API.Controllers;

namespace CTI.ELMS.API.Controllers.v1;

[ApiVersion("1.0")]
public class OfferingController : BaseApiController<OfferingController>
{
    [Authorize(Policy = Permission.Offering.View)]
    [HttpGet]
    public async Task<ActionResult<PagedListResponse<OfferingState>>> GetAsync([FromQuery] GetOfferingQuery query) =>
        Ok(await Mediator.Send(query));

    [Authorize(Policy = Permission.Offering.View)]
    [HttpGet("{id}")]
    public async Task<ActionResult<OfferingState>> GetAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new GetOfferingByIdQuery(id)));

    [Authorize(Policy = Permission.Offering.Create)]
    [HttpPost]
    public async Task<ActionResult<OfferingState>> PostAsync([FromBody] OfferingViewModel request) =>
        await ToActionResult(async () => await Mediator.Send(Mapper.Map<AddOfferingCommand>(request)));

    [Authorize(Policy = Permission.Offering.Edit)]
    [HttpPut("{id}")]
    public async Task<ActionResult<OfferingState>> PutAsync(string id, [FromBody] OfferingViewModel request)
    {
        var command = Mapper.Map<EditOfferingCommand>(request);
        return await ToActionResult(async () => await Mediator.Send(command with { Id = id }));
    }
}

public record OfferingViewModel
{
    
	public string? ProjectID { get;set; }
	public int? OfferingHistoryID { get;set; }
	[StringLength(50, ErrorMessage = "{0} length can't be more than {1}.")]
	public string? Status { get;set; }
	public DateTime? CommencementDate { get;set; } = DateTime.Now.Date;
	public DateTime? TerminationDate { get;set; } = DateTime.Now.Date;
	public int? Year { get;set; }
	public int? Month { get;set; }
	public int? Day { get;set; }
	public int? SecMonths { get;set; }
	public int? ConstructionMonths { get;set; }
	
	[DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
	public decimal? OtherChargesAircon { get;set; }
	
	public string? Concession { get;set; }
	
	public string? OffersheetRemarks { get;set; }
	
	[DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
	public decimal? ConstructionCAMC { get;set; }
	
	[DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
	public decimal? CommencementCAMC { get;set; }
	
	[DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
	public decimal? BoardUp { get;set; }
	
	public string? UnitsInformation { get;set; }
	[StringLength(100, ErrorMessage = "{0} length can't be more than {1}.")]
	public string? ANType { get;set; }
	
	public string? LeadID { get;set; }
	[StringLength(50, ErrorMessage = "{0} length can't be more than {1}.")]
	public string? TenantContractNo { get;set; }
	
	[DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
	public decimal? DocStamp { get;set; }
	public DateTime? AwardNoticeCreatedDate { get;set; } = DateTime.Now.Date;
	[StringLength(450, ErrorMessage = "{0} length can't be more than {1}.")]
	public string? AwardNoticeCreatedBy { get;set; }
	public DateTime? SignedOfferSheetDate { get;set; } = DateTime.Now.Date;
	
	public string? TagSignedOfferSheetBy { get;set; }
	
	[DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
	public decimal? TotalUnitArea { get;set; }
	public bool IsPOS { get;set; }
	[Required]
	
	public string Location { get;set; } = "";
	
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
	public int? OfferSheetPerProjectCounter { get;set; }
	public DateTime? SignedAwardNoticeDate { get;set; } = DateTime.Now.Date;
	
	public string? TagSignedAwardNoticeBy { get;set; }
	
	[DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
	public decimal? MinimumSalesQuota { get;set; }
	[Required]
	
	public string UnitType { get;set; } = "";
	
	public string? Provision { get;set; }
	public int? FitOutPeriod { get;set; }
	public DateTime? TurnOverDate { get;set; } = DateTime.Now.Date;
	public bool AutoComputeAnnualAdvertisingFee { get;set; }
	public DateTime? BookingDate { get;set; } = DateTime.Now.Date;
	[StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
	public string? SignatoryName { get;set; }
	[StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
	public string? SignatoryPosition { get;set; }
	[StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
	public string? ANSignatoryName { get;set; }
	[StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
	public string? ANSignatoryPosition { get;set; }
	public DateTime? LeaseContractCreatedDate { get;set; } = DateTime.Now.Date;
	[StringLength(450, ErrorMessage = "{0} length can't be more than {1}.")]
	public string? LeaseContractCreatedBy { get;set; }
	
	public string? TagSignedLeaseContractBy { get;set; }
	public DateTime? SignedLeaseContractDate { get;set; } = DateTime.Now.Date;
	
	public string? TagForReviewLeaseContractBy { get;set; }
	public DateTime? ForReviewLeaseContractDate { get;set; } = DateTime.Now.Date;
	
	public string? TagForFinalPrintLeaseContractBy { get;set; }
	public DateTime? ForFinalPrintLeaseContractDate { get;set; } = DateTime.Now.Date;
	public int? LeaseContractStatus { get;set; }
	public int? ANTermTypeID { get;set; }
	public int? ContractTypeID { get;set; }
	[StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
	public string? WitnessName { get;set; }
	[StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
	public string? PermittedUse { get;set; }
	[StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
	public string? ModifiedCategory { get;set; }
	public bool IsDisabledModifiedCategory { get;set; }
	[StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
	public string? ContractNumber { get;set; }
	   
}
