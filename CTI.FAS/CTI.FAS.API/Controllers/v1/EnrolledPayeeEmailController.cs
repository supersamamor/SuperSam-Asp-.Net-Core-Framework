using CTI.Common.Utility.Models;
using CTI.FAS.Application.Features.FAS.EnrolledPayeeEmail.Commands;
using CTI.FAS.Application.Features.FAS.EnrolledPayeeEmail.Queries;
using CTI.FAS.Core.FAS;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using CTI.Common.API.Controllers;

namespace CTI.FAS.API.Controllers.v1;

[ApiVersion("1.0")]
public class EnrolledPayeeEmailController : BaseApiController<EnrolledPayeeEmailController>
{
    [Authorize(Policy = Permission.EnrolledPayeeEmail.View)]
    [HttpGet]
    public async Task<ActionResult<PagedListResponse<EnrolledPayeeEmailState>>> GetAsync([FromQuery] GetEnrolledPayeeEmailQuery query) =>
        Ok(await Mediator.Send(query));

    [Authorize(Policy = Permission.EnrolledPayeeEmail.View)]
    [HttpGet("{id}")]
    public async Task<ActionResult<EnrolledPayeeEmailState>> GetAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new GetEnrolledPayeeEmailByIdQuery(id)));

    [Authorize(Policy = Permission.EnrolledPayeeEmail.Create)]
    [HttpPost]
    public async Task<ActionResult<EnrolledPayeeEmailState>> PostAsync([FromBody] EnrolledPayeeEmailViewModel request) =>
        await ToActionResult(async () => await Mediator.Send(Mapper.Map<AddEnrolledPayeeEmailCommand>(request)));

    [Authorize(Policy = Permission.EnrolledPayeeEmail.Edit)]
    [HttpPut("{id}")]
    public async Task<ActionResult<EnrolledPayeeEmailState>> PutAsync(string id, [FromBody] EnrolledPayeeEmailViewModel request)
    {
        var command = Mapper.Map<EditEnrolledPayeeEmailCommand>(request);
        return await ToActionResult(async () => await Mediator.Send(command with { Id = id }));
    }

    [Authorize(Policy = Permission.EnrolledPayeeEmail.Delete)]
    [HttpDelete("{id}")]
    public async Task<ActionResult<EnrolledPayeeEmailState>> DeleteAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new DeleteEnrolledPayeeEmailCommand { Id = id }));
}

public record EnrolledPayeeEmailViewModel
{
    [Required]
	[StringLength(70, ErrorMessage = "{0} length can't be more than {1}.")]
	public string Email { get;set; } = "";
	[Required]
	
	public string EnrolledPayeeId { get;set; } = "";
	   
}
