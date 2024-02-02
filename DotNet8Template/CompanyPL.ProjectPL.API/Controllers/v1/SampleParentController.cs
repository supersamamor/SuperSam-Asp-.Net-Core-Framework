using CompanyPL.Common.Utility.Models;
using CompanyPL.ProjectPL.Application.Features.ProjectPL.SampleParent.Commands;
using CompanyPL.ProjectPL.Application.Features.ProjectPL.SampleParent.Queries;
using CompanyPL.ProjectPL.Core.ProjectPL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using CompanyPL.Common.API.Controllers;

namespace CompanyPL.ProjectPL.API.Controllers.v1;

[ApiVersion("1.0")]
public class SampleParentController : BaseApiController<SampleParentController>
{
    [Authorize(Policy = Permission.SampleParent.View)]
    [HttpGet]
    public async Task<ActionResult<PagedListResponse<SampleParentState>>> GetAsync([FromQuery] GetSampleParentQuery query) =>
        Ok(await Mediator.Send(query));

    [Authorize(Policy = Permission.SampleParent.View)]
    [HttpGet("{id}")]
    public async Task<ActionResult<SampleParentState>> GetAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new GetSampleParentByIdQuery(id)));

    [Authorize(Policy = Permission.SampleParent.Create)]
    [HttpPost]
    public async Task<ActionResult<SampleParentState>> PostAsync([FromBody] SampleParentViewModel request) =>
        await ToActionResult(async () => await Mediator.Send(Mapper.Map<AddSampleParentCommand>(request)));

    [Authorize(Policy = Permission.SampleParent.Edit)]
    [HttpPut("{id}")]
    public async Task<ActionResult<SampleParentState>> PutAsync(string id, [FromBody] SampleParentViewModel request)
    {
        var command = Mapper.Map<EditSampleParentCommand>(request);
        return await ToActionResult(async () => await Mediator.Send(command with { Id = id }));
    }

    [Authorize(Policy = Permission.SampleParent.Delete)]
    [HttpDelete("{id}")]
    public async Task<ActionResult<SampleParentState>> DeleteAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new DeleteSampleParentCommand { Id = id }));
}

public record SampleParentViewModel
{
    [Required]
	[StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
	public string Name { get;set; } = "";
	   
}
