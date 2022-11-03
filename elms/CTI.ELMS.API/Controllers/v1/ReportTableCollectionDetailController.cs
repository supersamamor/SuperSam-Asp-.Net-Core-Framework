using CTI.Common.Utility.Models;
using CTI.ELMS.Application.Features.ELMS.ReportTableCollectionDetail.Commands;
using CTI.ELMS.Application.Features.ELMS.ReportTableCollectionDetail.Queries;
using CTI.ELMS.Core.ELMS;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using CTI.Common.API.Controllers;

namespace CTI.ELMS.API.Controllers.v1;

[ApiVersion("1.0")]
public class ReportTableCollectionDetailController : BaseApiController<ReportTableCollectionDetailController>
{
    [Authorize(Policy = Permission.ReportTableCollectionDetail.View)]
    [HttpGet]
    public async Task<ActionResult<PagedListResponse<ReportTableCollectionDetailState>>> GetAsync([FromQuery] GetReportTableCollectionDetailQuery query) =>
        Ok(await Mediator.Send(query));

    [Authorize(Policy = Permission.ReportTableCollectionDetail.View)]
    [HttpGet("{id}")]
    public async Task<ActionResult<ReportTableCollectionDetailState>> GetAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new GetReportTableCollectionDetailByIdQuery(id)));

    [Authorize(Policy = Permission.ReportTableCollectionDetail.Create)]
    [HttpPost]
    public async Task<ActionResult<ReportTableCollectionDetailState>> PostAsync([FromBody] ReportTableCollectionDetailViewModel request) =>
        await ToActionResult(async () => await Mediator.Send(Mapper.Map<AddReportTableCollectionDetailCommand>(request)));

    [Authorize(Policy = Permission.ReportTableCollectionDetail.Edit)]
    [HttpPut("{id}")]
    public async Task<ActionResult<ReportTableCollectionDetailState>> PutAsync(string id, [FromBody] ReportTableCollectionDetailViewModel request)
    {
        var command = Mapper.Map<EditReportTableCollectionDetailCommand>(request);
        return await ToActionResult(async () => await Mediator.Send(command with { Id = id }));
    }

    [Authorize(Policy = Permission.ReportTableCollectionDetail.Delete)]
    [HttpDelete("{id}")]
    public async Task<ActionResult<ReportTableCollectionDetailState>> DeleteAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new DeleteReportTableCollectionDetailCommand { Id = id }));
}

public record ReportTableCollectionDetailViewModel
{
    
	public string? ProjectID { get;set; }
	
	public string? IFCATenantInformationID { get;set; }
	public int? Month { get;set; }
	[Required]
	
	public string Year { get;set; } = "";
	public bool IsTerminated { get;set; }
	
	[DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
	public decimal? CurrentMonth { get;set; }
	
	[DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
	public decimal? PrevMonth1 { get;set; }
	
	[DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
	public decimal? PrevMonth2 { get;set; }
	
	[DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
	public decimal? PrevMonth3 { get;set; }
	
	[DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
	public decimal? Prior { get;set; }
	
	[DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
	public decimal? TotalOverDue { get;set; }
	
	[DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
	public decimal? GrandTotal { get;set; }
	
	[DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
	public decimal? Rental { get;set; }
	
	[DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
	public decimal? CusaAC { get;set; }
	
	[DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
	public decimal? Utilities { get;set; }
	
	[DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
	public decimal? Deposits { get;set; }
	
	[DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
	public decimal? Interests { get;set; }
	
	[DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
	public decimal? Penalty { get;set; }
	
	[DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
	public decimal? Others { get;set; }
	
	[DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
	public decimal? PaidSD { get;set; }
	
	[DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
	public decimal? SDExposure { get;set; }
	
	[DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
	public decimal? PayableCurrentMonth { get;set; }
	
	[DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
	public decimal? PayablePrevMonth1 { get;set; }
	
	[DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
	public decimal? PayablePrevMonth2 { get;set; }
	
	[DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
	public decimal? PayablePrevMonth3 { get;set; }
	
	[DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
	public decimal? PayablePrior { get;set; }
	[Required]
	[StringLength(50, ErrorMessage = "{0} length can't be more than {1}.")]
	public string Column1 { get;set; } = "";
	[Required]
	[StringLength(50, ErrorMessage = "{0} length can't be more than {1}.")]
	public string Column2 { get;set; } = "";
	[Required]
	[StringLength(50, ErrorMessage = "{0} length can't be more than {1}.")]
	public string Column3 { get;set; } = "";
	[Required]
	[StringLength(50, ErrorMessage = "{0} length can't be more than {1}.")]
	public string Column4 { get;set; } = "";
	   
}
