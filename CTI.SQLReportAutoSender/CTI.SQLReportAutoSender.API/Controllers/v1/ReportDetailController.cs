using CTI.Common.Utility.Models;
using CTI.SQLReportAutoSender.Application.Features.SQLReportAutoSender.ReportDetail.Commands;
using CTI.SQLReportAutoSender.Application.Features.SQLReportAutoSender.ReportDetail.Queries;
using CTI.SQLReportAutoSender.Core.SQLReportAutoSender;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using CTI.Common.API.Controllers;

namespace CTI.SQLReportAutoSender.API.Controllers.v1;

[ApiVersion("1.0")]
public class ReportDetailController : BaseApiController<ReportDetailController>
{
    [Authorize(Policy = Permission.ReportDetail.View)]
    [HttpGet]
    public async Task<ActionResult<PagedListResponse<ReportDetailState>>> GetAsync([FromQuery] GetReportDetailQuery query) =>
        Ok(await Mediator.Send(query));

    [Authorize(Policy = Permission.ReportDetail.View)]
    [HttpGet("{id}")]
    public async Task<ActionResult<ReportDetailState>> GetAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new GetReportDetailByIdQuery(id)));

    [Authorize(Policy = Permission.ReportDetail.Create)]
    [HttpPost]
    public async Task<ActionResult<ReportDetailState>> PostAsync([FromBody] ReportDetailViewModel request) =>
        await ToActionResult(async () => await Mediator.Send(Mapper.Map<AddReportDetailCommand>(request)));

    [Authorize(Policy = Permission.ReportDetail.Edit)]
    [HttpPut("{id}")]
    public async Task<ActionResult<ReportDetailState>> PutAsync(string id, [FromBody] ReportDetailViewModel request)
    {
        var command = Mapper.Map<EditReportDetailCommand>(request);
        return await ToActionResult(async () => await Mediator.Send(command with { Id = id }));
    }

    [Authorize(Policy = Permission.ReportDetail.Delete)]
    [HttpDelete("{id}")]
    public async Task<ActionResult<ReportDetailState>> DeleteAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new DeleteReportDetailCommand { Id = id }));
}

public record ReportDetailViewModel
{
    [Required]
	
	public string ReportId { get;set; } = "";
	[Required]
	public int ReportDetailNumber { get;set; }
	[Required]
	[StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
	public string Description { get;set; } = "";
	[Required]
	[StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
	public string ConnectionString { get;set; } = "";
	[Required]
	[StringLength(8000, ErrorMessage = "{0} length can't be more than {1}.")]
	public string QueryString { get;set; } = "";
	   
}
