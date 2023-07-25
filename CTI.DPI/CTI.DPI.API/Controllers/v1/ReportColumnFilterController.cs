using CTI.Common.Utility.Models;
using CTI.DPI.Application.Features.DPI.ReportColumnFilter.Commands;
using CTI.DPI.Application.Features.DPI.ReportColumnFilter.Queries;
using CTI.DPI.Core.DPI;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using CTI.Common.API.Controllers;

namespace CTI.DPI.API.Controllers.v1;

[ApiVersion("1.0")]
public class ReportColumnFilterController : BaseApiController<ReportColumnFilterController>
{
    [Authorize(Policy = Permission.ReportColumnFilter.View)]
    [HttpGet]
    public async Task<ActionResult<PagedListResponse<ReportColumnFilterState>>> GetAsync([FromQuery] GetReportColumnFilterQuery query) =>
        Ok(await Mediator.Send(query));

    [Authorize(Policy = Permission.ReportColumnFilter.View)]
    [HttpGet("{id}")]
    public async Task<ActionResult<ReportColumnFilterState>> GetAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new GetReportColumnFilterByIdQuery(id)));

    [Authorize(Policy = Permission.ReportColumnFilter.Create)]
    [HttpPost]
    public async Task<ActionResult<ReportColumnFilterState>> PostAsync([FromBody] ReportColumnFilterViewModel request) =>
        await ToActionResult(async () => await Mediator.Send(Mapper.Map<AddReportColumnFilterCommand>(request)));

    [Authorize(Policy = Permission.ReportColumnFilter.Edit)]
    [HttpPut("{id}")]
    public async Task<ActionResult<ReportColumnFilterState>> PutAsync(string id, [FromBody] ReportColumnFilterViewModel request)
    {
        var command = Mapper.Map<EditReportColumnFilterCommand>(request);
        return await ToActionResult(async () => await Mediator.Send(command with { Id = id }));
    }

    [Authorize(Policy = Permission.ReportColumnFilter.Delete)]
    [HttpDelete("{id}")]
    public async Task<ActionResult<ReportColumnFilterState>> DeleteAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new DeleteReportColumnFilterCommand { Id = id }));
}

public record ReportColumnFilterViewModel
{
    
	public string? ReportFilterGroupingId { get;set; }
	[StringLength(50, ErrorMessage = "{0} length can't be more than {1}.")]
	public string? LogicalOperator { get;set; }
	
	public string? TableId { get;set; }
	[StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
	public string? FieldName { get;set; }
	[StringLength(50, ErrorMessage = "{0} length can't be more than {1}.")]
	public string? ComparisonOperator { get;set; }
	public bool IsString { get;set; }
	   
}
