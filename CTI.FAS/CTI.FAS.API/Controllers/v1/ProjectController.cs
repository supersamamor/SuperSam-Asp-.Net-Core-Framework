using CTI.Common.Utility.Models;
using CTI.FAS.Application.Features.FAS.Project.Commands;
using CTI.FAS.Application.Features.FAS.Project.Queries;
using CTI.FAS.Core.FAS;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using CTI.Common.API.Controllers;

namespace CTI.FAS.API.Controllers.v1;

[ApiVersion("1.0")]
public class ProjectController : BaseApiController<ProjectController>
{
    [Authorize(Policy = Permission.Project.View)]
    [HttpGet]
    public async Task<ActionResult<PagedListResponse<ProjectState>>> GetAsync([FromQuery] GetProjectQuery query) =>
        Ok(await Mediator.Send(query));

    [Authorize(Policy = Permission.Project.View)]
    [HttpGet("{id}")]
    public async Task<ActionResult<ProjectState>> GetAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new GetProjectByIdQuery(id)));

    [Authorize(Policy = Permission.Project.Create)]
    [HttpPost]
    public async Task<ActionResult<ProjectState>> PostAsync([FromBody] ProjectViewModel request) =>
        await ToActionResult(async () => await Mediator.Send(Mapper.Map<AddProjectCommand>(request)));

    [Authorize(Policy = Permission.Project.Edit)]
    [HttpPut("{id}")]
    public async Task<ActionResult<ProjectState>> PutAsync(string id, [FromBody] ProjectViewModel request)
    {
        var command = Mapper.Map<EditProjectCommand>(request);
        return await ToActionResult(async () => await Mediator.Send(command with { Id = id }));
    }

    [Authorize(Policy = Permission.Project.Delete)]
    [HttpDelete("{id}")]
    public async Task<ActionResult<ProjectState>> DeleteAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new DeleteProjectCommand { Id = id }));
}

public record ProjectViewModel
{
    [Required]
	
	public string CompanyId { get;set; } = "";
	[Required]
	[StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
	public string Name { get;set; } = "";
	[Required]
	[StringLength(20, ErrorMessage = "{0} length can't be more than {1}.")]
	public string Code { get;set; } = "";
	[StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
	public string? ProjectAddress { get;set; }
	[StringLength(100, ErrorMessage = "{0} length can't be more than {1}.")]
	public string? Location { get;set; }
	[Required]
	
	[DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
	public decimal LandArea { get;set; }
	[Required]
	
	[DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
	public decimal GLA { get;set; }
	public bool IsDisabled { get;set; }
	   
}
