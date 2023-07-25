using CTI.Common.Utility.Models;
using CTI.DPI.Application.Features.DPI.ReportColumnHeader.Commands;
using CTI.DPI.Application.Features.DPI.ReportColumnHeader.Queries;
using CTI.DPI.Core.DPI;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using CTI.Common.API.Controllers;

namespace CTI.DPI.API.Controllers.v1;

[ApiVersion("1.0")]
public class ReportColumnHeaderController : BaseApiController<ReportColumnHeaderController>
{
    [Authorize(Policy = Permission.ReportColumnHeader.View)]
    [HttpGet]
    public async Task<ActionResult<PagedListResponse<ReportColumnHeaderState>>> GetAsync([FromQuery] GetReportColumnHeaderQuery query) =>
        Ok(await Mediator.Send(query));

    [Authorize(Policy = Permission.ReportColumnHeader.View)]
    [HttpGet("{id}")]
    public async Task<ActionResult<ReportColumnHeaderState>> GetAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new GetReportColumnHeaderByIdQuery(id)));

    [Authorize(Policy = Permission.ReportColumnHeader.Create)]
    [HttpPost]
    public async Task<ActionResult<ReportColumnHeaderState>> PostAsync([FromBody] ReportColumnHeaderViewModel request) =>
        await ToActionResult(async () => await Mediator.Send(Mapper.Map<AddReportColumnHeaderCommand>(request)));

    [Authorize(Policy = Permission.ReportColumnHeader.Edit)]
    [HttpPut("{id}")]
    public async Task<ActionResult<ReportColumnHeaderState>> PutAsync(string id, [FromBody] ReportColumnHeaderViewModel request)
    {
        var command = Mapper.Map<EditReportColumnHeaderCommand>(request);
        return await ToActionResult(async () => await Mediator.Send(command with { Id = id }));
    }

    [Authorize(Policy = Permission.ReportColumnHeader.Delete)]
    [HttpDelete("{id}")]
    public async Task<ActionResult<ReportColumnHeaderState>> DeleteAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new DeleteReportColumnHeaderCommand { Id = id }));
}

public record ReportColumnHeaderViewModel
{
    
	public string? ReportId { get;set; }
	[StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
	public string? Alias { get;set; }
	[StringLength(50, ErrorMessage = "{0} length can't be more than {1}.")]
	public string? AggregationOperator { get;set; }
	   
}
