using CTI.Common.Utility.Models;
using CTI.ELMS.Application.Features.ELMS.BusinessNatureCategory.Commands;
using CTI.ELMS.Application.Features.ELMS.BusinessNatureCategory.Queries;
using CTI.ELMS.Core.ELMS;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using CTI.Common.API.Controllers;

namespace CTI.ELMS.API.Controllers.v1;

[ApiVersion("1.0")]
public class BusinessNatureCategoryController : BaseApiController<BusinessNatureCategoryController>
{
    [Authorize(Policy = Permission.BusinessNatureCategory.View)]
    [HttpGet]
    public async Task<ActionResult<PagedListResponse<BusinessNatureCategoryState>>> GetAsync([FromQuery] GetBusinessNatureCategoryQuery query) =>
        Ok(await Mediator.Send(query));

    [Authorize(Policy = Permission.BusinessNatureCategory.View)]
    [HttpGet("{id}")]
    public async Task<ActionResult<BusinessNatureCategoryState>> GetAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new GetBusinessNatureCategoryByIdQuery(id)));

    [Authorize(Policy = Permission.BusinessNatureCategory.Create)]
    [HttpPost]
    public async Task<ActionResult<BusinessNatureCategoryState>> PostAsync([FromBody] BusinessNatureCategoryViewModel request) =>
        await ToActionResult(async () => await Mediator.Send(Mapper.Map<AddBusinessNatureCategoryCommand>(request)));

    [Authorize(Policy = Permission.BusinessNatureCategory.Edit)]
    [HttpPut("{id}")]
    public async Task<ActionResult<BusinessNatureCategoryState>> PutAsync(string id, [FromBody] BusinessNatureCategoryViewModel request)
    {
        var command = Mapper.Map<EditBusinessNatureCategoryCommand>(request);
        return await ToActionResult(async () => await Mediator.Send(command with { Id = id }));
    }

    [Authorize(Policy = Permission.BusinessNatureCategory.Delete)]
    [HttpDelete("{id}")]
    public async Task<ActionResult<BusinessNatureCategoryState>> DeleteAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new DeleteBusinessNatureCategoryCommand { Id = id }));
}

public record BusinessNatureCategoryViewModel
{
    [Required]
	
	public string BusinessNatureCategoryCode { get;set; } = "";
	[Required]
	
	public string BusinessNatureCategoryName { get;set; } = "";
	[Required]
	
	public string BusinessNatureSubItemID { get;set; } = "";
	public bool IsDisabled { get;set; }
	   
}
