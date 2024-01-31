using CompanyPL.Common.Utility.Models;
using CompanyPL.ProjectPL.Application.Features.ProjectPL.Employee.Commands;
using CompanyPL.ProjectPL.Application.Features.ProjectPL.Employee.Queries;
using CompanyPL.ProjectPL.Core.ProjectPL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using CompanyPL.Common.API.Controllers;

namespace CompanyPL.ProjectPL.API.Controllers.v1;

[ApiVersion("1.0")]
public class EmployeeController : BaseApiController<EmployeeController>
{
    [Authorize(Policy = Permission.Employee.View)]
    [HttpGet]
    public async Task<ActionResult<PagedListResponse<EmployeeState>>> GetAsync([FromQuery] GetEmployeeQuery query) =>
        Ok(await Mediator.Send(query));

    [Authorize(Policy = Permission.Employee.View)]
    [HttpGet("{id}")]
    public async Task<ActionResult<EmployeeState>> GetAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new GetEmployeeByIdQuery(id)));

    [Authorize(Policy = Permission.Employee.Create)]
    [HttpPost]
    public async Task<ActionResult<EmployeeState>> PostAsync([FromBody] EmployeeViewModel request) =>
        await ToActionResult(async () => await Mediator.Send(Mapper.Map<AddEmployeeCommand>(request)));

    [Authorize(Policy = Permission.Employee.Edit)]
    [HttpPut("{id}")]
    public async Task<ActionResult<EmployeeState>> PutAsync(string id, [FromBody] EmployeeViewModel request)
    {
        var command = Mapper.Map<EditEmployeeCommand>(request);
        return await ToActionResult(async () => await Mediator.Send(command with { Id = id }));
    }

    [Authorize(Policy = Permission.Employee.Delete)]
    [HttpDelete("{id}")]
    public async Task<ActionResult<EmployeeState>> DeleteAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new DeleteEmployeeCommand { Id = id }));
}

public record EmployeeViewModel
{
    [Required]
	[StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
	public string FirstName { get;set; } = "";
	[Required]
	[StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
	public string EmployeeCode { get;set; } = "";
	[Required]
	[StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
	public string LastName { get;set; } = "";
	[Required]
	[StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
	public string MiddleName { get;set; } = "";
	   
}
