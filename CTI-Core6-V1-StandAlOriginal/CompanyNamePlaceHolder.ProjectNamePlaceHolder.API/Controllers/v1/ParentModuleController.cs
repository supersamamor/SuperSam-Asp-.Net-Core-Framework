using CompanyNamePlaceHolder.Common.Utility.Models;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Features.ProjectNamePlaceHolder.ParentModule.Commands;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Features.ProjectNamePlaceHolder.ParentModule.Queries;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Core.ProjectNamePlaceHolder;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using CompanyNamePlaceHolder.Common.API.Controllers;

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.API.Controllers.v1;

[ApiVersion("1.0")]
public class ParentModuleController : BaseApiController<ParentModuleController>
{
    [Authorize(Policy = Permission.ParentModule.View)]
    [HttpGet]
    public async Task<ActionResult<PagedListResponse<ParentModuleState>>> GetAsync([FromQuery] GetParentModuleQuery query) =>
        Ok(await Mediator.Send(query));

    [Authorize(Policy = Permission.ParentModule.View)]
    [HttpGet("{id}")]
    public async Task<ActionResult<ParentModuleState>> GetAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new GetParentModuleByIdQuery(id)));

    [Authorize(Policy = Permission.ParentModule.Create)]
    [HttpPost]
    public async Task<ActionResult<ParentModuleState>> PostAsync([FromBody] ParentModuleViewModel request) =>
        await ToActionResult(async () => await Mediator.Send(Mapper.Map<AddParentModuleCommand>(request)));

    [Authorize(Policy = Permission.ParentModule.Edit)]
    [HttpPut("{id}")]
    public async Task<ActionResult<ParentModuleState>> PutAsync(string id, [FromBody] ParentModuleViewModel request)
    {
        var command = Mapper.Map<EditParentModuleCommand>(request);
        return await ToActionResult(async () => await Mediator.Send(command with { Id = id }));
    }

    [Authorize(Policy = Permission.ParentModule.Delete)]
    [HttpDelete("{id}")]
    public async Task<ActionResult<ParentModuleState>> DeleteAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new DeleteParentModuleCommand { Id = id }));
}

public record ParentModuleViewModel
{
    [Required]
	[StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
	public string Name { get;set; } = "";
	   
}
