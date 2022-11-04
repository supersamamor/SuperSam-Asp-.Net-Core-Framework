using CTI.Common.Utility.Models;
using CTI.ELMS.Application.Features.ELMS.EntityGroup.Commands;
using CTI.ELMS.Application.Features.ELMS.EntityGroup.Queries;
using CTI.ELMS.Core.ELMS;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using CTI.Common.API.Controllers;

namespace CTI.ELMS.API.Controllers.v1;

[ApiVersion("1.0")]
public class EntityGroupController : BaseApiController<EntityGroupController>
{
    [Authorize(Policy = Permission.EntityGroup.View)]
    [HttpGet]
    public async Task<ActionResult<PagedListResponse<EntityGroupState>>> GetAsync([FromQuery] GetEntityGroupQuery query) =>
        Ok(await Mediator.Send(query));

    [Authorize(Policy = Permission.EntityGroup.View)]
    [HttpGet("{id}")]
    public async Task<ActionResult<EntityGroupState>> GetAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new GetEntityGroupByIdQuery(id)));

    [Authorize(Policy = Permission.EntityGroup.Create)]
    [HttpPost]
    public async Task<ActionResult<EntityGroupState>> PostAsync([FromBody] EntityGroupViewModel request) =>
        await ToActionResult(async () => await Mediator.Send(Mapper.Map<AddEntityGroupCommand>(request)));

    [Authorize(Policy = Permission.EntityGroup.Edit)]
    [HttpPut("{id}")]
    public async Task<ActionResult<EntityGroupState>> PutAsync(string id, [FromBody] EntityGroupViewModel request)
    {
        var command = Mapper.Map<EditEntityGroupCommand>(request);
        return await ToActionResult(async () => await Mediator.Send(command with { Id = id }));
    }

    [Authorize(Policy = Permission.EntityGroup.Delete)]
    [HttpDelete("{id}")]
    public async Task<ActionResult<EntityGroupState>> DeleteAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new DeleteEntityGroupCommand { Id = id }));
}

public record EntityGroupViewModel
{
    
	public string? PPlusConnectionSetupID { get;set; }
	[Required]
	[StringLength(100, ErrorMessage = "{0} length can't be more than {1}.")]
	public string EntityName { get;set; } = "";
	[StringLength(5, ErrorMessage = "{0} length can't be more than {1}.")]
	public string? PPLUSEntityCode { get;set; }
	[Required]
	[StringLength(20, ErrorMessage = "{0} length can't be more than {1}.")]
	public string EntityShortName { get;set; } = "";
	[Required]
	[StringLength(17, ErrorMessage = "{0} length can't be more than {1}.")]
	public string TINNo { get;set; } = "";
	[Required]
	[StringLength(100, ErrorMessage = "{0} length can't be more than {1}.")]
	public string EntityDescription { get;set; } = "";
	[Required]
	[StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
	public string EntityAddress { get;set; } = "";
	[StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
	public string? EntityAddress2 { get;set; }
	public bool IsDisabled { get;set; }
	   
}
