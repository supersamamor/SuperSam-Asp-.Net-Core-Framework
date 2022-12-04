using CTI.Common.Utility.Models;
using CTI.FAS.Application.Features.FAS.EnrolledPayee.Commands;
using CTI.FAS.Application.Features.FAS.EnrolledPayee.Queries;
using CTI.FAS.Core.FAS;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using CTI.Common.API.Controllers;

namespace CTI.FAS.API.Controllers.v1;

[ApiVersion("1.0")]
public class EnrolledPayeeController : BaseApiController<EnrolledPayeeController>
{
    [Authorize(Policy = Permission.EnrolledPayee.View)]
    [HttpGet]
    public async Task<ActionResult<PagedListResponse<EnrolledPayeeState>>> GetAsync([FromQuery] GetEnrolledPayeeQuery query) =>
        Ok(await Mediator.Send(query));

    [Authorize(Policy = Permission.EnrolledPayee.View)]
    [HttpGet("{id}")]
    public async Task<ActionResult<EnrolledPayeeState>> GetAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new GetEnrolledPayeeByIdQuery(id)));

    [Authorize(Policy = Permission.EnrolledPayee.Create)]
    [HttpPost]
    public async Task<ActionResult<EnrolledPayeeState>> PostAsync([FromBody] EnrolledPayeeViewModel request) =>
        await ToActionResult(async () => await Mediator.Send(Mapper.Map<AddEnrolledPayeeCommand>(request)));

    [Authorize(Policy = Permission.EnrolledPayee.Edit)]
    [HttpPut("{id}")]
    public async Task<ActionResult<EnrolledPayeeState>> PutAsync(string id, [FromBody] EnrolledPayeeViewModel request)
    {
        var command = Mapper.Map<EditEnrolledPayeeCommand>(request);
        return await ToActionResult(async () => await Mediator.Send(command with { Id = id }));
    }

    [Authorize(Policy = Permission.EnrolledPayee.Delete)]
    [HttpDelete("{id}")]
    public async Task<ActionResult<EnrolledPayeeState>> DeleteAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new DeleteEnrolledPayeeCommand { Id = id }));
}

public record EnrolledPayeeViewModel
{
    [Required]
	
	public string CompanyId { get;set; } = "";
	[Required]
	
	public string CreditorId { get;set; } = "";
	[Required]
	[StringLength(50, ErrorMessage = "{0} length can't be more than {1}.")]
	public string PayeeAccountNumber { get;set; } = "";
	[Required]
	[StringLength(30, ErrorMessage = "{0} length can't be more than {1}.")]
	public string PayeeAccountType { get;set; } = "";
	[StringLength(30, ErrorMessage = "{0} length can't be more than {1}.")]
	public string? Status { get;set; }
	   
}
