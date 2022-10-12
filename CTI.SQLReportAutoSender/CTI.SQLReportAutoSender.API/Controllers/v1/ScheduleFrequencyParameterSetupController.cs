using CTI.Common.Utility.Models;
using CTI.SQLReportAutoSender.Application.Features.SQLReportAutoSender.ScheduleFrequencyParameterSetup.Commands;
using CTI.SQLReportAutoSender.Application.Features.SQLReportAutoSender.ScheduleFrequencyParameterSetup.Queries;
using CTI.SQLReportAutoSender.Core.SQLReportAutoSender;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using CTI.Common.API.Controllers;

namespace CTI.SQLReportAutoSender.API.Controllers.v1;

[ApiVersion("1.0")]
public class ScheduleFrequencyParameterSetupController : BaseApiController<ScheduleFrequencyParameterSetupController>
{
    [Authorize(Policy = Permission.ScheduleFrequencyParameterSetup.View)]
    [HttpGet]
    public async Task<ActionResult<PagedListResponse<ScheduleFrequencyParameterSetupState>>> GetAsync([FromQuery] GetScheduleFrequencyParameterSetupQuery query) =>
        Ok(await Mediator.Send(query));

    [Authorize(Policy = Permission.ScheduleFrequencyParameterSetup.View)]
    [HttpGet("{id}")]
    public async Task<ActionResult<ScheduleFrequencyParameterSetupState>> GetAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new GetScheduleFrequencyParameterSetupByIdQuery(id)));

    [Authorize(Policy = Permission.ScheduleFrequencyParameterSetup.Create)]
    [HttpPost]
    public async Task<ActionResult<ScheduleFrequencyParameterSetupState>> PostAsync([FromBody] ScheduleFrequencyParameterSetupViewModel request) =>
        await ToActionResult(async () => await Mediator.Send(Mapper.Map<AddScheduleFrequencyParameterSetupCommand>(request)));

    [Authorize(Policy = Permission.ScheduleFrequencyParameterSetup.Edit)]
    [HttpPut("{id}")]
    public async Task<ActionResult<ScheduleFrequencyParameterSetupState>> PutAsync(string id, [FromBody] ScheduleFrequencyParameterSetupViewModel request)
    {
        var command = Mapper.Map<EditScheduleFrequencyParameterSetupCommand>(request);
        return await ToActionResult(async () => await Mediator.Send(command with { Id = id }));
    }

    [Authorize(Policy = Permission.ScheduleFrequencyParameterSetup.Delete)]
    [HttpDelete("{id}")]
    public async Task<ActionResult<ScheduleFrequencyParameterSetupState>> DeleteAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new DeleteScheduleFrequencyParameterSetupCommand { Id = id }));
}

public record ScheduleFrequencyParameterSetupViewModel
{
    [Required]
	
	public string ScheduleFrequencyId { get;set; } = "";
	[Required]
	
	public string ScheduleParameterId { get;set; } = "";
	   
}
