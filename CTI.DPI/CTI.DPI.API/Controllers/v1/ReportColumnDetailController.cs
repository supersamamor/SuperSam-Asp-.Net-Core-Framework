using CTI.Common.Utility.Models;
using CTI.DPI.Application.Features.DPI.ReportColumnDetail.Commands;
using CTI.DPI.Application.Features.DPI.ReportColumnDetail.Queries;
using CTI.DPI.Core.DPI;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using CTI.Common.API.Controllers;

namespace CTI.DPI.API.Controllers.v1;

[ApiVersion("1.0")]
public class ReportColumnDetailController : BaseApiController<ReportColumnDetailController>
{
    [Authorize(Policy = Permission.ReportColumnDetail.View)]
    [HttpGet]
    public async Task<ActionResult<PagedListResponse<ReportColumnDetailState>>> GetAsync([FromQuery] GetReportColumnDetailQuery query) =>
        Ok(await Mediator.Send(query));

    [Authorize(Policy = Permission.ReportColumnDetail.View)]
    [HttpGet("{id}")]
    public async Task<ActionResult<ReportColumnDetailState>> GetAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new GetReportColumnDetailByIdQuery(id)));

    [Authorize(Policy = Permission.ReportColumnDetail.Create)]
    [HttpPost]
    public async Task<ActionResult<ReportColumnDetailState>> PostAsync([FromBody] ReportColumnDetailViewModel request) =>
        await ToActionResult(async () => await Mediator.Send(Mapper.Map<AddReportColumnDetailCommand>(request)));

    [Authorize(Policy = Permission.ReportColumnDetail.Edit)]
    [HttpPut("{id}")]
    public async Task<ActionResult<ReportColumnDetailState>> PutAsync(string id, [FromBody] ReportColumnDetailViewModel request)
    {
        var command = Mapper.Map<EditReportColumnDetailCommand>(request);
        return await ToActionResult(async () => await Mediator.Send(command with { Id = id }));
    }

    [Authorize(Policy = Permission.ReportColumnDetail.Delete)]
    [HttpDelete("{id}")]
    public async Task<ActionResult<ReportColumnDetailState>> DeleteAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new DeleteReportColumnDetailCommand { Id = id }));
}

public record ReportColumnDetailViewModel
{
    
	public string? ReportColumnId { get;set; }
	
	public string? TableId { get;set; }
	[StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
	public string? FieldName { get;set; }
	[StringLength(50, ErrorMessage = "{0} length can't be more than {1}.")]
	public string? Function { get;set; }
	[StringLength(50, ErrorMessage = "{0} length can't be more than {1}.")]
	public string? ArithmeticOperator { get;set; }
	public int? Sequence { get;set; }
	   
}
