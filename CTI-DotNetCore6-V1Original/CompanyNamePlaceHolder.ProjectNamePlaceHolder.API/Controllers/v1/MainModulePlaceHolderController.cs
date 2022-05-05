using CTI.Common.Utility.Models;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Features.AreaPlaceHolder.MainModulePlaceHolder.Commands;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Features.AreaPlaceHolder.MainModulePlaceHolder.Queries;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Core.AreaPlaceHolder;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.API.Controllers.v1;

[ApiVersion("1.0")]
public class MainModulePlaceHolderController : BaseApiController<MainModulePlaceHolderController>
{
    [Authorize(Policy = Permission.MainModulePlaceHolder.View)]
    [HttpGet]
    public async Task<ActionResult<PagedListResponse<ProjectState>>> GetAsync([FromQuery] GetMainModulePlaceHolderQuery query) =>
        Ok(await Mediator.Send(query));

    [Authorize(Policy = Permission.MainModulePlaceHolder.View)]
    [HttpGet("{id}")]
    public async Task<ActionResult<ProjectState>> GetAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new GetMainModulePlaceHolderByIdQuery(id)));

    [Authorize(Policy = Permission.MainModulePlaceHolder.Create)]
    [HttpPost]
    public async Task<ActionResult<ProjectState>> PostAsync([FromBody] ProjectViewModel request) =>
        await ToActionResult(async () => await Mediator.Send(Mapper.Map<AddMainModulePlaceHolderCommand>(request)));

    [Authorize(Policy = Permission.MainModulePlaceHolder.Edit)]
    [HttpPut]
    public async Task<ActionResult<ProjectState>> PutAsync([FromBody] ProjectViewModel request) =>
        await ToActionResult(async () => await Mediator.Send(Mapper.Map<EditMainModulePlaceHolderCommand>(request)));

    [Authorize(Policy = Permission.MainModulePlaceHolder.Delete)]
    [HttpDelete("{id}")]
    public async Task<ActionResult<ProjectState>> DeleteAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new DeleteMainModulePlaceHolderCommand { Id = id }));
}

public record ProjectViewModel
{
    [Required]
    public string Id { get; set; } = "";
    [Required]
    public string Code { get; set; } = "";
    [Required]
    public string Status { get; init; } = "";
    [Required]
    public string Name { get; set; } = "";
    [Required]
    public string Description { get; init; } = "";
}
