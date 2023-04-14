using CelerSoft.Common.Utility.Models;
using CelerSoft.TurboERP.Application.Features.TurboERP.Customer.Commands;
using CelerSoft.TurboERP.Application.Features.TurboERP.Customer.Queries;
using CelerSoft.TurboERP.Core.TurboERP;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using CelerSoft.Common.API.Controllers;

namespace CelerSoft.TurboERP.API.Controllers.v1;

[ApiVersion("1.0")]
public class CustomerController : BaseApiController<CustomerController>
{
    [Authorize(Policy = Permission.Customer.View)]
    [HttpGet]
    public async Task<ActionResult<PagedListResponse<CustomerState>>> GetAsync([FromQuery] GetCustomerQuery query) =>
        Ok(await Mediator.Send(query));

    [Authorize(Policy = Permission.Customer.View)]
    [HttpGet("{id}")]
    public async Task<ActionResult<CustomerState>> GetAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new GetCustomerByIdQuery(id)));

    [Authorize(Policy = Permission.Customer.Create)]
    [HttpPost]
    public async Task<ActionResult<CustomerState>> PostAsync([FromBody] CustomerViewModel request) =>
        await ToActionResult(async () => await Mediator.Send(Mapper.Map<AddCustomerCommand>(request)));

    [Authorize(Policy = Permission.Customer.Edit)]
    [HttpPut("{id}")]
    public async Task<ActionResult<CustomerState>> PutAsync(string id, [FromBody] CustomerViewModel request)
    {
        var command = Mapper.Map<EditCustomerCommand>(request);
        return await ToActionResult(async () => await Mediator.Send(command with { Id = id }));
    }

    [Authorize(Policy = Permission.Customer.Delete)]
    [HttpDelete("{id}")]
    public async Task<ActionResult<CustomerState>> DeleteAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new DeleteCustomerCommand { Id = id }));
}

public record CustomerViewModel
{
    [Required]
	[StringLength(450, ErrorMessage = "{0} length can't be more than {1}.")]
	public string Company { get;set; } = "";
	[StringLength(20, ErrorMessage = "{0} length can't be more than {1}.")]
	public string? TINNumber { get;set; }
	[Required]
	[StringLength(1000, ErrorMessage = "{0} length can't be more than {1}.")]
	public string Address { get;set; } = "";
	   
}
