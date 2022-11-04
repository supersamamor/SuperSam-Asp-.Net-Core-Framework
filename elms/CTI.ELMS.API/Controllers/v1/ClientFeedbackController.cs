using CTI.Common.Utility.Models;
using CTI.ELMS.Application.Features.ELMS.ClientFeedback.Commands;
using CTI.ELMS.Application.Features.ELMS.ClientFeedback.Queries;
using CTI.ELMS.Core.ELMS;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using CTI.Common.API.Controllers;

namespace CTI.ELMS.API.Controllers.v1;

[ApiVersion("1.0")]
public class ClientFeedbackController : BaseApiController<ClientFeedbackController>
{
    [Authorize(Policy = Permission.ClientFeedback.View)]
    [HttpGet]
    public async Task<ActionResult<PagedListResponse<ClientFeedbackState>>> GetAsync([FromQuery] GetClientFeedbackQuery query) =>
        Ok(await Mediator.Send(query));

    [Authorize(Policy = Permission.ClientFeedback.View)]
    [HttpGet("{id}")]
    public async Task<ActionResult<ClientFeedbackState>> GetAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new GetClientFeedbackByIdQuery(id)));

    [Authorize(Policy = Permission.ClientFeedback.Create)]
    [HttpPost]
    public async Task<ActionResult<ClientFeedbackState>> PostAsync([FromBody] ClientFeedbackViewModel request) =>
        await ToActionResult(async () => await Mediator.Send(Mapper.Map<AddClientFeedbackCommand>(request)));

    [Authorize(Policy = Permission.ClientFeedback.Edit)]
    [HttpPut("{id}")]
    public async Task<ActionResult<ClientFeedbackState>> PutAsync(string id, [FromBody] ClientFeedbackViewModel request)
    {
        var command = Mapper.Map<EditClientFeedbackCommand>(request);
        return await ToActionResult(async () => await Mediator.Send(command with { Id = id }));
    }

    [Authorize(Policy = Permission.ClientFeedback.Delete)]
    [HttpDelete("{id}")]
    public async Task<ActionResult<ClientFeedbackState>> DeleteAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new DeleteClientFeedbackCommand { Id = id }));
}

public record ClientFeedbackViewModel
{
    [Required]
	[StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
	public string ClientFeedbackName { get;set; } = "";
	   
}
