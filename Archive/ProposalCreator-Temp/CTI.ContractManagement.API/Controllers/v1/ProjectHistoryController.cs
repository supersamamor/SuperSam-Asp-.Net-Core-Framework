using CTI.Common.Utility.Models;
using CTI.ContractManagement.Application.Features.ContractManagement.ProjectHistory.Commands;
using CTI.ContractManagement.Application.Features.ContractManagement.ProjectHistory.Queries;
using CTI.ContractManagement.Core.ContractManagement;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using CTI.Common.API.Controllers;

namespace CTI.ContractManagement.API.Controllers.v1;

[ApiVersion("1.0")]
public class ProjectHistoryController : BaseApiController<ProjectHistoryController>
{
    [Authorize(Policy = Permission.ProjectHistory.View)]
    [HttpGet]
    public async Task<ActionResult<PagedListResponse<ProjectHistoryState>>> GetAsync([FromQuery] GetProjectHistoryQuery query) =>
        Ok(await Mediator.Send(query));

    [Authorize(Policy = Permission.ProjectHistory.View)]
    [HttpGet("{id}")]
    public async Task<ActionResult<ProjectHistoryState>> GetAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new GetProjectHistoryByIdQuery(id)));

    [Authorize(Policy = Permission.ProjectHistory.Create)]
    [HttpPost]
    public async Task<ActionResult<ProjectHistoryState>> PostAsync([FromBody] ProjectHistoryViewModel request) =>
        await ToActionResult(async () => await Mediator.Send(Mapper.Map<AddProjectHistoryCommand>(request)));

    [Authorize(Policy = Permission.ProjectHistory.Edit)]
    [HttpPut("{id}")]
    public async Task<ActionResult<ProjectHistoryState>> PutAsync(string id, [FromBody] ProjectHistoryViewModel request)
    {
        var command = Mapper.Map<EditProjectHistoryCommand>(request);
        return await ToActionResult(async () => await Mediator.Send(command with { Id = id }));
    }

    [Authorize(Policy = Permission.ProjectHistory.Delete)]
    [HttpDelete("{id}")]
    public async Task<ActionResult<ProjectHistoryState>> DeleteAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new DeleteProjectHistoryCommand { Id = id }));
}

public record ProjectHistoryViewModel
{
    [Required]
	
	public string ProjectId { get;set; } = "";
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
