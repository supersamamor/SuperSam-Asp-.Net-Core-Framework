using CTI.Common.Utility.Models;
using CTI.FAS.Application.Features.FAS.Batch.Commands;
using CTI.FAS.Application.Features.FAS.Batch.Queries;
using CTI.FAS.Core.FAS;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using CTI.Common.API.Controllers;

namespace CTI.FAS.API.Controllers.v1;

[ApiVersion("1.0")]
public class BatchController : BaseApiController<BatchController>
{
    [Authorize(Policy = Permission.Batch.View)]
    [HttpGet]
    public async Task<ActionResult<PagedListResponse<BatchState>>> GetAsync([FromQuery] GetBatchQuery query) =>
        Ok(await Mediator.Send(query));

    [Authorize(Policy = Permission.Batch.View)]
    [HttpGet("{id}")]
    public async Task<ActionResult<BatchState>> GetAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new GetBatchByIdQuery(id)));

    [Authorize(Policy = Permission.Batch.Create)]
    [HttpPost]
    public async Task<ActionResult<BatchState>> PostAsync([FromBody] BatchViewModel request) =>
        await ToActionResult(async () => await Mediator.Send(Mapper.Map<AddBatchCommand>(request)));

    [Authorize(Policy = Permission.Batch.Edit)]
    [HttpPut("{id}")]
    public async Task<ActionResult<BatchState>> PutAsync(string id, [FromBody] BatchViewModel request)
    {
        var command = Mapper.Map<EditBatchCommand>(request);
        return await ToActionResult(async () => await Mediator.Send(command with { Id = id }));
    }

    [Authorize(Policy = Permission.Batch.Delete)]
    [HttpDelete("{id}")]
    public async Task<ActionResult<BatchState>> DeleteAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new DeleteBatchCommand { Id = id }));
}

public record BatchViewModel
{
    [Required]
	public DateTime Date { get;set; } = DateTime.Now.Date;
	[Required]
	public int Batch { get;set; }
	   
}
