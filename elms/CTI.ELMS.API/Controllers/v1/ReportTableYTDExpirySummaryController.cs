using CTI.Common.Utility.Models;
using CTI.ELMS.Application.Features.ELMS.ReportTableYTDExpirySummary.Commands;
using CTI.ELMS.Application.Features.ELMS.ReportTableYTDExpirySummary.Queries;
using CTI.ELMS.Core.ELMS;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using CTI.Common.API.Controllers;

namespace CTI.ELMS.API.Controllers.v1;

[ApiVersion("1.0")]
public class ReportTableYTDExpirySummaryController : BaseApiController<ReportTableYTDExpirySummaryController>
{
    [Authorize(Policy = Permission.ReportTableYTDExpirySummary.View)]
    [HttpGet]
    public async Task<ActionResult<PagedListResponse<ReportTableYTDExpirySummaryState>>> GetAsync([FromQuery] GetReportTableYTDExpirySummaryQuery query) =>
        Ok(await Mediator.Send(query));

    [Authorize(Policy = Permission.ReportTableYTDExpirySummary.View)]
    [HttpGet("{id}")]
    public async Task<ActionResult<ReportTableYTDExpirySummaryState>> GetAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new GetReportTableYTDExpirySummaryByIdQuery(id)));

    [Authorize(Policy = Permission.ReportTableYTDExpirySummary.Create)]
    [HttpPost]
    public async Task<ActionResult<ReportTableYTDExpirySummaryState>> PostAsync([FromBody] ReportTableYTDExpirySummaryViewModel request) =>
        await ToActionResult(async () => await Mediator.Send(Mapper.Map<AddReportTableYTDExpirySummaryCommand>(request)));

    [Authorize(Policy = Permission.ReportTableYTDExpirySummary.Edit)]
    [HttpPut("{id}")]
    public async Task<ActionResult<ReportTableYTDExpirySummaryState>> PutAsync(string id, [FromBody] ReportTableYTDExpirySummaryViewModel request)
    {
        var command = Mapper.Map<EditReportTableYTDExpirySummaryCommand>(request);
        return await ToActionResult(async () => await Mediator.Send(command with { Id = id }));
    }

    [Authorize(Policy = Permission.ReportTableYTDExpirySummary.Delete)]
    [HttpDelete("{id}")]
    public async Task<ActionResult<ReportTableYTDExpirySummaryState>> DeleteAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new DeleteReportTableYTDExpirySummaryCommand { Id = id }));
}

public record ReportTableYTDExpirySummaryViewModel
{
    public int? EntityID { get;set; }
	[Required]
	[StringLength(20, ErrorMessage = "{0} length can't be more than {1}.")]
	public string EntityShortName { get;set; } = "";
	[Required]
	[StringLength(100, ErrorMessage = "{0} length can't be more than {1}.")]
	public string EntityName { get;set; } = "";
	public int? ProjectID { get;set; }
	[Required]
	[StringLength(100, ErrorMessage = "{0} length can't be more than {1}.")]
	public string ProjectName { get;set; } = "";
	[Required]
	[StringLength(100, ErrorMessage = "{0} length can't be more than {1}.")]
	public string Location { get;set; } = "";
	
	[DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
	public decimal? LandArea { get;set; }
	
	[DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
	public decimal? TotalGLA { get;set; }
	[Required]
	[StringLength(20, ErrorMessage = "{0} length can't be more than {1}.")]
	public string ColumnName { get;set; } = "";
	
	[DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
	public decimal? ExpiryLotArea { get;set; }
	
	[DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
	public decimal? Renewed { get;set; }
	
	[DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
	public decimal? NewLeases { get;set; }
	
	[DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
	public decimal? WithProspectNego { get;set; }
	public int? OrderBy { get;set; }
	public int? VerticalOrderBy { get;set; }
	public int? ReportYear { get;set; }
	public DateTime? ProcessedDate { get;set; } = DateTime.Now.Date;
	   
}
