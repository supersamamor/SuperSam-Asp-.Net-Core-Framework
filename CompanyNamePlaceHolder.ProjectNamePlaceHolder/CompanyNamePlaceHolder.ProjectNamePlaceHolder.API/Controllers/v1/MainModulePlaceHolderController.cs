using CompanyNamePlaceHolder.Common.Utility.Models;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Features.AreaPlaceHolder.MainModulePlaceHolder.Commands;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Features.AreaPlaceHolder.MainModulePlaceHolder.Queries;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Core.AreaPlaceHolder;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using CompanyNamePlaceHolder.Common.API.Controllers;

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.API.Controllers.v1;

[ApiVersion("1.0")]
public class MainModulePlaceHolderController : BaseApiController<MainModulePlaceHolderController>
{
    [Authorize(Policy = Permission.MainModulePlaceHolder.View)]
    [HttpGet]
    public async Task<ActionResult<PagedListResponse<MainModulePlaceHolderState>>> GetAsync([FromQuery] GetMainModulePlaceHolderQuery query) =>
        Ok(await Mediator.Send(query));

    [Authorize(Policy = Permission.MainModulePlaceHolder.View)]
    [HttpGet("{id}")]
    public async Task<ActionResult<MainModulePlaceHolderState>> GetAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new GetMainModulePlaceHolderByIdQuery(id)));

    [Authorize(Policy = Permission.MainModulePlaceHolder.Create)]
    [HttpPost]
    public async Task<ActionResult<MainModulePlaceHolderState>> PostAsync([FromBody] MainModulePlaceHolderViewModel request) =>
        await ToActionResult(async () => await Mediator.Send(Mapper.Map<AddMainModulePlaceHolderCommand>(request)));

    [Authorize(Policy = Permission.MainModulePlaceHolder.Edit)]
    [HttpPut("{id}")]
    public async Task<ActionResult<MainModulePlaceHolderState>> PutAsync(string id, [FromBody] MainModulePlaceHolderViewModel request)
    {
        var command = Mapper.Map<EditMainModulePlaceHolderCommand>(request);
        return await ToActionResult(async () => await Mediator.Send(command with { Id = id }));
    }

    [Authorize(Policy = Permission.MainModulePlaceHolder.Delete)]
    [HttpDelete("{id}")]
    public async Task<ActionResult<MainModulePlaceHolderState>> DeleteAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new DeleteMainModulePlaceHolderCommand { Id = id }));
}

public record MainModulePlaceHolderViewModel
{
    [Required]
	[StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
	public string Code { get;set; } = "";
	   
}
