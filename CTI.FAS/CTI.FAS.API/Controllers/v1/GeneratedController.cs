using CTI.Common.Utility.Models;
using CTI.FAS.Application.Features.FAS.Generated.Commands;
using CTI.FAS.Application.Features.FAS.Generated.Queries;
using CTI.FAS.Core.FAS;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using CTI.Common.API.Controllers;

namespace CTI.FAS.API.Controllers.v1;

[ApiVersion("1.0")]
public class GeneratedController : BaseApiController<GeneratedController>
{
    [Authorize(Policy = Permission.Generated.View)]
    [HttpGet]
    public async Task<ActionResult<PagedListResponse<GeneratedState>>> GetAsync([FromQuery] GetGeneratedQuery query) =>
        Ok(await Mediator.Send(query));

    [Authorize(Policy = Permission.Generated.View)]
    [HttpGet("{id}")]
    public async Task<ActionResult<GeneratedState>> GetAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new GetGeneratedByIdQuery(id)));

    [Authorize(Policy = Permission.Generated.Create)]
    [HttpPost]
    public async Task<ActionResult<GeneratedState>> PostAsync([FromBody] GeneratedViewModel request) =>
        await ToActionResult(async () => await Mediator.Send(Mapper.Map<AddGeneratedCommand>(request)));

    [Authorize(Policy = Permission.Generated.Edit)]
    [HttpPut("{id}")]
    public async Task<ActionResult<GeneratedState>> PutAsync(string id, [FromBody] GeneratedViewModel request)
    {
        var command = Mapper.Map<EditGeneratedCommand>(request);
        return await ToActionResult(async () => await Mediator.Send(command with { Id = id }));
    }

    [Authorize(Policy = Permission.Generated.Delete)]
    [HttpDelete("{id}")]
    public async Task<ActionResult<GeneratedState>> DeleteAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new DeleteGeneratedCommand { Id = id }));
}

public record GeneratedViewModel
{
    [Required]
	[StringLength(4, ErrorMessage = "{0} length can't be more than {1}.")]
	public string CompanyId { get;set; } = "";
	[Required]
	[StringLength(20, ErrorMessage = "{0} length can't be more than {1}.")]
	public string CreditorId { get;set; } = "";
	[Required]
	
	public string BatchId { get;set; } = "";
	[Required]
	[StringLength(100, ErrorMessage = "{0} length can't be more than {1}.")]
	public string Name { get;set; } = "";
	[Required]
	public DateTime TransmissionDate { get;set; } = DateTime.Now.Date;
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
	[StringLength(1, ErrorMessage = "{0} length can't be more than {1}.")]
	public string Release { get;set; } = "";
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
	[StringLength(9, ErrorMessage = "{0} length can't be more than {1}.")]
	public string GroupCode { get;set; } = "";
	[Required]
	[StringLength(9, ErrorMessage = "{0} length can't be more than {1}.")]
	public string Status { get;set; } = "";
	   
}
