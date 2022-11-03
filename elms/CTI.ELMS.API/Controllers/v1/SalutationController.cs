using CTI.Common.Utility.Models;
using CTI.ELMS.Application.Features.ELMS.Salutation.Commands;
using CTI.ELMS.Application.Features.ELMS.Salutation.Queries;
using CTI.ELMS.Core.ELMS;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using CTI.Common.API.Controllers;

namespace CTI.ELMS.API.Controllers.v1;

[ApiVersion("1.0")]
public class SalutationController : BaseApiController<SalutationController>
{
    [Authorize(Policy = Permission.Salutation.View)]
    [HttpGet]
    public async Task<ActionResult<PagedListResponse<SalutationState>>> GetAsync([FromQuery] GetSalutationQuery query) =>
        Ok(await Mediator.Send(query));

    [Authorize(Policy = Permission.Salutation.View)]
    [HttpGet("{id}")]
    public async Task<ActionResult<SalutationState>> GetAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new GetSalutationByIdQuery(id)));

    [Authorize(Policy = Permission.Salutation.Create)]
    [HttpPost]
    public async Task<ActionResult<SalutationState>> PostAsync([FromBody] SalutationViewModel request) =>
        await ToActionResult(async () => await Mediator.Send(Mapper.Map<AddSalutationCommand>(request)));

    [Authorize(Policy = Permission.Salutation.Edit)]
    [HttpPut("{id}")]
    public async Task<ActionResult<SalutationState>> PutAsync(string id, [FromBody] SalutationViewModel request)
    {
        var command = Mapper.Map<EditSalutationCommand>(request);
        return await ToActionResult(async () => await Mediator.Send(command with { Id = id }));
    }

    [Authorize(Policy = Permission.Salutation.Delete)]
    [HttpDelete("{id}")]
    public async Task<ActionResult<SalutationState>> DeleteAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new DeleteSalutationCommand { Id = id }));
}

public record SalutationViewModel
{
    [Required]
	[StringLength(15, ErrorMessage = "{0} length can't be more than {1}.")]
	public string SalutationDescription { get;set; } = "";
	   
}
