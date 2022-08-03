using CTI.Common.Utility.Models;
using CTI.TenantSales.Application.Features.TenantSales.TenantPOSSales.Commands;
using CTI.TenantSales.Application.Features.TenantSales.TenantPOSSales.Queries;
using CTI.TenantSales.Core.TenantSales;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using CTI.Common.API.Controllers;

namespace CTI.TenantSales.API.Controllers.v1;

[ApiVersion("1.0")]
public class TenantPOSSalesController : BaseApiController<TenantPOSSalesController>
{
    [Authorize(Policy = Permission.TenantPOSSales.View)]
    [HttpGet]
    public async Task<ActionResult<PagedListResponse<TenantPOSSalesState>>> GetAsync([FromQuery] GetTenantPOSSalesQuery query) =>
        Ok(await Mediator.Send(query));

    [Authorize(Policy = Permission.TenantPOSSales.View)]
    [HttpGet("{id}")]
    public async Task<ActionResult<TenantPOSSalesState>> GetAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new GetTenantPOSSalesByIdQuery(id)));

    [Authorize(Policy = Permission.TenantPOSSales.Create)]
    [HttpPost]
    public async Task<ActionResult<TenantPOSSalesState>> PostAsync([FromBody] TenantPOSSalesViewModel request) =>
        await ToActionResult(async () => await Mediator.Send(Mapper.Map<AddTenantPOSSalesCommand>(request)));

    [Authorize(Policy = Permission.TenantPOSSales.Edit)]
    [HttpPut("{id}")]
    public async Task<ActionResult<TenantPOSSalesState>> PutAsync(string id, [FromBody] TenantPOSSalesViewModel request)
    {
        var command = Mapper.Map<EditTenantPOSSalesCommand>(request);
        return await ToActionResult(async () => await Mediator.Send(command with { Id = id }));
    }

    [Authorize(Policy = Permission.TenantPOSSales.Delete)]
    [HttpDelete("{id}")]
    public async Task<ActionResult<TenantPOSSalesState>> DeleteAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new DeleteTenantPOSSalesCommand { Id = id }));
}

public record TenantPOSSalesViewModel
{
    [Required]
	public int SalesType { get;set; }
	[Required]
	public int HourCode { get;set; }
	[StringLength(20, ErrorMessage = "{0} length can't be more than {1}.")]
	public string? SalesCategory { get;set; }
	[Required]
	public DateTime SalesDate { get;set; } = DateTime.Now.Date;
	public bool IsAutoCompute { get;set; }
	[Required]
	
	[DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
	public decimal SalesAmount { get;set; }
	[Required]
	
	[DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
	public decimal OldAccumulatedTotal { get;set; }
	[Required]
	
	[DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
	public decimal NewAccumulatedTotal { get;set; }
	[Required]
	
	[DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
	public decimal TaxableSalesAmount { get;set; }
	[Required]
	
	[DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
	public decimal NonTaxableSalesAmount { get;set; }
	[Required]
	
	[DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
	public decimal SeniorCitizenDiscount { get;set; }
	[Required]
	
	[DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
	public decimal PromoDiscount { get;set; }
	[Required]
	
	[DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
	public decimal OtherDiscount { get;set; }
	[Required]
	
	[DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
	public decimal RefundDiscount { get;set; }
	[Required]
	
	[DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
	public decimal VoidAmount { get;set; }
	[Required]
	
	[DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
	public decimal AdjustmentAmount { get;set; }
	[Required]
	
	[DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
	public decimal TotalServiceCharge { get;set; }
	[Required]
	
	[DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
	public decimal TotalTax { get;set; }
	[Required]
	
	[DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
	public decimal NoOfSalesTransactions { get;set; }
	[Required]
	
	[DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
	public decimal NoOfTransactions { get;set; }
	[Required]
	
	[DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
	public decimal TotalNetSales { get;set; }
	[Required]
	public int ControlNumber { get;set; }
	[StringLength(100, ErrorMessage = "{0} length can't be more than {1}.")]
	public string? FileName { get;set; }
	[Required]
	
	public string TenantPOSId { get;set; } = "";
	[Required]
	public int ValidationStatus { get;set; }
	[StringLength(1000, ErrorMessage = "{0} length can't be more than {1}.")]
	public string? ValidationRemarks { get;set; }
	[Required]
	
	[DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
	public decimal AutocalculatedNewAccumulatedTotal { get;set; }
	[Required]
	
	[DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
	public decimal AutocalculatedOldAccumulatedTotal { get;set; }
	   
}
