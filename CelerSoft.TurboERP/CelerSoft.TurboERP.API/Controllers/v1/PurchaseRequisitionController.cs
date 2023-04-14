using CelerSoft.Common.Utility.Models;
using CelerSoft.TurboERP.Application.Features.TurboERP.PurchaseRequisition.Commands;
using CelerSoft.TurboERP.Application.Features.TurboERP.PurchaseRequisition.Queries;
using CelerSoft.TurboERP.Core.TurboERP;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using CelerSoft.Common.API.Controllers;

namespace CelerSoft.TurboERP.API.Controllers.v1;

[ApiVersion("1.0")]
public class PurchaseRequisitionController : BaseApiController<PurchaseRequisitionController>
{
    [Authorize(Policy = Permission.PurchaseRequisition.View)]
    [HttpGet]
    public async Task<ActionResult<PagedListResponse<PurchaseRequisitionState>>> GetAsync([FromQuery] GetPurchaseRequisitionQuery query) =>
        Ok(await Mediator.Send(query));

    [Authorize(Policy = Permission.PurchaseRequisition.View)]
    [HttpGet("{id}")]
    public async Task<ActionResult<PurchaseRequisitionState>> GetAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new GetPurchaseRequisitionByIdQuery(id)));

    [Authorize(Policy = Permission.PurchaseRequisition.Create)]
    [HttpPost]
    public async Task<ActionResult<PurchaseRequisitionState>> PostAsync([FromBody] PurchaseRequisitionViewModel request) =>
        await ToActionResult(async () => await Mediator.Send(Mapper.Map<AddPurchaseRequisitionCommand>(request)));

    [Authorize(Policy = Permission.PurchaseRequisition.Edit)]
    [HttpPut("{id}")]
    public async Task<ActionResult<PurchaseRequisitionState>> PutAsync(string id, [FromBody] PurchaseRequisitionViewModel request)
    {
        var command = Mapper.Map<EditPurchaseRequisitionCommand>(request);
        return await ToActionResult(async () => await Mediator.Send(command with { Id = id }));
    }

    [Authorize(Policy = Permission.PurchaseRequisition.Delete)]
    [HttpDelete("{id}")]
    public async Task<ActionResult<PurchaseRequisitionState>> DeleteAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new DeletePurchaseRequisitionCommand { Id = id }));
}

public record PurchaseRequisitionViewModel
{
    [Required]
	public DateTime DateRequired { get;set; } = DateTime.Now.Date;
	[StringLength(500, ErrorMessage = "{0} length can't be more than {1}.")]
	public string? Purpose { get;set; }
	[StringLength(500, ErrorMessage = "{0} length can't be more than {1}.")]
	public string? Remarks { get;set; }
	[StringLength(50, ErrorMessage = "{0} length can't be more than {1}.")]
	public string? Status { get;set; }
	   
}
