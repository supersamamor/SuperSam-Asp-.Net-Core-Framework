using CTI.Common.Utility.Models;
using CTI.SQLReportAutoSender.Application.Features.SQLReportAutoSender.ReportScheduleSetting.Commands;
using CTI.SQLReportAutoSender.Application.Features.SQLReportAutoSender.ReportScheduleSetting.Queries;
using CTI.SQLReportAutoSender.Core.SQLReportAutoSender;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using CTI.Common.API.Controllers;

namespace CTI.SQLReportAutoSender.API.Controllers.v1;

[ApiVersion("1.0")]
public class ReportScheduleSettingController : BaseApiController<ReportScheduleSettingController>
{
    [Authorize(Policy = Permission.ReportScheduleSetting.View)]
    [HttpGet]
    public async Task<ActionResult<PagedListResponse<ReportScheduleSettingState>>> GetAsync([FromQuery] GetReportScheduleSettingQuery query) =>
        Ok(await Mediator.Send(query));

    [Authorize(Policy = Permission.ReportScheduleSetting.View)]
    [HttpGet("{id}")]
    public async Task<ActionResult<ReportScheduleSettingState>> GetAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new GetReportScheduleSettingByIdQuery(id)));

    [Authorize(Policy = Permission.ReportScheduleSetting.Create)]
    [HttpPost]
    public async Task<ActionResult<ReportScheduleSettingState>> PostAsync([FromBody] ReportScheduleSettingViewModel request) =>
        await ToActionResult(async () => await Mediator.Send(Mapper.Map<AddReportScheduleSettingCommand>(request)));

    [Authorize(Policy = Permission.ReportScheduleSetting.Edit)]
    [HttpPut("{id}")]
    public async Task<ActionResult<ReportScheduleSettingState>> PutAsync(string id, [FromBody] ReportScheduleSettingViewModel request)
    {
        var command = Mapper.Map<EditReportScheduleSettingCommand>(request);
        return await ToActionResult(async () => await Mediator.Send(command with { Id = id }));
    }

    [Authorize(Policy = Permission.ReportScheduleSetting.Delete)]
    [HttpDelete("{id}")]
    public async Task<ActionResult<ReportScheduleSettingState>> DeleteAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new DeleteReportScheduleSettingCommand { Id = id }));
}

public record ReportScheduleSettingViewModel
{
    [Required]
	
	public string ReportId { get;set; } = "";
	[Required]
	
	public string ScheduleFrequencyId { get;set; } = "";
	[Required]
	
	public string ScheduleParameterId { get;set; } = "";
	[Required]
	[StringLength(50, ErrorMessage = "{0} length can't be more than {1}.")]
	public string Value { get;set; } = "";
	   
}
