using CTI.Common.Utility.Models;
using CTI.DPI.Application.Features.DPI.ReportTableJoinParameter.Commands;
using CTI.DPI.Application.Features.DPI.ReportTableJoinParameter.Queries;
using CTI.DPI.Core.DPI;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using CTI.Common.API.Controllers;

namespace CTI.DPI.API.Controllers.v1;

[ApiVersion("1.0")]
public class ReportTableJoinParameterController : BaseApiController<ReportTableJoinParameterController>
{
    [Authorize(Policy = Permission.ReportTableJoinParameter.View)]
    [HttpGet]
    public async Task<ActionResult<PagedListResponse<ReportTableJoinParameterState>>> GetAsync([FromQuery] GetReportTableJoinParameterQuery query) =>
        Ok(await Mediator.Send(query));

    [Authorize(Policy = Permission.ReportTableJoinParameter.View)]
    [HttpGet("{id}")]
    public async Task<ActionResult<ReportTableJoinParameterState>> GetAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new GetReportTableJoinParameterByIdQuery(id)));

    [Authorize(Policy = Permission.ReportTableJoinParameter.Create)]
    [HttpPost]
    public async Task<ActionResult<ReportTableJoinParameterState>> PostAsync([FromBody] ReportTableJoinParameterViewModel request) =>
        await ToActionResult(async () => await Mediator.Send(Mapper.Map<AddReportTableJoinParameterCommand>(request)));

    [Authorize(Policy = Permission.ReportTableJoinParameter.Edit)]
    [HttpPut("{id}")]
    public async Task<ActionResult<ReportTableJoinParameterState>> PutAsync(string id, [FromBody] ReportTableJoinParameterViewModel request)
    {
        var command = Mapper.Map<EditReportTableJoinParameterCommand>(request);
        return await ToActionResult(async () => await Mediator.Send(command with { Id = id }));
    }

    [Authorize(Policy = Permission.ReportTableJoinParameter.Delete)]
    [HttpDelete("{id}")]
    public async Task<ActionResult<ReportTableJoinParameterState>> DeleteAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new DeleteReportTableJoinParameterCommand { Id = id }));
}

public record ReportTableJoinParameterViewModel
{
    [Required]
	[StringLength(50, ErrorMessage = "{0} length can't be more than {1}.")]
	public string ReportId { get;set; } = "";
	[StringLength(50, ErrorMessage = "{0} length can't be more than {1}.")]
	public string? LogicalOperator { get;set; }
	[Required]
	
	public string TableId { get;set; } = "";
	[Required]
	[StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
	public string FieldName { get;set; } = "";
	[Required]
	
	public string JoinFromTableId { get;set; } = "";
	[Required]
	[StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
	public string JoinFromFieldName { get;set; } = "";
	[Required]
	public int Sequence { get;set; }
	   
}
