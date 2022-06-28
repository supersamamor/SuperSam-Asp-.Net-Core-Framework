using CompanyNamePlaceHolder.Common.Utility.Models;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Features.ProjectNamePlaceHolder.ModuleNamePlaceHolder.Commands;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Features.ProjectNamePlaceHolder.ModuleNamePlaceHolder.Queries;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Core.ProjectNamePlaceHolder;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using CompanyNamePlaceHolder.Common.API.Controllers;

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.API.Controllers.v1;

[ApiVersion("1.0")]
public class ModuleNamePlaceHolderController : BaseApiController<ModuleNamePlaceHolderController>
{
    [Authorize(Policy = Permission.ModuleNamePlaceHolder.View)]
    [HttpGet]
    public async Task<ActionResult<PagedListResponse<ModuleNamePlaceHolderState>>> GetAsync([FromQuery] GetModuleNamePlaceHolderQuery query) =>
        Ok(await Mediator.Send(query));

    [Authorize(Policy = Permission.ModuleNamePlaceHolder.View)]
    [HttpGet("{id}")]
    public async Task<ActionResult<ModuleNamePlaceHolderState>> GetAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new GetModuleNamePlaceHolderByIdQuery(id)));

    [Authorize(Policy = Permission.ModuleNamePlaceHolder.Create)]
    [HttpPost]
    public async Task<ActionResult<ModuleNamePlaceHolderState>> PostAsync([FromBody] ModuleNamePlaceHolderViewModel request) =>
        await ToActionResult(async () => await Mediator.Send(Mapper.Map<AddModuleNamePlaceHolderCommand>(request)));

    [Authorize(Policy = Permission.ModuleNamePlaceHolder.Edit)]
    [HttpPut("{id}")]
    public async Task<ActionResult<ModuleNamePlaceHolderState>> PutAsync(string id, [FromBody] ModuleNamePlaceHolderViewModel request)
    {
        var command = Mapper.Map<EditModuleNamePlaceHolderCommand>(request);
        return await ToActionResult(async () => await Mediator.Send(command with { Id = id }));
    }

    [Authorize(Policy = Permission.ModuleNamePlaceHolder.Delete)]
    [HttpDelete("{id}")]
    public async Task<ActionResult<ModuleNamePlaceHolderState>> DeleteAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new DeleteModuleNamePlaceHolderCommand { Id = id }));
}

public record ModuleNamePlaceHolderViewModel
{
    [Required]
	[StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
	public string Code { get;set; } = "";
	   
}
