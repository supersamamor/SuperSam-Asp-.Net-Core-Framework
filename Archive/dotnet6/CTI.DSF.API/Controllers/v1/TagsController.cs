using CTI.Common.Utility.Models;
using CTI.DSF.Application.Features.DSF.Tags.Commands;
using CTI.DSF.Application.Features.DSF.Tags.Queries;
using CTI.DSF.Core.DSF;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using CTI.Common.API.Controllers;

namespace CTI.DSF.API.Controllers.v1;

[ApiVersion("1.0")]
public class TagsController : BaseApiController<TagsController>
{
    [Authorize(Policy = Permission.Tags.View)]
    [HttpGet]
    public async Task<ActionResult<PagedListResponse<TagsState>>> GetAsync([FromQuery] GetTagsQuery query) =>
        Ok(await Mediator.Send(query));

    [Authorize(Policy = Permission.Tags.View)]
    [HttpGet("{id}")]
    public async Task<ActionResult<TagsState>> GetAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new GetTagsByIdQuery(id)));

    [Authorize(Policy = Permission.Tags.Create)]
    [HttpPost]
    public async Task<ActionResult<TagsState>> PostAsync([FromBody] TagsViewModel request) =>
        await ToActionResult(async () => await Mediator.Send(Mapper.Map<AddTagsCommand>(request)));

    [Authorize(Policy = Permission.Tags.Edit)]
    [HttpPut("{id}")]
    public async Task<ActionResult<TagsState>> PutAsync(string id, [FromBody] TagsViewModel request)
    {
        var command = Mapper.Map<EditTagsCommand>(request);
        return await ToActionResult(async () => await Mediator.Send(command with { Id = id }));
    }

    [Authorize(Policy = Permission.Tags.Delete)]
    [HttpDelete("{id}")]
    public async Task<ActionResult<TagsState>> DeleteAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new DeleteTagsCommand { Id = id }));
}

public record TagsViewModel
{
    [Required]
	[StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
	public string Name { get;set; } = "";
	   
}
