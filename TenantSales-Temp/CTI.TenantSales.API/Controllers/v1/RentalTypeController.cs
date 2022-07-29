using CTI.Common.Utility.Models;
using CTI.TenantSales.Application.Features.TenantSales.RentalType.Commands;
using CTI.TenantSales.Application.Features.TenantSales.RentalType.Queries;
using CTI.TenantSales.Core.TenantSales;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using CTI.Common.API.Controllers;

namespace CTI.TenantSales.API.Controllers.v1;

[ApiVersion("1.0")]
public class RentalTypeController : BaseApiController<RentalTypeController>
{
    [Authorize(Policy = Permission.RentalType.View)]
    [HttpGet]
    public async Task<ActionResult<PagedListResponse<RentalTypeState>>> GetAsync([FromQuery] GetRentalTypeQuery query) =>
        Ok(await Mediator.Send(query));

    [Authorize(Policy = Permission.RentalType.View)]
    [HttpGet("{id}")]
    public async Task<ActionResult<RentalTypeState>> GetAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new GetRentalTypeByIdQuery(id)));

    [Authorize(Policy = Permission.RentalType.Create)]
    [HttpPost]
    public async Task<ActionResult<RentalTypeState>> PostAsync([FromBody] RentalTypeViewModel request) =>
        await ToActionResult(async () => await Mediator.Send(Mapper.Map<AddRentalTypeCommand>(request)));

    [Authorize(Policy = Permission.RentalType.Edit)]
    [HttpPut("{id}")]
    public async Task<ActionResult<RentalTypeState>> PutAsync(string id, [FromBody] RentalTypeViewModel request)
    {
        var command = Mapper.Map<EditRentalTypeCommand>(request);
        return await ToActionResult(async () => await Mediator.Send(command with { Id = id }));
    }

    [Authorize(Policy = Permission.RentalType.Delete)]
    [HttpDelete("{id}")]
    public async Task<ActionResult<RentalTypeState>> DeleteAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new DeleteRentalTypeCommand { Id = id }));
}

public record RentalTypeViewModel
{
    [Required]
	[StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
	public string Name { get;set; } = "";
	   
}
