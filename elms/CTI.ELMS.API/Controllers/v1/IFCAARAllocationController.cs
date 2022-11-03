using CTI.Common.Utility.Models;
using CTI.ELMS.Application.Features.ELMS.IFCAARAllocation.Commands;
using CTI.ELMS.Application.Features.ELMS.IFCAARAllocation.Queries;
using CTI.ELMS.Core.ELMS;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using CTI.Common.API.Controllers;

namespace CTI.ELMS.API.Controllers.v1;

[ApiVersion("1.0")]
public class IFCAARAllocationController : BaseApiController<IFCAARAllocationController>
{
    [Authorize(Policy = Permission.IFCAARAllocation.View)]
    [HttpGet]
    public async Task<ActionResult<PagedListResponse<IFCAARAllocationState>>> GetAsync([FromQuery] GetIFCAARAllocationQuery query) =>
        Ok(await Mediator.Send(query));

    [Authorize(Policy = Permission.IFCAARAllocation.View)]
    [HttpGet("{id}")]
    public async Task<ActionResult<IFCAARAllocationState>> GetAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new GetIFCAARAllocationByIdQuery(id)));

    [Authorize(Policy = Permission.IFCAARAllocation.Create)]
    [HttpPost]
    public async Task<ActionResult<IFCAARAllocationState>> PostAsync([FromBody] IFCAARAllocationViewModel request) =>
        await ToActionResult(async () => await Mediator.Send(Mapper.Map<AddIFCAARAllocationCommand>(request)));

    [Authorize(Policy = Permission.IFCAARAllocation.Edit)]
    [HttpPut("{id}")]
    public async Task<ActionResult<IFCAARAllocationState>> PutAsync(string id, [FromBody] IFCAARAllocationViewModel request)
    {
        var command = Mapper.Map<EditIFCAARAllocationCommand>(request);
        return await ToActionResult(async () => await Mediator.Send(command with { Id = id }));
    }

    [Authorize(Policy = Permission.IFCAARAllocation.Delete)]
    [HttpDelete("{id}")]
    public async Task<ActionResult<IFCAARAllocationState>> DeleteAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new DeleteIFCAARAllocationCommand { Id = id }));
}

public record IFCAARAllocationViewModel
{
    
	public string? ProjectID { get;set; }
	[Required]
	[StringLength(20, ErrorMessage = "{0} length can't be more than {1}.")]
	public string TenantContractNo { get;set; } = "";
	[Required]
	[StringLength(20, ErrorMessage = "{0} length can't be more than {1}.")]
	public string DocumentNo { get;set; } = "";
	
	[DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
	public decimal? TransactionAmount { get;set; }
	[Required]
	[StringLength(20, ErrorMessage = "{0} length can't be more than {1}.")]
	public string TransactionType { get;set; } = "";
	public DateTime? DocumentDate { get;set; } = DateTime.Now.Date;
	   
}
