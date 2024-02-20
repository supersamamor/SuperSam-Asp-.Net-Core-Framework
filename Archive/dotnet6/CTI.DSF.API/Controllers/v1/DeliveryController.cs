using CTI.Common.Utility.Models;
using CTI.DSF.Application.Features.DSF.Delivery.Commands;
using CTI.DSF.Application.Features.DSF.Delivery.Queries;
using CTI.DSF.Core.DSF;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using CTI.Common.API.Controllers;

namespace CTI.DSF.API.Controllers.v1;

[ApiVersion("1.0")]
public class DeliveryController : BaseApiController<DeliveryController>
{
    [Authorize(Policy = Permission.Delivery.View)]
    [HttpGet]
    public async Task<ActionResult<PagedListResponse<DeliveryState>>> GetAsync([FromQuery] GetDeliveryQuery query) =>
        Ok(await Mediator.Send(query));

    [Authorize(Policy = Permission.Delivery.View)]
    [HttpGet("{id}")]
    public async Task<ActionResult<DeliveryState>> GetAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new GetDeliveryByIdQuery(id)));

    [Authorize(Policy = Permission.Delivery.Create)]
    [HttpPost]
    public async Task<ActionResult<DeliveryState>> PostAsync([FromBody] DeliveryViewModel request) =>
        await ToActionResult(async () => await Mediator.Send(Mapper.Map<AddDeliveryCommand>(request)));

    [Authorize(Policy = Permission.Delivery.Edit)]
    [HttpPut("{id}")]
    public async Task<ActionResult<DeliveryState>> PutAsync(string id, [FromBody] DeliveryViewModel request)
    {
        var command = Mapper.Map<EditDeliveryCommand>(request);
        return await ToActionResult(async () => await Mediator.Send(command with { Id = id }));
    }

    [Authorize(Policy = Permission.Delivery.Delete)]
    [HttpDelete("{id}")]
    public async Task<ActionResult<DeliveryState>> DeleteAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new DeleteDeliveryCommand { Id = id }));
}

public record DeliveryViewModel
{
    [Required]
	[StringLength(450, ErrorMessage = "{0} length can't be more than {1}.")]
	public string TaskCompanyAssignmentId { get;set; } = "";
	[Required]
	[StringLength(450, ErrorMessage = "{0} length can't be more than {1}.")]
	public string DeliveryCode { get;set; } = "";
	[Required]
	[StringLength(450, ErrorMessage = "{0} length can't be more than {1}.")]
	public string AssignmentId { get;set; } = "";
	[Required]
	public DateTime DueDate { get;set; } = DateTime.Now.Date;
	[Required]
	[StringLength(50, ErrorMessage = "{0} length can't be more than {1}.")]
	public string Status { get;set; } = "";
	[Required]
	public string? DeliveryAttachment { get;set; } = "";
	[Required]
	
	public string Remarks { get;set; } = "";
	[Required]
	
	public string HolidayTag { get;set; } = "";
	public DateTime? SubmittedDate { get;set; } = DateTime.Now.Date;
	[StringLength(450, ErrorMessage = "{0} length can't be more than {1}.")]
	public string? SubmittedBy { get;set; }
	public DateTime? ReviewedDate { get;set; } = DateTime.Now.Date;
	[StringLength(450, ErrorMessage = "{0} length can't be more than {1}.")]
	public string? ReviewedBy { get;set; }
	public DateTime? ApprovedDate { get;set; } = DateTime.Now.Date;
	[StringLength(450, ErrorMessage = "{0} length can't be more than {1}.")]
	public string? ApprovedBy { get;set; }
	public DateTime? RejectedDate { get;set; } = DateTime.Now.Date;
	[StringLength(450, ErrorMessage = "{0} length can't be more than {1}.")]
	public string? RejectedBy { get;set; }
	public DateTime? CancelledDate { get;set; } = DateTime.Now.Date;
	[StringLength(450, ErrorMessage = "{0} length can't be more than {1}.")]
	public string? CancelledBy { get;set; }
	   
}
