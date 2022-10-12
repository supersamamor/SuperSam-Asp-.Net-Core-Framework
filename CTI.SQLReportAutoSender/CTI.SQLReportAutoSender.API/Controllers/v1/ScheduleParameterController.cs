using CTI.Common.Utility.Models;
using CTI.SQLReportAutoSender.Application.Features.SQLReportAutoSender.ScheduleParameter.Commands;
using CTI.SQLReportAutoSender.Application.Features.SQLReportAutoSender.ScheduleParameter.Queries;
using CTI.SQLReportAutoSender.Core.SQLReportAutoSender;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using CTI.Common.API.Controllers;

namespace CTI.SQLReportAutoSender.API.Controllers.v1;

[ApiVersion("1.0")]
public class ScheduleParameterController : BaseApiController<ScheduleParameterController>
{
    [Authorize(Policy = Permission.ScheduleParameter.View)]
    [HttpGet]
    public async Task<ActionResult<PagedListResponse<ScheduleParameterState>>> GetAsync([FromQuery] GetScheduleParameterQuery query) =>
        Ok(await Mediator.Send(query));

    [Authorize(Policy = Permission.ScheduleParameter.View)]
    [HttpGet("{id}")]
    public async Task<ActionResult<ScheduleParameterState>> GetAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new GetScheduleParameterByIdQuery(id)));

    [Authorize(Policy = Permission.ScheduleParameter.Create)]
    [HttpPost]
    public async Task<ActionResult<ScheduleParameterState>> PostAsync([FromBody] ScheduleParameterViewModel request) =>
        await ToActionResult(async () => await Mediator.Send(Mapper.Map<AddScheduleParameterCommand>(request)));

    [Authorize(Policy = Permission.ScheduleParameter.Edit)]
    [HttpPut("{id}")]
    public async Task<ActionResult<ScheduleParameterState>> PutAsync(string id, [FromBody] ScheduleParameterViewModel request)
    {
        var command = Mapper.Map<EditScheduleParameterCommand>(request);
        return await ToActionResult(async () => await Mediator.Send(command with { Id = id }));
    }

    [Authorize(Policy = Permission.ScheduleParameter.Delete)]
    [HttpDelete("{id}")]
    public async Task<ActionResult<ScheduleParameterState>> DeleteAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new DeleteScheduleParameterCommand { Id = id }));
}

public record ScheduleParameterViewModel
{
    [Required]
	[StringLength(50, ErrorMessage = "{0} length can't be more than {1}.")]
	public string Description { get;set; } = "";
	   
}
