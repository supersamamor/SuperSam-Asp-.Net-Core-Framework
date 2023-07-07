using CNPlaceHolder.Common.Utility.Models;
using CNPlaceHolder.PNPlaceHolder.Application.Features.PNPlaceHolder.ModPlaceHolder.Commands;
using CNPlaceHolder.PNPlaceHolder.Application.Features.PNPlaceHolder.ModPlaceHolder.Queries;
using CNPlaceHolder.PNPlaceHolder.Core.PNPlaceHolder;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using CNPlaceHolder.Common.API.Controllers;

namespace CNPlaceHolder.PNPlaceHolder.API.Controllers.v1;

[ApiVersion("1.0")]
public class ModPlaceHolderController : BaseApiController<ModPlaceHolderController>
{
    [Authorize(Policy = Permission.ModPlaceHolder.View)]
    [HttpGet]
    public async Task<ActionResult<PagedListResponse<ModPlaceHolderState>>> GetAsync([FromQuery] GetModPlaceHolderQuery query) =>
        Ok(await Mediator.Send(query));

    [Authorize(Policy = Permission.ModPlaceHolder.View)]
    [HttpGet("{id}")]
    public async Task<ActionResult<ModPlaceHolderState>> GetAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new GetModPlaceHolderByIdQuery(id)));

    [Authorize(Policy = Permission.ModPlaceHolder.Create)]
    [HttpPost]
    public async Task<ActionResult<ModPlaceHolderState>> PostAsync([FromBody] ModPlaceHolderViewModel request) =>
        await ToActionResult(async () => await Mediator.Send(Mapper.Map<AddModPlaceHolderCommand>(request)));

    [Authorize(Policy = Permission.ModPlaceHolder.Edit)]
    [HttpPut("{id}")]
    public async Task<ActionResult<ModPlaceHolderState>> PutAsync(string id, [FromBody] ModPlaceHolderViewModel request)
    {
        var command = Mapper.Map<EditModPlaceHolderCommand>(request);
        return await ToActionResult(async () => await Mediator.Send(command with { Id = id }));
    }

    [Authorize(Policy = Permission.ModPlaceHolder.Delete)]
    [HttpDelete("{id}")]
    public async Task<ActionResult<ModPlaceHolderState>> DeleteAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new DeleteModPlaceHolderCommand { Id = id }));
}

public record ModPlaceHolderViewModel
{
    [Required]
	[StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
	public string ColPlaceHolder { get;set; } = "";
	   
}
