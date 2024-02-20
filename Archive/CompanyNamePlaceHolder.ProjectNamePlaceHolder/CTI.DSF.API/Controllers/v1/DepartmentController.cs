using CTI.Common.Utility.Models;
using CTI.DSF.Application.Features.DSF.Department.Commands;
using CTI.DSF.Application.Features.DSF.Department.Queries;
using CTI.DSF.Core.DSF;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using CTI.Common.API.Controllers;

namespace CTI.DSF.API.Controllers.v1;

[ApiVersion("1.0")]
public class DepartmentController : BaseApiController<DepartmentController>
{
    [Authorize(Policy = Permission.Department.View)]
    [HttpGet]
    public async Task<ActionResult<PagedListResponse<DepartmentState>>> GetAsync([FromQuery] GetDepartmentQuery query) =>
        Ok(await Mediator.Send(query));

    [Authorize(Policy = Permission.Department.View)]
    [HttpGet("{id}")]
    public async Task<ActionResult<DepartmentState>> GetAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new GetDepartmentByIdQuery(id)));

    [Authorize(Policy = Permission.Department.Create)]
    [HttpPost]
    public async Task<ActionResult<DepartmentState>> PostAsync([FromBody] DepartmentViewModel request) =>
        await ToActionResult(async () => await Mediator.Send(Mapper.Map<AddDepartmentCommand>(request)));

    [Authorize(Policy = Permission.Department.Edit)]
    [HttpPut("{id}")]
    public async Task<ActionResult<DepartmentState>> PutAsync(string id, [FromBody] DepartmentViewModel request)
    {
        var command = Mapper.Map<EditDepartmentCommand>(request);
        return await ToActionResult(async () => await Mediator.Send(command with { Id = id }));
    }

    [Authorize(Policy = Permission.Department.Delete)]
    [HttpDelete("{id}")]
    public async Task<ActionResult<DepartmentState>> DeleteAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new DeleteDepartmentCommand { Id = id }));
}

public record DepartmentViewModel
{
    [Required]
	[StringLength(450, ErrorMessage = "{0} length can't be more than {1}.")]
	public string CompanyCode { get;set; } = "";
	[Required]
	[StringLength(450, ErrorMessage = "{0} length can't be more than {1}.")]
	public string DepartmentCode { get;set; } = "";
	[Required]
	
	public string DepartmentName { get;set; } = "";
	public bool Active { get;set; }
	   
}
