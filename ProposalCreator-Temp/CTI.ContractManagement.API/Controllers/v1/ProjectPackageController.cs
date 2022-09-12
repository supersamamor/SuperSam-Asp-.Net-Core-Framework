using CTI.Common.Utility.Models;
using CTI.ContractManagement.Application.Features.ContractManagement.ProjectPackage.Commands;
using CTI.ContractManagement.Application.Features.ContractManagement.ProjectPackage.Queries;
using CTI.ContractManagement.Core.ContractManagement;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using CTI.Common.API.Controllers;

namespace CTI.ContractManagement.API.Controllers.v1;

[ApiVersion("1.0")]
public class ProjectPackageController : BaseApiController<ProjectPackageController>
{
    [Authorize(Policy = Permission.ProjectPackage.View)]
    [HttpGet]
    public async Task<ActionResult<PagedListResponse<ProjectPackageState>>> GetAsync([FromQuery] GetProjectPackageQuery query) =>
        Ok(await Mediator.Send(query));

    [Authorize(Policy = Permission.ProjectPackage.View)]
    [HttpGet("{id}")]
    public async Task<ActionResult<ProjectPackageState>> GetAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new GetProjectPackageByIdQuery(id)));

    [Authorize(Policy = Permission.ProjectPackage.Create)]
    [HttpPost]
    public async Task<ActionResult<ProjectPackageState>> PostAsync([FromBody] ProjectPackageViewModel request) =>
        await ToActionResult(async () => await Mediator.Send(Mapper.Map<AddProjectPackageCommand>(request)));

    [Authorize(Policy = Permission.ProjectPackage.Edit)]
    [HttpPut("{id}")]
    public async Task<ActionResult<ProjectPackageState>> PutAsync(string id, [FromBody] ProjectPackageViewModel request)
    {
        var command = Mapper.Map<EditProjectPackageCommand>(request);
        return await ToActionResult(async () => await Mediator.Send(command with { Id = id }));
    }

    [Authorize(Policy = Permission.ProjectPackage.Delete)]
    [HttpDelete("{id}")]
    public async Task<ActionResult<ProjectPackageState>> DeleteAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new DeleteProjectPackageCommand { Id = id }));
}

public record ProjectPackageViewModel
{
    [Required]
	
	public string ProjectId { get;set; } = "";
	public int? OptionNumber { get;set; }
	
	[DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
	public decimal? Amount { get;set; }
	   
}
