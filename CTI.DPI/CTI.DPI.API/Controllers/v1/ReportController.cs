using CTI.Common.Utility.Models;
using CTI.DPI.Application.Features.DPI.Report.Commands;
using CTI.DPI.Application.Features.DPI.Report.Queries;
using CTI.DPI.Core.DPI;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using CTI.Common.API.Controllers;

namespace CTI.DPI.API.Controllers.v1;

[ApiVersion("1.0")]
public class ReportController : BaseApiController<ReportController>
{
    [Authorize(Policy = Permission.Report.View)]
    [HttpGet]
    public async Task<ActionResult<PagedListResponse<ReportState>>> GetAsync([FromQuery] GetReportQuery query) =>
        Ok(await Mediator.Send(query));

    [Authorize(Policy = Permission.Report.View)]
    [HttpGet("{id}")]
    public async Task<ActionResult<ReportState>> GetAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new GetReportByIdQuery(id)));

    [Authorize(Policy = Permission.Report.Create)]
    [HttpPost]
    public async Task<ActionResult<ReportState>> PostAsync([FromBody] ReportViewModel request) =>
        await ToActionResult(async () => await Mediator.Send(Mapper.Map<AddReportCommand>(request)));

    [Authorize(Policy = Permission.Report.Edit)]
    [HttpPut("{id}")]
    public async Task<ActionResult<ReportState>> PutAsync(string id, [FromBody] ReportViewModel request)
    {
        var command = Mapper.Map<EditReportCommand>(request);
        return await ToActionResult(async () => await Mediator.Send(command with { Id = id }));
    }

    [Authorize(Policy = Permission.Report.Delete)]
    [HttpDelete("{id}")]
    public async Task<ActionResult<ReportState>> DeleteAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new DeleteReportCommand { Id = id }));
}

public record ReportViewModel
{
    [Required]
	[StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
	public string ReportName { get;set; } = "";
	[Required]
	[StringLength(50, ErrorMessage = "{0} length can't be more than {1}.")]
	public string QueryType { get;set; } = "";
	[Required]
	[StringLength(50, ErrorMessage = "{0} length can't be more than {1}.")]
	public string ReportOrChartType { get;set; } = "";
	[Required]
	public bool IsDistinct { get;set; }
	[StringLength(8000, ErrorMessage = "{0} length can't be more than {1}.")]
	public string? QueryString { get;set; }
	   
}
