using CelerSoft.Common.Utility.Models;
using CelerSoft.TurboERP.Application.Features.TurboERP.CustomerContactPerson.Commands;
using CelerSoft.TurboERP.Application.Features.TurboERP.CustomerContactPerson.Queries;
using CelerSoft.TurboERP.Core.TurboERP;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using CelerSoft.Common.API.Controllers;

namespace CelerSoft.TurboERP.API.Controllers.v1;

[ApiVersion("1.0")]
public class CustomerContactPersonController : BaseApiController<CustomerContactPersonController>
{
    [Authorize(Policy = Permission.CustomerContactPerson.View)]
    [HttpGet]
    public async Task<ActionResult<PagedListResponse<CustomerContactPersonState>>> GetAsync([FromQuery] GetCustomerContactPersonQuery query) =>
        Ok(await Mediator.Send(query));

    [Authorize(Policy = Permission.CustomerContactPerson.View)]
    [HttpGet("{id}")]
    public async Task<ActionResult<CustomerContactPersonState>> GetAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new GetCustomerContactPersonByIdQuery(id)));

    [Authorize(Policy = Permission.CustomerContactPerson.Create)]
    [HttpPost]
    public async Task<ActionResult<CustomerContactPersonState>> PostAsync([FromBody] CustomerContactPersonViewModel request) =>
        await ToActionResult(async () => await Mediator.Send(Mapper.Map<AddCustomerContactPersonCommand>(request)));

    [Authorize(Policy = Permission.CustomerContactPerson.Edit)]
    [HttpPut("{id}")]
    public async Task<ActionResult<CustomerContactPersonState>> PutAsync(string id, [FromBody] CustomerContactPersonViewModel request)
    {
        var command = Mapper.Map<EditCustomerContactPersonCommand>(request);
        return await ToActionResult(async () => await Mediator.Send(command with { Id = id }));
    }

    [Authorize(Policy = Permission.CustomerContactPerson.Delete)]
    [HttpDelete("{id}")]
    public async Task<ActionResult<CustomerContactPersonState>> DeleteAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new DeleteCustomerContactPersonCommand { Id = id }));
}

public record CustomerContactPersonViewModel
{
    
	public string? CustomerId { get;set; }
	[StringLength(450, ErrorMessage = "{0} length can't be more than {1}.")]
	public string? FullName { get;set; }
	[StringLength(100, ErrorMessage = "{0} length can't be more than {1}.")]
	public string? Position { get;set; }
	[Required]
	[StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
	public string Email { get;set; } = "";
	[Required]
	[StringLength(50, ErrorMessage = "{0} length can't be more than {1}.")]
	public string MobileNumber { get;set; } = "";
	[StringLength(50, ErrorMessage = "{0} length can't be more than {1}.")]
	public string? PhoneNumber { get;set; }
	   
}
