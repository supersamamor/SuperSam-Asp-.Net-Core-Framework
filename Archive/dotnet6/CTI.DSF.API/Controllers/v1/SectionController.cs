using CTI.Common.Utility.Models;
using CTI.DSF.Application.Features.DSF.Section.Commands;
using CTI.DSF.Application.Features.DSF.Section.Queries;
using CTI.DSF.Core.DSF;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using CTI.Common.API.Controllers;

namespace CTI.DSF.API.Controllers.v1;

[ApiVersion("1.0")]
public class SectionController : BaseApiController<SectionController>
{
    [Authorize(Policy = Permission.Section.View)]
    [HttpGet]
    public async Task<ActionResult<PagedListResponse<SectionState>>> GetAsync([FromQuery] GetSectionQuery query) =>
        Ok(await Mediator.Send(query));

    [Authorize(Policy = Permission.Section.View)]
    [HttpGet("{id}")]
    public async Task<ActionResult<SectionState>> GetAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new GetSectionByIdQuery(id)));

    [Authorize(Policy = Permission.Section.Create)]
    [HttpPost]
    public async Task<ActionResult<SectionState>> PostAsync([FromBody] SectionViewModel request) =>
        await ToActionResult(async () => await Mediator.Send(Mapper.Map<AddSectionCommand>(request)));

    [Authorize(Policy = Permission.Section.Edit)]
    [HttpPut("{id}")]
    public async Task<ActionResult<SectionState>> PutAsync(string id, [FromBody] SectionViewModel request)
    {
        var command = Mapper.Map<EditSectionCommand>(request);
        return await ToActionResult(async () => await Mediator.Send(command with { Id = id }));
    }

    [Authorize(Policy = Permission.Section.Delete)]
    [HttpDelete("{id}")]
    public async Task<ActionResult<SectionState>> DeleteAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new DeleteSectionCommand { Id = id }));
}

public record SectionViewModel
{
    [Required]
	[StringLength(450, ErrorMessage = "{0} length can't be more than {1}.")]
	public string DepartmentCode { get;set; } = "";
	[Required]
	[StringLength(450, ErrorMessage = "{0} length can't be more than {1}.")]
	public string SectionCode { get;set; } = "";
	[Required]
	
	public string SectionName { get;set; } = "";
	public bool Active { get;set; }
	   
}
