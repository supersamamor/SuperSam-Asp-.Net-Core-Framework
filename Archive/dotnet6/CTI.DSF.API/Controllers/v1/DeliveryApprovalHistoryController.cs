using CTI.Common.Utility.Models;
using CTI.DSF.Application.Features.DSF.DeliveryApprovalHistory.Commands;
using CTI.DSF.Application.Features.DSF.DeliveryApprovalHistory.Queries;
using CTI.DSF.Core.DSF;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using CTI.Common.API.Controllers;

namespace CTI.DSF.API.Controllers.v1;

[ApiVersion("1.0")]
public class DeliveryApprovalHistoryController : BaseApiController<DeliveryApprovalHistoryController>
{
    [Authorize(Policy = Permission.DeliveryApprovalHistory.View)]
    [HttpGet]
    public async Task<ActionResult<PagedListResponse<DeliveryApprovalHistoryState>>> GetAsync([FromQuery] GetDeliveryApprovalHistoryQuery query) =>
        Ok(await Mediator.Send(query));

    [Authorize(Policy = Permission.DeliveryApprovalHistory.View)]
    [HttpGet("{id}")]
    public async Task<ActionResult<DeliveryApprovalHistoryState>> GetAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new GetDeliveryApprovalHistoryByIdQuery(id)));

    [Authorize(Policy = Permission.DeliveryApprovalHistory.Create)]
    [HttpPost]
    public async Task<ActionResult<DeliveryApprovalHistoryState>> PostAsync([FromBody] DeliveryApprovalHistoryViewModel request) =>
        await ToActionResult(async () => await Mediator.Send(Mapper.Map<AddDeliveryApprovalHistoryCommand>(request)));

    [Authorize(Policy = Permission.DeliveryApprovalHistory.Edit)]
    [HttpPut("{id}")]
    public async Task<ActionResult<DeliveryApprovalHistoryState>> PutAsync(string id, [FromBody] DeliveryApprovalHistoryViewModel request)
    {
        var command = Mapper.Map<EditDeliveryApprovalHistoryCommand>(request);
        return await ToActionResult(async () => await Mediator.Send(command with { Id = id }));
    }

    [Authorize(Policy = Permission.DeliveryApprovalHistory.Delete)]
    [HttpDelete("{id}")]
    public async Task<ActionResult<DeliveryApprovalHistoryState>> DeleteAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new DeleteDeliveryApprovalHistoryCommand { Id = id }));
}

public record DeliveryApprovalHistoryViewModel
{
    [Required]
	[StringLength(450, ErrorMessage = "{0} length can't be more than {1}.")]
	public string DeliveryId { get;set; } = "";
	public DateTime? TransactionDateTime { get;set; } = DateTime.Now.Date;
	[StringLength(50, ErrorMessage = "{0} length can't be more than {1}.")]
	public string? Status { get;set; }
	[StringLength(450, ErrorMessage = "{0} length can't be more than {1}.")]
	public string? TransactedBy { get;set; }
	[Required]
	
	public string Remarks { get;set; } = "";
	   
}
