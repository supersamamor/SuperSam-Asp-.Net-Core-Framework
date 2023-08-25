using CTI.Common.Utility.Models;
using CTI.DSF.Application.Features.DSF.Holiday.Commands;
using CTI.DSF.Application.Features.DSF.Holiday.Queries;
using CTI.DSF.Core.DSF;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using CTI.Common.API.Controllers;

namespace CTI.DSF.API.Controllers.v1;

[ApiVersion("1.0")]
public class HolidayController : BaseApiController<HolidayController>
{
    [Authorize(Policy = Permission.Holiday.View)]
    [HttpGet]
    public async Task<ActionResult<PagedListResponse<HolidayState>>> GetAsync([FromQuery] GetHolidayQuery query) =>
        Ok(await Mediator.Send(query));

    [Authorize(Policy = Permission.Holiday.View)]
    [HttpGet("{id}")]
    public async Task<ActionResult<HolidayState>> GetAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new GetHolidayByIdQuery(id)));

    [Authorize(Policy = Permission.Holiday.Create)]
    [HttpPost]
    public async Task<ActionResult<HolidayState>> PostAsync([FromBody] HolidayViewModel request) =>
        await ToActionResult(async () => await Mediator.Send(Mapper.Map<AddHolidayCommand>(request)));

    [Authorize(Policy = Permission.Holiday.Edit)]
    [HttpPut("{id}")]
    public async Task<ActionResult<HolidayState>> PutAsync(string id, [FromBody] HolidayViewModel request)
    {
        var command = Mapper.Map<EditHolidayCommand>(request);
        return await ToActionResult(async () => await Mediator.Send(command with { Id = id }));
    }

    [Authorize(Policy = Permission.Holiday.Delete)]
    [HttpDelete("{id}")]
    public async Task<ActionResult<HolidayState>> DeleteAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new DeleteHolidayCommand { Id = id }));
}

public record HolidayViewModel
{
    public DateTime? HolidayDate { get;set; } = DateTime.Now.Date;
	[StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
	public string? HolidayName { get;set; }
	   
}
