using CTI.Common.Utility.Models;
using CTI.TenantSales.Application.Features.TenantSales.Revalidate.Commands;
using CTI.TenantSales.Application.Features.TenantSales.Revalidate.Queries;
using CTI.TenantSales.Core.TenantSales;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using CTI.Common.API.Controllers;

namespace CTI.TenantSales.API.Controllers.v1;

[ApiVersion("1.0")]
public class RevalidateController : BaseApiController<RevalidateController>
{
    [Authorize(Policy = Permission.Revalidate.View)]
    [HttpGet]
    public async Task<ActionResult<PagedListResponse<RevalidateState>>> GetAsync([FromQuery] GetRevalidateQuery query) =>
        Ok(await Mediator.Send(query));

    [Authorize(Policy = Permission.Revalidate.View)]
    [HttpGet("{id}")]
    public async Task<ActionResult<RevalidateState>> GetAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new GetRevalidateByIdQuery(id)));

    [Authorize(Policy = Permission.Revalidate.Create)]
    [HttpPost]
    public async Task<ActionResult<RevalidateState>> PostAsync([FromBody] RevalidateViewModel request) =>
        await ToActionResult(async () => await Mediator.Send(Mapper.Map<AddRevalidateCommand>(request)));

    [Authorize(Policy = Permission.Revalidate.Edit)]
    [HttpPut("{id}")]
    public async Task<ActionResult<RevalidateState>> PutAsync(string id, [FromBody] RevalidateViewModel request)
    {
        var command = Mapper.Map<EditRevalidateCommand>(request);
        return await ToActionResult(async () => await Mediator.Send(command with { Id = id }));
    }

    [Authorize(Policy = Permission.Revalidate.Delete)]
    [HttpDelete("{id}")]
    public async Task<ActionResult<RevalidateState>> DeleteAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new DeleteRevalidateCommand { Id = id }));
}

public record RevalidateViewModel
{
    [Required]
	public DateTime SalesDate { get;set; } = DateTime.Now.Date;
	[Required]
	
	public string ProjectId { get;set; } = "";
	
	public string? TenantId { get;set; }
	[Required]
	[StringLength(50, ErrorMessage = "{0} length can't be more than {1}.")]
	public string Status { get;set; } = "";
	
	public string? ProcessingRemarks { get;set; }
	   
}
