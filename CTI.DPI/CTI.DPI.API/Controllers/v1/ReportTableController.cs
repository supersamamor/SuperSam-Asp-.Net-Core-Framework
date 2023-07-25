using CTI.Common.Utility.Models;
using CTI.DPI.Application.Features.DPI.ReportTable.Commands;
using CTI.DPI.Application.Features.DPI.ReportTable.Queries;
using CTI.DPI.Core.DPI;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using CTI.Common.API.Controllers;

namespace CTI.DPI.API.Controllers.v1;

[ApiVersion("1.0")]
public class ReportTableController : BaseApiController<ReportTableController>
{
    [Authorize(Policy = Permission.ReportTable.View)]
    [HttpGet]
    public async Task<ActionResult<PagedListResponse<ReportTableState>>> GetAsync([FromQuery] GetReportTableQuery query) =>
        Ok(await Mediator.Send(query));

    [Authorize(Policy = Permission.ReportTable.View)]
    [HttpGet("{id}")]
    public async Task<ActionResult<ReportTableState>> GetAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new GetReportTableByIdQuery(id)));

    [Authorize(Policy = Permission.ReportTable.Create)]
    [HttpPost]
    public async Task<ActionResult<ReportTableState>> PostAsync([FromBody] ReportTableViewModel request) =>
        await ToActionResult(async () => await Mediator.Send(Mapper.Map<AddReportTableCommand>(request)));

    [Authorize(Policy = Permission.ReportTable.Edit)]
    [HttpPut("{id}")]
    public async Task<ActionResult<ReportTableState>> PutAsync(string id, [FromBody] ReportTableViewModel request)
    {
        var command = Mapper.Map<EditReportTableCommand>(request);
        return await ToActionResult(async () => await Mediator.Send(command with { Id = id }));
    }

    [Authorize(Policy = Permission.ReportTable.Delete)]
    [HttpDelete("{id}")]
    public async Task<ActionResult<ReportTableState>> DeleteAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new DeleteReportTableCommand { Id = id }));
}

public record ReportTableViewModel
{
    [Required]
	
	public string ReportId { get;set; } = "";
	[Required]
	[StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
	public string TableName { get;set; } = "";
	[StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
	public string? Alias { get;set; }
	[StringLength(50, ErrorMessage = "{0} length can't be more than {1}.")]
	public string? JoinType { get;set; }
	[Required]
	public int Sequence { get;set; }
	   
}
