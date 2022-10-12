using CTI.Common.Utility.Models;
using CTI.SQLReportAutoSender.Application.Features.SQLReportAutoSender.ReportInbox.Commands;
using CTI.SQLReportAutoSender.Application.Features.SQLReportAutoSender.ReportInbox.Queries;
using CTI.SQLReportAutoSender.Core.SQLReportAutoSender;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using CTI.Common.API.Controllers;

namespace CTI.SQLReportAutoSender.API.Controllers.v1;

[ApiVersion("1.0")]
public class ReportInboxController : BaseApiController<ReportInboxController>
{
    [Authorize(Policy = Permission.ReportInbox.View)]
    [HttpGet]
    public async Task<ActionResult<PagedListResponse<ReportInboxState>>> GetAsync([FromQuery] GetReportInboxQuery query) =>
        Ok(await Mediator.Send(query));

    [Authorize(Policy = Permission.ReportInbox.View)]
    [HttpGet("{id}")]
    public async Task<ActionResult<ReportInboxState>> GetAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new GetReportInboxByIdQuery(id)));

    [Authorize(Policy = Permission.ReportInbox.Create)]
    [HttpPost]
    public async Task<ActionResult<ReportInboxState>> PostAsync([FromBody] ReportInboxViewModel request) =>
        await ToActionResult(async () => await Mediator.Send(Mapper.Map<AddReportInboxCommand>(request)));

    [Authorize(Policy = Permission.ReportInbox.Edit)]
    [HttpPut("{id}")]
    public async Task<ActionResult<ReportInboxState>> PutAsync(string id, [FromBody] ReportInboxViewModel request)
    {
        var command = Mapper.Map<EditReportInboxCommand>(request);
        return await ToActionResult(async () => await Mediator.Send(command with { Id = id }));
    }

    [Authorize(Policy = Permission.ReportInbox.Delete)]
    [HttpDelete("{id}")]
    public async Task<ActionResult<ReportInboxState>> DeleteAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new DeleteReportInboxCommand { Id = id }));
}

public record ReportInboxViewModel
{
    [Required]
	
	public string ReportId { get;set; } = "";
	[Required]
	[StringLength(450, ErrorMessage = "{0} length can't be more than {1}.")]
	public string Status { get;set; } = "";
	[Required]
	public DateTime DateTimeSent { get;set; } = DateTime.Now.Date;
	[Required]
	[StringLength(1000, ErrorMessage = "{0} length can't be more than {1}.")]
	public string Remarks { get;set; } = "";
	   
}
