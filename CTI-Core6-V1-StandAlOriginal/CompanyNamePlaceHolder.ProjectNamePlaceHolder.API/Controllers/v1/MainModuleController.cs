using CompanyNamePlaceHolder.Common.Utility.Models;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Features.ProjectNamePlaceHolder.MainModule.Commands;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Features.ProjectNamePlaceHolder.MainModule.Queries;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Core.ProjectNamePlaceHolder;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using CompanyNamePlaceHolder.Common.API.Controllers;

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.API.Controllers.v1;

[ApiVersion("1.0")]
public class MainModuleController : BaseApiController<MainModuleController>
{
    [Authorize(Policy = Permission.MainModule.View)]
    [HttpGet]
    public async Task<ActionResult<PagedListResponse<MainModuleState>>> GetAsync([FromQuery] GetMainModuleQuery query) =>
        Ok(await Mediator.Send(query));

    [Authorize(Policy = Permission.MainModule.View)]
    [HttpGet("{id}")]
    public async Task<ActionResult<MainModuleState>> GetAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new GetMainModuleByIdQuery(id)));

    [Authorize(Policy = Permission.MainModule.Create)]
    [HttpPost]
    public async Task<ActionResult<MainModuleState>> PostAsync([FromBody] MainModuleViewModel request) =>
        await ToActionResult(async () => await Mediator.Send(Mapper.Map<AddMainModuleCommand>(request)));

    [Authorize(Policy = Permission.MainModule.Edit)]
    [HttpPut("{id}")]
    public async Task<ActionResult<MainModuleState>> PutAsync(string id, [FromBody] MainModuleViewModel request)
    {
        var command = Mapper.Map<EditMainModuleCommand>(request);
        return await ToActionResult(async () => await Mediator.Send(command with { Id = id }));
    }

    [Authorize(Policy = Permission.MainModule.Delete)]
    [HttpDelete("{id}")]
    public async Task<ActionResult<MainModuleState>> DeleteAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new DeleteMainModuleCommand { Id = id }));
}

public record MainModuleViewModel
{
    [Required]
	public string ParentModuleId { get;set; } = "";
	[Required]
	public string FileUpload { get;set; }
	[Required]
	[StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
	public string Code { get;set; } = "";
	   
}
