using CompanyPL.Common.Utility.Models;
using CompanyPL.EISPL.Application.Features.EISPL.Test.Commands;
using CompanyPL.EISPL.Application.Features.EISPL.Test.Queries;
using CompanyPL.EISPL.Core.EISPL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using CompanyPL.Common.API.Controllers;

namespace CompanyPL.EISPL.API.Controllers.v1;

[ApiVersion("1.0")]
public class TestController : BaseApiController<TestController>
{
    [Authorize(Policy = Permission.Test.View)]
    [HttpGet]
    public async Task<ActionResult<PagedListResponse<TestState>>> GetAsync([FromQuery] GetTestQuery query) =>
        Ok(await Mediator.Send(query));

    [Authorize(Policy = Permission.Test.View)]
    [HttpGet("{id}")]
    public async Task<ActionResult<TestState>> GetAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new GetTestByIdQuery(id)));

    [Authorize(Policy = Permission.Test.Create)]
    [HttpPost]
    public async Task<ActionResult<TestState>> PostAsync([FromBody] TestViewModel request) =>
        await ToActionResult(async () => await Mediator.Send(Mapper.Map<AddTestCommand>(request)));

    [Authorize(Policy = Permission.Test.Edit)]
    [HttpPut("{id}")]
    public async Task<ActionResult<TestState>> PutAsync(string id, [FromBody] TestViewModel request)
    {
        var command = Mapper.Map<EditTestCommand>(request);
        return await ToActionResult(async () => await Mediator.Send(command with { Id = id }));
    }

    [Authorize(Policy = Permission.Test.Delete)]
    [HttpDelete("{id}")]
    public async Task<ActionResult<TestState>> DeleteAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new DeleteTestCommand { Id = id }));
}

public record TestViewModel
{
    [Required]
	
	public string PLEmployeeId { get;set; } = "";
	[Required]
	
	public string TestColumn { get;set; } = "";
	   
}
