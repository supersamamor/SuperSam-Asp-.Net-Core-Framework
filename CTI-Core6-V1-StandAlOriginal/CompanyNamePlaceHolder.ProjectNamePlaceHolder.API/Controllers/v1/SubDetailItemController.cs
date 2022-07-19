using CompanyNamePlaceHolder.Common.Utility.Models;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Features.ProjectNamePlaceHolder.SubDetailItem.Commands;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Features.ProjectNamePlaceHolder.SubDetailItem.Queries;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Core.ProjectNamePlaceHolder;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using CompanyNamePlaceHolder.Common.API.Controllers;

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.API.Controllers.v1;

[ApiVersion("1.0")]
public class SubDetailItemController : BaseApiController<SubDetailItemController>
{
    [Authorize(Policy = Permission.SubDetailItem.View)]
    [HttpGet]
    public async Task<ActionResult<PagedListResponse<SubDetailItemState>>> GetAsync([FromQuery] GetSubDetailItemQuery query) =>
        Ok(await Mediator.Send(query));

    [Authorize(Policy = Permission.SubDetailItem.View)]
    [HttpGet("{id}")]
    public async Task<ActionResult<SubDetailItemState>> GetAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new GetSubDetailItemByIdQuery(id)));

    [Authorize(Policy = Permission.SubDetailItem.Create)]
    [HttpPost]
    public async Task<ActionResult<SubDetailItemState>> PostAsync([FromBody] SubDetailItemViewModel request) =>
        await ToActionResult(async () => await Mediator.Send(Mapper.Map<AddSubDetailItemCommand>(request)));

    [Authorize(Policy = Permission.SubDetailItem.Edit)]
    [HttpPut("{id}")]
    public async Task<ActionResult<SubDetailItemState>> PutAsync(string id, [FromBody] SubDetailItemViewModel request)
    {
        var command = Mapper.Map<EditSubDetailItemCommand>(request);
        return await ToActionResult(async () => await Mediator.Send(command with { Id = id }));
    }

    [Authorize(Policy = Permission.SubDetailItem.Delete)]
    [HttpDelete("{id}")]
    public async Task<ActionResult<SubDetailItemState>> DeleteAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new DeleteSubDetailItemCommand { Id = id }));
}

public record SubDetailItemViewModel
{
    [Required]
	public string TestForeignKeyTwo { get;set; } = "";
	[Required]
	[StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
	public string Code { get;set; } = "";
	   
}
