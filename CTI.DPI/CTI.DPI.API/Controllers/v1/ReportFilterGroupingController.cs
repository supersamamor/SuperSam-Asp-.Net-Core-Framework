using CTI.Common.Utility.Models;
using CTI.DPI.Application.Features.DPI.ReportFilterGrouping.Commands;
using CTI.DPI.Application.Features.DPI.ReportFilterGrouping.Queries;
using CTI.DPI.Core.DPI;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using CTI.Common.API.Controllers;

namespace CTI.DPI.API.Controllers.v1;

[ApiVersion("1.0")]
public class ReportFilterGroupingController : BaseApiController<ReportFilterGroupingController>
{
    [Authorize(Policy = Permission.ReportFilterGrouping.View)]
    [HttpGet]
    public async Task<ActionResult<PagedListResponse<ReportFilterGroupingState>>> GetAsync([FromQuery] GetReportFilterGroupingQuery query) =>
        Ok(await Mediator.Send(query));

    [Authorize(Policy = Permission.ReportFilterGrouping.View)]
    [HttpGet("{id}")]
    public async Task<ActionResult<ReportFilterGroupingState>> GetAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new GetReportFilterGroupingByIdQuery(id)));

    [Authorize(Policy = Permission.ReportFilterGrouping.Create)]
    [HttpPost]
    public async Task<ActionResult<ReportFilterGroupingState>> PostAsync([FromBody] ReportFilterGroupingViewModel request) =>
        await ToActionResult(async () => await Mediator.Send(Mapper.Map<AddReportFilterGroupingCommand>(request)));

    [Authorize(Policy = Permission.ReportFilterGrouping.Edit)]
    [HttpPut("{id}")]
    public async Task<ActionResult<ReportFilterGroupingState>> PutAsync(string id, [FromBody] ReportFilterGroupingViewModel request)
    {
        var command = Mapper.Map<EditReportFilterGroupingCommand>(request);
        return await ToActionResult(async () => await Mediator.Send(command with { Id = id }));
    }

    [Authorize(Policy = Permission.ReportFilterGrouping.Delete)]
    [HttpDelete("{id}")]
    public async Task<ActionResult<ReportFilterGroupingState>> DeleteAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new DeleteReportFilterGroupingCommand { Id = id }));
}

public record ReportFilterGroupingViewModel
{
    
	public string? ReportId { get;set; }
	[StringLength(50, ErrorMessage = "{0} length can't be more than {1}.")]
	public string? LogicalOperator { get;set; }
	public int? GroupLevel { get;set; }
	public int? Sequence { get;set; }
	   
}
