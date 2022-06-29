using CTI.Common.Utility.Models;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Features.ProjectNamePlaceHolder.SubDetailListPlaceHolder.Commands;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Features.ProjectNamePlaceHolder.SubDetailListPlaceHolder.Queries;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Core.ProjectNamePlaceHolder;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using CTI.Common.API.Controllers;

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.API.Controllers.v1;

[ApiVersion("1.0")]
public class SubDetailListPlaceHolderController : BaseApiController<SubDetailListPlaceHolderController>
{
    [Authorize(Policy = Permission.SubDetailListPlaceHolder.View)]
    [HttpGet]
    public async Task<ActionResult<PagedListResponse<SubDetailListPlaceHolderState>>> GetAsync([FromQuery] GetSubDetailListPlaceHolderQuery query) =>
        Ok(await Mediator.Send(query));

    [Authorize(Policy = Permission.SubDetailListPlaceHolder.View)]
    [HttpGet("{id}")]
    public async Task<ActionResult<SubDetailListPlaceHolderState>> GetAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new GetSubDetailListPlaceHolderByIdQuery(id)));

    [Authorize(Policy = Permission.SubDetailListPlaceHolder.Create)]
    [HttpPost]
    public async Task<ActionResult<SubDetailListPlaceHolderState>> PostAsync([FromBody] SubDetailListPlaceHolderViewModel request) =>
        await ToActionResult(async () => await Mediator.Send(Mapper.Map<AddSubDetailListPlaceHolderCommand>(request)));

    [Authorize(Policy = Permission.SubDetailListPlaceHolder.Edit)]
    [HttpPut("{id}")]
    public async Task<ActionResult<SubDetailListPlaceHolderState>> PutAsync(string id, [FromBody] SubDetailListPlaceHolderViewModel request)
    {
        var command = Mapper.Map<EditSubDetailListPlaceHolderCommand>(request);
        return await ToActionResult(async () => await Mediator.Send(command with { Id = id }));
    }

    [Authorize(Policy = Permission.SubDetailListPlaceHolder.Delete)]
    [HttpDelete("{id}")]
    public async Task<ActionResult<SubDetailListPlaceHolderState>> DeleteAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new DeleteSubDetailListPlaceHolderCommand { Id = id }));
}

public record SubDetailListPlaceHolderViewModel
{
    [Required]
	public string MainModulePlaceHolderId { get;set; } = "";
	[Required]
	[StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
	public string Code { get;set; } = "";
	   
}
