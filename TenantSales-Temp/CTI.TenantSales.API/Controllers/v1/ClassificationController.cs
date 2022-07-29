using CTI.Common.Utility.Models;
using CTI.TenantSales.Application.Features.TenantSales.Classification.Commands;
using CTI.TenantSales.Application.Features.TenantSales.Classification.Queries;
using CTI.TenantSales.Core.TenantSales;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using CTI.Common.API.Controllers;

namespace CTI.TenantSales.API.Controllers.v1;

[ApiVersion("1.0")]
public class ClassificationController : BaseApiController<ClassificationController>
{
    [Authorize(Policy = Permission.Classification.View)]
    [HttpGet]
    public async Task<ActionResult<PagedListResponse<ClassificationState>>> GetAsync([FromQuery] GetClassificationQuery query) =>
        Ok(await Mediator.Send(query));

    [Authorize(Policy = Permission.Classification.View)]
    [HttpGet("{id}")]
    public async Task<ActionResult<ClassificationState>> GetAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new GetClassificationByIdQuery(id)));

    [Authorize(Policy = Permission.Classification.Create)]
    [HttpPost]
    public async Task<ActionResult<ClassificationState>> PostAsync([FromBody] ClassificationViewModel request) =>
        await ToActionResult(async () => await Mediator.Send(Mapper.Map<AddClassificationCommand>(request)));

    [Authorize(Policy = Permission.Classification.Edit)]
    [HttpPut("{id}")]
    public async Task<ActionResult<ClassificationState>> PutAsync(string id, [FromBody] ClassificationViewModel request)
    {
        var command = Mapper.Map<EditClassificationCommand>(request);
        return await ToActionResult(async () => await Mediator.Send(command with { Id = id }));
    }

    [Authorize(Policy = Permission.Classification.Delete)]
    [HttpDelete("{id}")]
    public async Task<ActionResult<ClassificationState>> DeleteAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new DeleteClassificationCommand { Id = id }));
}

public record ClassificationViewModel
{
    [Required]
	
	public string ThemeId { get;set; } = "";
	[Required]
	[StringLength(80, ErrorMessage = "{0} length can't be more than {1}.")]
	public string Name { get;set; } = "";
	[Required]
	[StringLength(15, ErrorMessage = "{0} length can't be more than {1}.")]
	public string Code { get;set; } = "";
	   
}
