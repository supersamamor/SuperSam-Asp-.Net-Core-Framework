using CTI.Common.Utility.Models;
using CTI.ELMS.Application.Features.ELMS.IFCAARLedger.Commands;
using CTI.ELMS.Application.Features.ELMS.IFCAARLedger.Queries;
using CTI.ELMS.Core.ELMS;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using CTI.Common.API.Controllers;

namespace CTI.ELMS.API.Controllers.v1;

[ApiVersion("1.0")]
public class IFCAARLedgerController : BaseApiController<IFCAARLedgerController>
{
    [Authorize(Policy = Permission.IFCAARLedger.View)]
    [HttpGet]
    public async Task<ActionResult<PagedListResponse<IFCAARLedgerState>>> GetAsync([FromQuery] GetIFCAARLedgerQuery query) =>
        Ok(await Mediator.Send(query));

    [Authorize(Policy = Permission.IFCAARLedger.View)]
    [HttpGet("{id}")]
    public async Task<ActionResult<IFCAARLedgerState>> GetAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new GetIFCAARLedgerByIdQuery(id)));

    [Authorize(Policy = Permission.IFCAARLedger.Create)]
    [HttpPost]
    public async Task<ActionResult<IFCAARLedgerState>> PostAsync([FromBody] IFCAARLedgerViewModel request) =>
        await ToActionResult(async () => await Mediator.Send(Mapper.Map<AddIFCAARLedgerCommand>(request)));

    [Authorize(Policy = Permission.IFCAARLedger.Edit)]
    [HttpPut("{id}")]
    public async Task<ActionResult<IFCAARLedgerState>> PutAsync(string id, [FromBody] IFCAARLedgerViewModel request)
    {
        var command = Mapper.Map<EditIFCAARLedgerCommand>(request);
        return await ToActionResult(async () => await Mediator.Send(command with { Id = id }));
    }

    [Authorize(Policy = Permission.IFCAARLedger.Delete)]
    [HttpDelete("{id}")]
    public async Task<ActionResult<IFCAARLedgerState>> DeleteAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new DeleteIFCAARLedgerCommand { Id = id }));
}

public record IFCAARLedgerViewModel
{
    [Required]
	[StringLength(20, ErrorMessage = "{0} length can't be more than {1}.")]
	public string TenantContractNo { get;set; } = "";
	[Required]
	[StringLength(20, ErrorMessage = "{0} length can't be more than {1}.")]
	public string DocumentNo { get;set; } = "";
	public DateTime? DocumentDate { get;set; } = DateTime.Now.Date;
	[Required]
	[StringLength(1000, ErrorMessage = "{0} length can't be more than {1}.")]
	public string DocumentDescription { get;set; } = "";
	[Required]
	[StringLength(10, ErrorMessage = "{0} length can't be more than {1}.")]
	public string Mode { get;set; } = "";
	[Required]
	[StringLength(1000, ErrorMessage = "{0} length can't be more than {1}.")]
	public string LedgerDescription { get;set; } = "";
	
	[DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
	public decimal? TransactionWithHoldingTaxAmount { get;set; }
	[Required]
	[StringLength(20, ErrorMessage = "{0} length can't be more than {1}.")]
	public string TransactionType { get;set; } = "";
	
	[DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
	public decimal? TransactionAmount { get;set; }
	[Required]
	[StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
	public string LotNo { get;set; } = "";
	public int? LineNo { get;set; }
	[Required]
	[StringLength(20, ErrorMessage = "{0} length can't be more than {1}.")]
	public string TaxScheme { get;set; } = "";
	
	[DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
	public decimal? TransactionTaxBaseAmount { get;set; }
	
	[DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
	public decimal? TransactionTaxAmount { get;set; }
	public int? SequenceNo { get;set; }
	[Required]
	[StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
	public string ReferenceNo { get;set; } = "";
	[Required]
	[StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
	public string TransactionClass { get;set; } = "";
	[Required]
	[StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
	public string GLAccount { get;set; } = "";
	
	public string? ProjectID { get;set; }
	[Required]
	[StringLength(500, ErrorMessage = "{0} length can't be more than {1}.")]
	public string TradeName { get;set; } = "";
	[Required]
	[StringLength(500, ErrorMessage = "{0} length can't be more than {1}.")]
	public string TransactionDesc { get;set; } = "";
	   
}
