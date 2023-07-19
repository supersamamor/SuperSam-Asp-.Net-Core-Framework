using CompanyPL.Common.Utility.Models;
using CompanyPL.EISPL.Application.Features.EISPL.PLEmployee.Commands;
using CompanyPL.EISPL.Application.Features.EISPL.PLEmployee.Queries;
using CompanyPL.EISPL.Core.EISPL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using CompanyPL.Common.API.Controllers;

namespace CompanyPL.EISPL.API.Controllers.v1;

[ApiVersion("1.0")]
public class PLEmployeeController : BaseApiController<PLEmployeeController>
{
    [Authorize(Policy = Permission.PLEmployee.View)]
    [HttpGet]
    public async Task<ActionResult<PagedListResponse<PLEmployeeState>>> GetAsync([FromQuery] GetPLEmployeeQuery query) =>
        Ok(await Mediator.Send(query));

    [Authorize(Policy = Permission.PLEmployee.View)]
    [HttpGet("{id}")]
    public async Task<ActionResult<PLEmployeeState>> GetAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new GetPLEmployeeByIdQuery(id)));

    [Authorize(Policy = Permission.PLEmployee.Create)]
    [HttpPost]
    public async Task<ActionResult<PLEmployeeState>> PostAsync([FromBody] PLEmployeeViewModel request) =>
        await ToActionResult(async () => await Mediator.Send(Mapper.Map<AddPLEmployeeCommand>(request)));

    [Authorize(Policy = Permission.PLEmployee.Edit)]
    [HttpPut("{id}")]
    public async Task<ActionResult<PLEmployeeState>> PutAsync(string id, [FromBody] PLEmployeeViewModel request)
    {
        var command = Mapper.Map<EditPLEmployeeCommand>(request);
        return await ToActionResult(async () => await Mediator.Send(command with { Id = id }));
    }

    [Authorize(Policy = Permission.PLEmployee.Delete)]
    [HttpDelete("{id}")]
    public async Task<ActionResult<PLEmployeeState>> DeleteAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new DeletePLEmployeeCommand { Id = id }));
}

public record PLEmployeeViewModel
{
    [Required]
	[StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
	public string PLFirstName { get;set; } = "";
	[Required]
	[StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
	public string PLMiddleName { get;set; } = "";
	[Required]
	[StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
	public string PLEmployeeCode { get;set; } = "";
	[Required]
	[StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
	public string PLLastName { get;set; } = "";
	   
}
