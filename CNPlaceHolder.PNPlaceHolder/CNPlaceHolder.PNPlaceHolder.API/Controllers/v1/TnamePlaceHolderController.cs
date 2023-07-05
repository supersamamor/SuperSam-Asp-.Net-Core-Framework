using CNPlaceHolder.Common.Utility.Models;
using CNPlaceHolder.PNPlaceHolder.Application.Features.PNPlaceHolder.TnamePlaceHolder.Commands;
using CNPlaceHolder.PNPlaceHolder.Application.Features.PNPlaceHolder.TnamePlaceHolder.Queries;
using CNPlaceHolder.PNPlaceHolder.Core.PNPlaceHolder;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using CNPlaceHolder.Common.API.Controllers;

namespace CNPlaceHolder.PNPlaceHolder.API.Controllers.v1;

[ApiVersion("1.0")]
public class TnamePlaceHolderController : BaseApiController<TnamePlaceHolderController>
{
    [Authorize(Policy = Permission.TnamePlaceHolder.View)]
    [HttpGet]
    public async Task<ActionResult<PagedListResponse<TnamePlaceHolderState>>> GetAsync([FromQuery] GetTnamePlaceHolderQuery query) =>
        Ok(await Mediator.Send(query));

    [Authorize(Policy = Permission.TnamePlaceHolder.View)]
    [HttpGet("{id}")]
    public async Task<ActionResult<TnamePlaceHolderState>> GetAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new GetTnamePlaceHolderByIdQuery(id)));

    [Authorize(Policy = Permission.TnamePlaceHolder.Create)]
    [HttpPost]
    public async Task<ActionResult<TnamePlaceHolderState>> PostAsync([FromBody] TnamePlaceHolderViewModel request) =>
        await ToActionResult(async () => await Mediator.Send(Mapper.Map<AddTnamePlaceHolderCommand>(request)));

    [Authorize(Policy = Permission.TnamePlaceHolder.Edit)]
    [HttpPut("{id}")]
    public async Task<ActionResult<TnamePlaceHolderState>> PutAsync(string id, [FromBody] TnamePlaceHolderViewModel request)
    {
        var command = Mapper.Map<EditTnamePlaceHolderCommand>(request);
        return await ToActionResult(async () => await Mediator.Send(command with { Id = id }));
    }

    [Authorize(Policy = Permission.TnamePlaceHolder.Delete)]
    [HttpDelete("{id}")]
    public async Task<ActionResult<TnamePlaceHolderState>> DeleteAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new DeleteTnamePlaceHolderCommand { Id = id }));
}

public record TnamePlaceHolderViewModel
{
    [Required]
	[StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
	public string Colname { get;set; } = "";
	   
}
