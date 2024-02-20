using CTI.Common.Utility.Models;
using CTI.ContractManagement.Application.Features.ContractManagement.Project.Commands;
using CTI.ContractManagement.Application.Features.ContractManagement.Project.Queries;
using CTI.ContractManagement.Core.ContractManagement;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using CTI.Common.API.Controllers;

namespace CTI.ContractManagement.API.Controllers.v1;

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
	
	public string ClientId { get;set; } = "";
	[Required]
	[StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
	public string ProjectName { get;set; } = "";
	[Required]
	[StringLength(500, ErrorMessage = "{0} length can't be more than {1}.")]
	public string ProjectDescription { get;set; } = "";
	public string? Logo { get;set; }
	[Required]
	[StringLength(500, ErrorMessage = "{0} length can't be more than {1}.")]
	public string ProjectGoals { get;set; } = "";
	
	[DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
	public decimal? Discount { get;set; }
	[Required]
	
	public string PricingTypeId { get;set; } = "";
	public bool EnablePricing { get;set; }
	
	public string? Template { get;set; }
	[Required]
	[StringLength(400, ErrorMessage = "{0} length can't be more than {1}.")]
	public string RevisionSummary { get;set; } = "";
	[Required]
	[StringLength(50, ErrorMessage = "{0} length can't be more than {1}.")]
	public string DocumentCode { get;set; } = "";
	   
}
