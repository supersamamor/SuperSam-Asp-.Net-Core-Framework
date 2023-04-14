using CelerSoft.Common.Utility.Models;
using CelerSoft.TurboERP.Application.Features.TurboERP.WebContent.Commands;
using CelerSoft.TurboERP.Application.Features.TurboERP.WebContent.Queries;
using CelerSoft.TurboERP.Core.TurboERP;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using CelerSoft.Common.API.Controllers;

namespace CelerSoft.TurboERP.API.Controllers.v1;

[ApiVersion("1.0")]
public class WebContentController : BaseApiController<WebContentController>
{
    [Authorize(Policy = Permission.WebContent.View)]
    [HttpGet]
    public async Task<ActionResult<PagedListResponse<WebContentState>>> GetAsync([FromQuery] GetWebContentQuery query) =>
        Ok(await Mediator.Send(query));

    [Authorize(Policy = Permission.WebContent.View)]
    [HttpGet("{id}")]
    public async Task<ActionResult<WebContentState>> GetAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new GetWebContentByIdQuery(id)));

    [Authorize(Policy = Permission.WebContent.Create)]
    [HttpPost]
    public async Task<ActionResult<WebContentState>> PostAsync([FromBody] WebContentViewModel request) =>
        await ToActionResult(async () => await Mediator.Send(Mapper.Map<AddWebContentCommand>(request)));

    [Authorize(Policy = Permission.WebContent.Edit)]
    [HttpPut("{id}")]
    public async Task<ActionResult<WebContentState>> PutAsync(string id, [FromBody] WebContentViewModel request)
    {
        var command = Mapper.Map<EditWebContentCommand>(request);
        return await ToActionResult(async () => await Mediator.Send(command with { Id = id }));
    }

    [Authorize(Policy = Permission.WebContent.Delete)]
    [HttpDelete("{id}")]
    public async Task<ActionResult<WebContentState>> DeleteAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new DeleteWebContentCommand { Id = id }));
}

public record WebContentViewModel
{
    [StringLength(20, ErrorMessage = "{0} length can't be more than {1}.")]
	public string? Code { get;set; }
	[Required]
	
	public string Content { get;set; } = "";
	[StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
	public string? PageName { get;set; }
	[StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
	public string? PageTitle { get;set; }
	   
}
