using CTI.Common.Utility.Models;
using CTI.TenantSales.Application.Features.TenantSales.BusinessUnit.Commands;
using CTI.TenantSales.Application.Features.TenantSales.BusinessUnit.Queries;
using CTI.TenantSales.Core.TenantSales;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using CTI.Common.API.Controllers;

namespace CTI.TenantSales.API.Controllers.v1;

[ApiVersion("1.0")]
public class BusinessUnitController : BaseApiController<BusinessUnitController>
{
    [Authorize(Policy = Permission.BusinessUnit.View)]
    [HttpGet]
    public async Task<ActionResult<PagedListResponse<BusinessUnitState>>> GetAsync([FromQuery] GetBusinessUnitQuery query) =>
        Ok(await Mediator.Send(query));

    [Authorize(Policy = Permission.BusinessUnit.View)]
    [HttpGet("{id}")]
    public async Task<ActionResult<BusinessUnitState>> GetAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new GetBusinessUnitByIdQuery(id)));

    [Authorize(Policy = Permission.BusinessUnit.Create)]
    [HttpPost]
    public async Task<ActionResult<BusinessUnitState>> PostAsync([FromBody] BusinessUnitViewModel request) =>
        await ToActionResult(async () => await Mediator.Send(Mapper.Map<AddBusinessUnitCommand>(request)));

    [Authorize(Policy = Permission.BusinessUnit.Edit)]
    [HttpPut("{id}")]
    public async Task<ActionResult<BusinessUnitState>> PutAsync(string id, [FromBody] BusinessUnitViewModel request)
    {
        var command = Mapper.Map<EditBusinessUnitCommand>(request);
        return await ToActionResult(async () => await Mediator.Send(command with { Id = id }));
    }

    [Authorize(Policy = Permission.BusinessUnit.Delete)]
    [HttpDelete("{id}")]
    public async Task<ActionResult<BusinessUnitState>> DeleteAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new DeleteBusinessUnitCommand { Id = id }));
}

public record BusinessUnitViewModel
{
    [Required]
	[StringLength(50, ErrorMessage = "{0} length can't be more than {1}.")]
	public string Name { get;set; } = "";
	public bool IsDisabled { get;set; }
	   
}
