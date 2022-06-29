using CompanyNamePlaceHolder.Common.Utility.Models;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Features.ProjectNamePlaceHolder.SubDetailItemPlaceHolder.Commands;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Features.ProjectNamePlaceHolder.SubDetailItemPlaceHolder.Queries;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Core.ProjectNamePlaceHolder;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using CompanyNamePlaceHolder.Common.API.Controllers;

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.API.Controllers.v1;

[ApiVersion("1.0")]
public class SubDetailItemPlaceHolderController : BaseApiController<SubDetailItemPlaceHolderController>
{
    [Authorize(Policy = Permission.SubDetailItemPlaceHolder.View)]
    [HttpGet]
    public async Task<ActionResult<PagedListResponse<SubDetailItemPlaceHolderState>>> GetAsync([FromQuery] GetSubDetailItemPlaceHolderQuery query) =>
        Ok(await Mediator.Send(query));

    [Authorize(Policy = Permission.SubDetailItemPlaceHolder.View)]
    [HttpGet("{id}")]
    public async Task<ActionResult<SubDetailItemPlaceHolderState>> GetAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new GetSubDetailItemPlaceHolderByIdQuery(id)));

    [Authorize(Policy = Permission.SubDetailItemPlaceHolder.Create)]
    [HttpPost]
    public async Task<ActionResult<SubDetailItemPlaceHolderState>> PostAsync([FromBody] SubDetailItemPlaceHolderViewModel request) =>
        await ToActionResult(async () => await Mediator.Send(Mapper.Map<AddSubDetailItemPlaceHolderCommand>(request)));

    [Authorize(Policy = Permission.SubDetailItemPlaceHolder.Edit)]
    [HttpPut("{id}")]
    public async Task<ActionResult<SubDetailItemPlaceHolderState>> PutAsync(string id, [FromBody] SubDetailItemPlaceHolderViewModel request)
    {
        var command = Mapper.Map<EditSubDetailItemPlaceHolderCommand>(request);
        return await ToActionResult(async () => await Mediator.Send(command with { Id = id }));
    }

    [Authorize(Policy = Permission.SubDetailItemPlaceHolder.Delete)]
    [HttpDelete("{id}")]
    public async Task<ActionResult<SubDetailItemPlaceHolderState>> DeleteAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new DeleteSubDetailItemPlaceHolderCommand { Id = id }));
}

public record SubDetailItemPlaceHolderViewModel
{
    [Required]
	public string MainModulePlaceHolderId { get;set; } = "";
	[Required]
	[StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
	public string Code { get;set; } = "";
	   
}
