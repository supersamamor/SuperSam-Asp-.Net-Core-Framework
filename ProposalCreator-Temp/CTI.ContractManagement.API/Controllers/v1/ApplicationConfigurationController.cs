using CTI.Common.Utility.Models;
using CTI.ContractManagement.Application.Features.ContractManagement.ApplicationConfiguration.Commands;
using CTI.ContractManagement.Application.Features.ContractManagement.ApplicationConfiguration.Queries;
using CTI.ContractManagement.Core.ContractManagement;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using CTI.Common.API.Controllers;

namespace CTI.ContractManagement.API.Controllers.v1;

[ApiVersion("1.0")]
public class ApplicationConfigurationController : BaseApiController<ApplicationConfigurationController>
{
    [Authorize(Policy = Permission.ApplicationConfiguration.View)]
    [HttpGet]
    public async Task<ActionResult<PagedListResponse<ApplicationConfigurationState>>> GetAsync([FromQuery] GetApplicationConfigurationQuery query) =>
        Ok(await Mediator.Send(query));

    [Authorize(Policy = Permission.ApplicationConfiguration.View)]
    [HttpGet("{id}")]
    public async Task<ActionResult<ApplicationConfigurationState>> GetAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new GetApplicationConfigurationByIdQuery(id)));

    [Authorize(Policy = Permission.ApplicationConfiguration.Create)]
    [HttpPost]
    public async Task<ActionResult<ApplicationConfigurationState>> PostAsync([FromBody] ApplicationConfigurationViewModel request) =>
        await ToActionResult(async () => await Mediator.Send(Mapper.Map<AddApplicationConfigurationCommand>(request)));

    [Authorize(Policy = Permission.ApplicationConfiguration.Edit)]
    [HttpPut("{id}")]
    public async Task<ActionResult<ApplicationConfigurationState>> PutAsync(string id, [FromBody] ApplicationConfigurationViewModel request)
    {
        var command = Mapper.Map<EditApplicationConfigurationCommand>(request);
        return await ToActionResult(async () => await Mediator.Send(command with { Id = id }));
    }

    [Authorize(Policy = Permission.ApplicationConfiguration.Delete)]
    [HttpDelete("{id}")]
    public async Task<ActionResult<ApplicationConfigurationState>> DeleteAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new DeleteApplicationConfigurationCommand { Id = id }));
}

public record ApplicationConfigurationViewModel
{
    [Required]
	public string? Logo { get;set; } = "";
	[Required]
	[StringLength(400, ErrorMessage = "{0} length can't be more than {1}.")]
	public string AddressLineOne { get;set; } = "";
	[StringLength(400, ErrorMessage = "{0} length can't be more than {1}.")]
	public string? AddressLineTwo { get;set; }
	[Required]
	
	public string OrganizationOverview { get;set; } = "";
	[Required]
	
	public string DocumentFooter { get;set; } = "";
	   
}
