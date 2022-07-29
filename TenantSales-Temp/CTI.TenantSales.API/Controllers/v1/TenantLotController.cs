using CTI.Common.Utility.Models;
using CTI.TenantSales.Application.Features.TenantSales.TenantLot.Commands;
using CTI.TenantSales.Application.Features.TenantSales.TenantLot.Queries;
using CTI.TenantSales.Core.TenantSales;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using CTI.Common.API.Controllers;

namespace CTI.TenantSales.API.Controllers.v1;

[ApiVersion("1.0")]
public class TenantLotController : BaseApiController<TenantLotController>
{
    [Authorize(Policy = Permission.TenantLot.View)]
    [HttpGet]
    public async Task<ActionResult<PagedListResponse<TenantLotState>>> GetAsync([FromQuery] GetTenantLotQuery query) =>
        Ok(await Mediator.Send(query));

    [Authorize(Policy = Permission.TenantLot.View)]
    [HttpGet("{id}")]
    public async Task<ActionResult<TenantLotState>> GetAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new GetTenantLotByIdQuery(id)));

    [Authorize(Policy = Permission.TenantLot.Create)]
    [HttpPost]
    public async Task<ActionResult<TenantLotState>> PostAsync([FromBody] TenantLotViewModel request) =>
        await ToActionResult(async () => await Mediator.Send(Mapper.Map<AddTenantLotCommand>(request)));

    [Authorize(Policy = Permission.TenantLot.Edit)]
    [HttpPut("{id}")]
    public async Task<ActionResult<TenantLotState>> PutAsync(string id, [FromBody] TenantLotViewModel request)
    {
        var command = Mapper.Map<EditTenantLotCommand>(request);
        return await ToActionResult(async () => await Mediator.Send(command with { Id = id }));
    }

    [Authorize(Policy = Permission.TenantLot.Delete)]
    [HttpDelete("{id}")]
    public async Task<ActionResult<TenantLotState>> DeleteAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new DeleteTenantLotCommand { Id = id }));
}

public record TenantLotViewModel
{
    [Required]
	
	[DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
	public decimal Area { get;set; }
	[StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
	public string? LotNo { get;set; }
	[Required]
	
	public string TenantId { get;set; } = "";
	   
}
