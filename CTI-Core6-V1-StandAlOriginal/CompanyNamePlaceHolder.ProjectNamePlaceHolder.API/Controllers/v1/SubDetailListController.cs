using CompanyNamePlaceHolder.Common.Utility.Models;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Features.ProjectNamePlaceHolder.SubDetailList.Commands;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Features.ProjectNamePlaceHolder.SubDetailList.Queries;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Core.AreaPlaceHolder;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using CompanyNamePlaceHolder.Common.API.Controllers;

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.API.Controllers.v1;

[ApiVersion("1.0")]
public class SubDetailListController : BaseApiController<SubDetailListController>
{
    [Authorize(Policy = Permission.SubDetailList.View)]
    [HttpGet]
    public async Task<ActionResult<PagedListResponse<SubDetailListState>>> GetAsync([FromQuery] GetSubDetailListQuery query) =>
        Ok(await Mediator.Send(query));

    [Authorize(Policy = Permission.SubDetailList.View)]
    [HttpGet("{id}")]
    public async Task<ActionResult<SubDetailListState>> GetAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new GetSubDetailListByIdQuery(id)));

    [Authorize(Policy = Permission.SubDetailList.Create)]
    [HttpPost]
    public async Task<ActionResult<SubDetailListState>> PostAsync([FromBody] SubDetailListViewModel request) =>
        await ToActionResult(async () => await Mediator.Send(Mapper.Map<AddSubDetailListCommand>(request)));

    [Authorize(Policy = Permission.SubDetailList.Edit)]
    [HttpPut("{id}")]
    public async Task<ActionResult<SubDetailListState>> PutAsync(string id, [FromBody] SubDetailListViewModel request)
    {
        var command = Mapper.Map<EditSubDetailListCommand>(request);
        return await ToActionResult(async () => await Mediator.Send(command with { Id = id }));
    }

    [Authorize(Policy = Permission.SubDetailList.Delete)]
    [HttpDelete("{id}")]
    public async Task<ActionResult<SubDetailListState>> DeleteAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new DeleteSubDetailListCommand { Id = id }));
}

public record SubDetailListViewModel
{
    [Required]
	[StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
	public string Code { get;set; } = "";
	[Required]
	public string TestForeignKeyOne { get;set; } = "";
	   
}
