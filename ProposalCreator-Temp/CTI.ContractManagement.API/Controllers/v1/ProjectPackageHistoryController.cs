using CTI.Common.Utility.Models;
using CTI.ContractManagement.Application.Features.ContractManagement.ProjectPackageHistory.Commands;
using CTI.ContractManagement.Application.Features.ContractManagement.ProjectPackageHistory.Queries;
using CTI.ContractManagement.Core.ContractManagement;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using CTI.Common.API.Controllers;

namespace CTI.ContractManagement.API.Controllers.v1;

[ApiVersion("1.0")]
public class ProjectPackageHistoryController : BaseApiController<ProjectPackageHistoryController>
{
    [Authorize(Policy = Permission.ProjectPackageHistory.View)]
    [HttpGet]
    public async Task<ActionResult<PagedListResponse<ProjectPackageHistoryState>>> GetAsync([FromQuery] GetProjectPackageHistoryQuery query) =>
        Ok(await Mediator.Send(query));

    [Authorize(Policy = Permission.ProjectPackageHistory.View)]
    [HttpGet("{id}")]
    public async Task<ActionResult<ProjectPackageHistoryState>> GetAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new GetProjectPackageHistoryByIdQuery(id)));

    [Authorize(Policy = Permission.ProjectPackageHistory.Create)]
    [HttpPost]
    public async Task<ActionResult<ProjectPackageHistoryState>> PostAsync([FromBody] ProjectPackageHistoryViewModel request) =>
        await ToActionResult(async () => await Mediator.Send(Mapper.Map<AddProjectPackageHistoryCommand>(request)));

    [Authorize(Policy = Permission.ProjectPackageHistory.Edit)]
    [HttpPut("{id}")]
    public async Task<ActionResult<ProjectPackageHistoryState>> PutAsync(string id, [FromBody] ProjectPackageHistoryViewModel request)
    {
        var command = Mapper.Map<EditProjectPackageHistoryCommand>(request);
        return await ToActionResult(async () => await Mediator.Send(command with { Id = id }));
    }

    [Authorize(Policy = Permission.ProjectPackageHistory.Delete)]
    [HttpDelete("{id}")]
    public async Task<ActionResult<ProjectPackageHistoryState>> DeleteAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new DeleteProjectPackageHistoryCommand { Id = id }));
}

public record ProjectPackageHistoryViewModel
{
    [Required]
	
	public string ProjectHistoryId { get;set; } = "";
	public int? OptionNumber { get;set; }
	
	[DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
	public decimal? Amount { get;set; }
	   
}
