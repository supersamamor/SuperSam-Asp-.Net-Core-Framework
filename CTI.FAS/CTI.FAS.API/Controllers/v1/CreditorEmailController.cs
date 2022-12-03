using CTI.Common.Utility.Models;
using CTI.FAS.Application.Features.FAS.CreditorEmail.Commands;
using CTI.FAS.Application.Features.FAS.CreditorEmail.Queries;
using CTI.FAS.Core.FAS;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using CTI.Common.API.Controllers;

namespace CTI.FAS.API.Controllers.v1;

[ApiVersion("1.0")]
public class CreditorEmailController : BaseApiController<CreditorEmailController>
{
    [Authorize(Policy = Permission.CreditorEmail.View)]
    [HttpGet]
    public async Task<ActionResult<PagedListResponse<CreditorEmailState>>> GetAsync([FromQuery] GetCreditorEmailQuery query) =>
        Ok(await Mediator.Send(query));

    [Authorize(Policy = Permission.CreditorEmail.View)]
    [HttpGet("{id}")]
    public async Task<ActionResult<CreditorEmailState>> GetAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new GetCreditorEmailByIdQuery(id)));

    [Authorize(Policy = Permission.CreditorEmail.Create)]
    [HttpPost]
    public async Task<ActionResult<CreditorEmailState>> PostAsync([FromBody] CreditorEmailViewModel request) =>
        await ToActionResult(async () => await Mediator.Send(Mapper.Map<AddCreditorEmailCommand>(request)));

    [Authorize(Policy = Permission.CreditorEmail.Edit)]
    [HttpPut("{id}")]
    public async Task<ActionResult<CreditorEmailState>> PutAsync(string id, [FromBody] CreditorEmailViewModel request)
    {
        var command = Mapper.Map<EditCreditorEmailCommand>(request);
        return await ToActionResult(async () => await Mediator.Send(command with { Id = id }));
    }

    [Authorize(Policy = Permission.CreditorEmail.Delete)]
    [HttpDelete("{id}")]
    public async Task<ActionResult<CreditorEmailState>> DeleteAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new DeleteCreditorEmailCommand { Id = id }));
}

public record CreditorEmailViewModel
{
    [Required]
	[StringLength(50, ErrorMessage = "{0} length can't be more than {1}.")]
	public string Email { get;set; } = "";
	[Required]
	
	public string CreditorId { get;set; } = "";
	   
}
