using CTI.Common.Utility.Models;
using CTI.ContractManagement.Application.Features.ContractManagement.Client.Commands;
using CTI.ContractManagement.Application.Features.ContractManagement.Client.Queries;
using CTI.ContractManagement.Core.ContractManagement;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using CTI.Common.API.Controllers;

namespace CTI.ContractManagement.API.Controllers.v1;

[ApiVersion("1.0")]
public class ClientController : BaseApiController<ClientController>
{
    [Authorize(Policy = Permission.Client.View)]
    [HttpGet]
    public async Task<ActionResult<PagedListResponse<ClientState>>> GetAsync([FromQuery] GetClientQuery query) =>
        Ok(await Mediator.Send(query));

    [Authorize(Policy = Permission.Client.View)]
    [HttpGet("{id}")]
    public async Task<ActionResult<ClientState>> GetAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new GetClientByIdQuery(id)));

    [Authorize(Policy = Permission.Client.Create)]
    [HttpPost]
    public async Task<ActionResult<ClientState>> PostAsync([FromBody] ClientViewModel request) =>
        await ToActionResult(async () => await Mediator.Send(Mapper.Map<AddClientCommand>(request)));

    [Authorize(Policy = Permission.Client.Edit)]
    [HttpPut("{id}")]
    public async Task<ActionResult<ClientState>> PutAsync(string id, [FromBody] ClientViewModel request)
    {
        var command = Mapper.Map<EditClientCommand>(request);
        return await ToActionResult(async () => await Mediator.Send(command with { Id = id }));
    }

    [Authorize(Policy = Permission.Client.Delete)]
    [HttpDelete("{id}")]
    public async Task<ActionResult<ClientState>> DeleteAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new DeleteClientCommand { Id = id }));
}

public record ClientViewModel
{
    [Required]
	[StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
	public string ContactPersonName { get;set; } = "";
	[Required]
	[StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
	public string ContactPersonPosition { get;set; } = "";
	[StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
	public string? CompanyName { get;set; }
	[StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
	public string? CompanyDescription { get;set; }
	[Required]
	[StringLength(400, ErrorMessage = "{0} length can't be more than {1}.")]
	public string CompanyAddressLineOne { get;set; } = "";
	[StringLength(400, ErrorMessage = "{0} length can't be more than {1}.")]
	public string? CompanyAddressLineTwo { get;set; }
	[Required]
	[StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
	public string EmailAddress { get;set; } = "";
	   
}
