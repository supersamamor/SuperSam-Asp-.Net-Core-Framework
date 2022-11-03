using CTI.Common.Utility.Models;
using CTI.ELMS.Application.Features.ELMS.IFCATransactionType.Commands;
using CTI.ELMS.Application.Features.ELMS.IFCATransactionType.Queries;
using CTI.ELMS.Core.ELMS;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using CTI.Common.API.Controllers;

namespace CTI.ELMS.API.Controllers.v1;

[ApiVersion("1.0")]
public class IFCATransactionTypeController : BaseApiController<IFCATransactionTypeController>
{
    [Authorize(Policy = Permission.IFCATransactionType.View)]
    [HttpGet]
    public async Task<ActionResult<PagedListResponse<IFCATransactionTypeState>>> GetAsync([FromQuery] GetIFCATransactionTypeQuery query) =>
        Ok(await Mediator.Send(query));

    [Authorize(Policy = Permission.IFCATransactionType.View)]
    [HttpGet("{id}")]
    public async Task<ActionResult<IFCATransactionTypeState>> GetAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new GetIFCATransactionTypeByIdQuery(id)));

    [Authorize(Policy = Permission.IFCATransactionType.Create)]
    [HttpPost]
    public async Task<ActionResult<IFCATransactionTypeState>> PostAsync([FromBody] IFCATransactionTypeViewModel request) =>
        await ToActionResult(async () => await Mediator.Send(Mapper.Map<AddIFCATransactionTypeCommand>(request)));

    [Authorize(Policy = Permission.IFCATransactionType.Edit)]
    [HttpPut("{id}")]
    public async Task<ActionResult<IFCATransactionTypeState>> PutAsync(string id, [FromBody] IFCATransactionTypeViewModel request)
    {
        var command = Mapper.Map<EditIFCATransactionTypeCommand>(request);
        return await ToActionResult(async () => await Mediator.Send(command with { Id = id }));
    }

    [Authorize(Policy = Permission.IFCATransactionType.Delete)]
    [HttpDelete("{id}")]
    public async Task<ActionResult<IFCATransactionTypeState>> DeleteAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new DeleteIFCATransactionTypeCommand { Id = id }));
}

public record IFCATransactionTypeViewModel
{
    [Required]
	[StringLength(10, ErrorMessage = "{0} length can't be more than {1}.")]
	public string TransCode { get;set; } = "";
	[Required]
	[StringLength(20, ErrorMessage = "{0} length can't be more than {1}.")]
	public string TransGroup { get;set; } = "";
	[Required]
	[StringLength(500, ErrorMessage = "{0} length can't be more than {1}.")]
	public string Description { get;set; } = "";
	
	public string? EntityID { get;set; }
	[Required]
	[StringLength(1, ErrorMessage = "{0} length can't be more than {1}.")]
	public string Mode { get;set; } = "";
	   
}
