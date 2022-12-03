using CTI.Common.Utility.Models;
using CTI.FAS.Application.Features.FAS.Creditor.Commands;
using CTI.FAS.Application.Features.FAS.Creditor.Queries;
using CTI.FAS.Core.FAS;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using CTI.Common.API.Controllers;

namespace CTI.FAS.API.Controllers.v1;

[ApiVersion("1.0")]
public class CreditorController : BaseApiController<CreditorController>
{
    [Authorize(Policy = Permission.Creditor.View)]
    [HttpGet]
    public async Task<ActionResult<PagedListResponse<CreditorState>>> GetAsync([FromQuery] GetCreditorQuery query) =>
        Ok(await Mediator.Send(query));

    [Authorize(Policy = Permission.Creditor.View)]
    [HttpGet("{id}")]
    public async Task<ActionResult<CreditorState>> GetAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new GetCreditorByIdQuery(id)));

    [Authorize(Policy = Permission.Creditor.Create)]
    [HttpPost]
    public async Task<ActionResult<CreditorState>> PostAsync([FromBody] CreditorViewModel request) =>
        await ToActionResult(async () => await Mediator.Send(Mapper.Map<AddCreditorCommand>(request)));

    [Authorize(Policy = Permission.Creditor.Edit)]
    [HttpPut("{id}")]
    public async Task<ActionResult<CreditorState>> PutAsync(string id, [FromBody] CreditorViewModel request)
    {
        var command = Mapper.Map<EditCreditorCommand>(request);
        return await ToActionResult(async () => await Mediator.Send(command with { Id = id }));
    }

    [Authorize(Policy = Permission.Creditor.Delete)]
    [HttpDelete("{id}")]
    public async Task<ActionResult<CreditorState>> DeleteAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new DeleteCreditorCommand { Id = id }));
}

public record CreditorViewModel
{
    [Required]
	
	public string CompanyId { get;set; } = "";
	[Required]
	[StringLength(50, ErrorMessage = "{0} length can't be more than {1}.")]
	public string CreditorAccount { get;set; } = "";
	[Required]
	[StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
	public string AccountName { get;set; } = "";
	[Required]
	[StringLength(2, ErrorMessage = "{0} length can't be more than {1}.")]
	public string AccountType { get;set; } = "";
	[Required]
	[StringLength(20, ErrorMessage = "{0} length can't be more than {1}.")]
	public string AccountNumber { get;set; } = "";
	[Required]
	[StringLength(50, ErrorMessage = "{0} length can't be more than {1}.")]
	public string PayeeAccountName { get;set; } = "";
	[Required]
	[StringLength(50, ErrorMessage = "{0} length can't be more than {1}.")]
	public string PayeeAccountNumber { get;set; } = "";
	[Required]
	[StringLength(50, ErrorMessage = "{0} length can't be more than {1}.")]
	public string Email { get;set; } = "";
	[Required]
	[StringLength(50, ErrorMessage = "{0} length can't be more than {1}.")]
	public string PayeeAccountCode { get;set; } = "";
	[Required]
	[StringLength(20, ErrorMessage = "{0} length can't be more than {1}.")]
	public string PayeeAccountTIN { get;set; } = "";
	[Required]
	[StringLength(60, ErrorMessage = "{0} length can't be more than {1}.")]
	public string PayeeAccountAddress { get;set; } = "";
	[StringLength(30, ErrorMessage = "{0} length can't be more than {1}.")]
	public string? Status { get;set; }
	   
}
