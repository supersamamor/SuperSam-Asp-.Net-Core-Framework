using CTI.Common.Utility.Models;
using CTI.DPI.Application.Features.DPI.ReportQueryFilter.Commands;
using CTI.DPI.Application.Features.DPI.ReportQueryFilter.Queries;
using CTI.DPI.Core.DPI;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using CTI.Common.API.Controllers;

namespace CTI.DPI.API.Controllers.v1;

[ApiVersion("1.0")]
public class ReportQueryFilterController : BaseApiController<ReportQueryFilterController>
{
    [Authorize(Policy = Permission.ReportQueryFilter.View)]
    [HttpGet]
    public async Task<ActionResult<PagedListResponse<ReportQueryFilterState>>> GetAsync([FromQuery] GetReportQueryFilterQuery query) =>
        Ok(await Mediator.Send(query));

    [Authorize(Policy = Permission.ReportQueryFilter.View)]
    [HttpGet("{id}")]
    public async Task<ActionResult<ReportQueryFilterState>> GetAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new GetReportQueryFilterByIdQuery(id)));

    [Authorize(Policy = Permission.ReportQueryFilter.Create)]
    [HttpPost]
    public async Task<ActionResult<ReportQueryFilterState>> PostAsync([FromBody] ReportQueryFilterViewModel request) =>
        await ToActionResult(async () => await Mediator.Send(Mapper.Map<AddReportQueryFilterCommand>(request)));

    [Authorize(Policy = Permission.ReportQueryFilter.Edit)]
    [HttpPut("{id}")]
    public async Task<ActionResult<ReportQueryFilterState>> PutAsync(string id, [FromBody] ReportQueryFilterViewModel request)
    {
        var command = Mapper.Map<EditReportQueryFilterCommand>(request);
        return await ToActionResult(async () => await Mediator.Send(command with { Id = id }));
    }

    [Authorize(Policy = Permission.ReportQueryFilter.Delete)]
    [HttpDelete("{id}")]
    public async Task<ActionResult<ReportQueryFilterState>> DeleteAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new DeleteReportQueryFilterCommand { Id = id }));
}

public record ReportQueryFilterViewModel
{
    
	public string? ReportId { get;set; }
	[StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
	public string? FieldName { get;set; }
	[StringLength(50, ErrorMessage = "{0} length can't be more than {1}.")]
	public string? ComparisonOperator { get;set; }
	   
}
