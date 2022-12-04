using CTI.Common.Utility.Models;
using CTI.FAS.Application.Features.FAS.PaymentTransaction.Commands;
using CTI.FAS.Application.Features.FAS.PaymentTransaction.Queries;
using CTI.FAS.Core.FAS;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using CTI.Common.API.Controllers;

namespace CTI.FAS.API.Controllers.v1;

[ApiVersion("1.0")]
public class PaymentTransactionController : BaseApiController<PaymentTransactionController>
{
    [Authorize(Policy = Permission.PaymentTransaction.View)]
    [HttpGet]
    public async Task<ActionResult<PagedListResponse<PaymentTransactionState>>> GetAsync([FromQuery] GetPaymentTransactionQuery query) =>
        Ok(await Mediator.Send(query));

    [Authorize(Policy = Permission.PaymentTransaction.View)]
    [HttpGet("{id}")]
    public async Task<ActionResult<PaymentTransactionState>> GetAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new GetPaymentTransactionByIdQuery(id)));

    [Authorize(Policy = Permission.PaymentTransaction.Create)]
    [HttpPost]
    public async Task<ActionResult<PaymentTransactionState>> PostAsync([FromBody] PaymentTransactionViewModel request) =>
        await ToActionResult(async () => await Mediator.Send(Mapper.Map<AddPaymentTransactionCommand>(request)));

    [Authorize(Policy = Permission.PaymentTransaction.Edit)]
    [HttpPut("{id}")]
    public async Task<ActionResult<PaymentTransactionState>> PutAsync(string id, [FromBody] PaymentTransactionViewModel request)
    {
        var command = Mapper.Map<EditPaymentTransactionCommand>(request);
        return await ToActionResult(async () => await Mediator.Send(command with { Id = id }));
    }

    [Authorize(Policy = Permission.PaymentTransaction.Delete)]
    [HttpDelete("{id}")]
    public async Task<ActionResult<PaymentTransactionState>> DeleteAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new DeletePaymentTransactionCommand { Id = id }));
}

public record PaymentTransactionViewModel
{
    [Required]
	[StringLength(4, ErrorMessage = "{0} length can't be more than {1}.")]
	public string EnrolledPayeeId { get;set; } = "";
	
	public string? BatchId { get;set; }
	public DateTime? TransmissionDate { get;set; } = DateTime.Now.Date;
	[Required]
	[StringLength(15, ErrorMessage = "{0} length can't be more than {1}.")]
	public string DocumentNumber { get;set; } = "";
	[Required]
	public DateTime DocumentDate { get;set; } = DateTime.Now.Date;
	[Required]
	
	[DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
	public decimal DocumentAmount { get;set; }
	[Required]
	[StringLength(15, ErrorMessage = "{0} length can't be more than {1}.")]
	public string CheckNumber { get;set; } = "";
	[Required]
	[StringLength(9, ErrorMessage = "{0} length can't be more than {1}.")]
	public string PaymentType { get;set; } = "";
	[Required]
	[StringLength(50, ErrorMessage = "{0} length can't be more than {1}.")]
	public string TextFileName { get;set; } = "";
	[Required]
	[StringLength(50, ErrorMessage = "{0} length can't be more than {1}.")]
	public string PdfReport { get;set; } = "";
	[Required]
	public bool Emailed { get;set; }
	[Required]
	[StringLength(50, ErrorMessage = "{0} length can't be more than {1}.")]
	public string GroupCode { get;set; } = "";
	[Required]
	[StringLength(30, ErrorMessage = "{0} length can't be more than {1}.")]
	public string Status { get;set; } = "";
	[Required]
	
	[DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
	public decimal IfcaBatchNumber { get;set; }
	[Required]
	
	[DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
	public decimal IfcaLineNumber { get;set; }
	[Required]
	public int EmailSentCount { get;set; }
	public DateTime? EmailSentDateTime { get;set; } = DateTime.Now.Date;
	[Required]
	public bool IsForSending { get;set; }
	   
}
