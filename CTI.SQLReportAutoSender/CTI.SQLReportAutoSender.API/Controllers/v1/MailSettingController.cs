using CTI.Common.Utility.Models;
using CTI.SQLReportAutoSender.Application.Features.SQLReportAutoSender.MailSetting.Commands;
using CTI.SQLReportAutoSender.Application.Features.SQLReportAutoSender.MailSetting.Queries;
using CTI.SQLReportAutoSender.Core.SQLReportAutoSender;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using CTI.Common.API.Controllers;

namespace CTI.SQLReportAutoSender.API.Controllers.v1;

[ApiVersion("1.0")]
public class MailSettingController : BaseApiController<MailSettingController>
{
    [Authorize(Policy = Permission.MailSetting.View)]
    [HttpGet]
    public async Task<ActionResult<PagedListResponse<MailSettingState>>> GetAsync([FromQuery] GetMailSettingQuery query) =>
        Ok(await Mediator.Send(query));

    [Authorize(Policy = Permission.MailSetting.View)]
    [HttpGet("{id}")]
    public async Task<ActionResult<MailSettingState>> GetAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new GetMailSettingByIdQuery(id)));

    [Authorize(Policy = Permission.MailSetting.Create)]
    [HttpPost]
    public async Task<ActionResult<MailSettingState>> PostAsync([FromBody] MailSettingViewModel request) =>
        await ToActionResult(async () => await Mediator.Send(Mapper.Map<AddMailSettingCommand>(request)));

    [Authorize(Policy = Permission.MailSetting.Edit)]
    [HttpPut("{id}")]
    public async Task<ActionResult<MailSettingState>> PutAsync(string id, [FromBody] MailSettingViewModel request)
    {
        var command = Mapper.Map<EditMailSettingCommand>(request);
        return await ToActionResult(async () => await Mediator.Send(command with { Id = id }));
    }

    [Authorize(Policy = Permission.MailSetting.Delete)]
    [HttpDelete("{id}")]
    public async Task<ActionResult<MailSettingState>> DeleteAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new DeleteMailSettingCommand { Id = id }));
}

public record MailSettingViewModel
{
    [Required]
	
	public string ReportId { get;set; } = "";
	[Required]
	[StringLength(50, ErrorMessage = "{0} length can't be more than {1}.")]
	public string Account { get;set; } = "";
	[Required]
	[StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
	public string Password { get;set; } = "";
	[Required]
	[StringLength(2500, ErrorMessage = "{0} length can't be more than {1}.")]
	public string Body { get;set; } = "";
	[Required]
	[StringLength(50, ErrorMessage = "{0} length can't be more than {1}.")]
	public string Subject { get;set; } = "";
	   
}
