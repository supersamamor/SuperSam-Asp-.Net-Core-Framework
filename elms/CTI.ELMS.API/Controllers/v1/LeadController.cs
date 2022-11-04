using CTI.Common.Utility.Models;
using CTI.ELMS.Application.Features.ELMS.Lead.Commands;
using CTI.ELMS.Application.Features.ELMS.Lead.Queries;
using CTI.ELMS.Core.ELMS;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using CTI.Common.API.Controllers;

namespace CTI.ELMS.API.Controllers.v1;

[ApiVersion("1.0")]
public class LeadController : BaseApiController<LeadController>
{
    [Authorize(Policy = Permission.Lead.View)]
    [HttpGet]
    public async Task<ActionResult<PagedListResponse<LeadState>>> GetAsync([FromQuery] GetLeadQuery query) =>
        Ok(await Mediator.Send(query));

    [Authorize(Policy = Permission.Lead.View)]
    [HttpGet("{id}")]
    public async Task<ActionResult<LeadState>> GetAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new GetLeadByIdQuery(id)));

    [Authorize(Policy = Permission.Lead.Create)]
    [HttpPost]
    public async Task<ActionResult<LeadState>> PostAsync([FromBody] LeadViewModel request) =>
        await ToActionResult(async () => await Mediator.Send(Mapper.Map<AddLeadCommand>(request)));

    [Authorize(Policy = Permission.Lead.Edit)]
    [HttpPut("{id}")]
    public async Task<ActionResult<LeadState>> PutAsync(string id, [FromBody] LeadViewModel request)
    {
        var command = Mapper.Map<EditLeadCommand>(request);
        return await ToActionResult(async () => await Mediator.Send(command with { Id = id }));
    }

    [Authorize(Policy = Permission.Lead.Delete)]
    [HttpDelete("{id}")]
    public async Task<ActionResult<LeadState>> DeleteAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new DeleteLeadCommand { Id = id }));
}

public record LeadViewModel
{
    [Required]
	
	public string ClientType { get;set; } = "";
	[Required]
	[StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
	public string Brand { get;set; } = "";
	[Required]
	[StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
	public string Company { get;set; } = "";
	[StringLength(100, ErrorMessage = "{0} length can't be more than {1}.")]
	public string? Street { get;set; }
	[StringLength(100, ErrorMessage = "{0} length can't be more than {1}.")]
	public string? City { get;set; }
	[StringLength(50, ErrorMessage = "{0} length can't be more than {1}.")]
	public string? Province { get;set; }
	[Required]
	
	public string Country { get;set; } = "";
	[Required]
	
	public string LeadSourceId { get;set; } = "";
	[Required]
	
	public string LeadTouchpointId { get;set; } = "";
	[Required]
	
	public string OperationTypeID { get;set; } = "";
	[Required]
	
	public string BusinessNatureID { get;set; } = "";
	[Required]
	
	public string BusinessNatureSubItemID { get;set; } = "";
	
	public string? BusinessNatureCategoryID { get;set; }
	[StringLength(20, ErrorMessage = "{0} length can't be more than {1}.")]
	public string? TINNumber { get;set; }
	public bool IsFranchise { get;set; }
	   
}
