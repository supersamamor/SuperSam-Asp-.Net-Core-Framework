using CompanyNamePlaceHolder.Common.Utility.Models;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Features.AreaPlaceHolder.Delivery.Commands;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Features.AreaPlaceHolder.Delivery.Queries;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Core.AreaPlaceHolder;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using CompanyNamePlaceHolder.Common.API.Controllers;

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.API.Controllers.v1;

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
    
	public string? ApproverRemarks { get;set; }
	
	public string? Status { get;set; }
	[StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
	public string? EndorserRemarks { get;set; }
	public DateTime? EndorsedDate { get;set; } = DateTime.Now.Date;
	
	public string? ApprovedTag { get;set; }
	
	public string? EndorsedTag { get;set; }
	[Required]
	
	public string DeliveryCode { get;set; } = "";
	[Required]
	public DateTime ActualDeliveryDate { get;set; } = DateTime.Now.Date;
	[Required]
	public string? DeliveryAttachment { get;set; } = "";
	[StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
	public string? ActualDeliveryRemarks { get;set; }
	[Required]
	
	public string AssignmentCode { get;set; } = "";
	public DateTime? DueDate { get;set; } = DateTime.Now.Date;
	   
}
