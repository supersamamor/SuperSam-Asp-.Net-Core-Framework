using CTI.Common.Utility.Models;
using CTI.SQLReportAutoSender.Application.Features.SQLReportAutoSender.MailRecipient.Commands;
using CTI.SQLReportAutoSender.Application.Features.SQLReportAutoSender.MailRecipient.Queries;
using CTI.SQLReportAutoSender.Core.SQLReportAutoSender;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using CTI.Common.API.Controllers;

namespace CTI.SQLReportAutoSender.API.Controllers.v1;

[ApiVersion("1.0")]
public class MailRecipientController : BaseApiController<MailRecipientController>
{
    [Authorize(Policy = Permission.MailRecipient.View)]
    [HttpGet]
    public async Task<ActionResult<PagedListResponse<MailRecipientState>>> GetAsync([FromQuery] GetMailRecipientQuery query) =>
        Ok(await Mediator.Send(query));

    [Authorize(Policy = Permission.MailRecipient.View)]
    [HttpGet("{id}")]
    public async Task<ActionResult<MailRecipientState>> GetAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new GetMailRecipientByIdQuery(id)));

    [Authorize(Policy = Permission.MailRecipient.Create)]
    [HttpPost]
    public async Task<ActionResult<MailRecipientState>> PostAsync([FromBody] MailRecipientViewModel request) =>
        await ToActionResult(async () => await Mediator.Send(Mapper.Map<AddMailRecipientCommand>(request)));

    [Authorize(Policy = Permission.MailRecipient.Edit)]
    [HttpPut("{id}")]
    public async Task<ActionResult<MailRecipientState>> PutAsync(string id, [FromBody] MailRecipientViewModel request)
    {
        var command = Mapper.Map<EditMailRecipientCommand>(request);
        return await ToActionResult(async () => await Mediator.Send(command with { Id = id }));
    }

    [Authorize(Policy = Permission.MailRecipient.Delete)]
    [HttpDelete("{id}")]
    public async Task<ActionResult<MailRecipientState>> DeleteAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new DeleteMailRecipientCommand { Id = id }));
}

public record MailRecipientViewModel
{
    [Required]
	
	public string ReportId { get;set; } = "";
	[Required]
	[StringLength(100, ErrorMessage = "{0} length can't be more than {1}.")]
	public string RecipientEmail { get;set; } = "";
	[Required]
	public int SequenceNumber { get;set; }
	   
}
