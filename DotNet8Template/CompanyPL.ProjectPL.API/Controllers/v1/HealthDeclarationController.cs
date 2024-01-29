using CompanyPL.Common.Utility.Models;
using CompanyPL.ProjectPL.Application.Features.ProjectPL.HealthDeclaration.Commands;
using CompanyPL.ProjectPL.Application.Features.ProjectPL.HealthDeclaration.Queries;
using CompanyPL.ProjectPL.Core.ProjectPL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using CompanyPL.Common.API.Controllers;

namespace CompanyPL.ProjectPL.API.Controllers.v1;

[ApiVersion("1.0")]
public class HealthDeclarationController : BaseApiController<HealthDeclarationController>
{
    [Authorize(Policy = Permission.HealthDeclaration.View)]
    [HttpGet]
    public async Task<ActionResult<PagedListResponse<HealthDeclarationState>>> GetAsync([FromQuery] GetHealthDeclarationQuery query) =>
        Ok(await Mediator.Send(query));

    [Authorize(Policy = Permission.HealthDeclaration.View)]
    [HttpGet("{id}")]
    public async Task<ActionResult<HealthDeclarationState>> GetAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new GetHealthDeclarationByIdQuery(id)));

    [Authorize(Policy = Permission.HealthDeclaration.Create)]
    [HttpPost]
    public async Task<ActionResult<HealthDeclarationState>> PostAsync([FromBody] HealthDeclarationViewModel request) =>
        await ToActionResult(async () => await Mediator.Send(Mapper.Map<AddHealthDeclarationCommand>(request)));

    [Authorize(Policy = Permission.HealthDeclaration.Edit)]
    [HttpPut("{id}")]
    public async Task<ActionResult<HealthDeclarationState>> PutAsync(string id, [FromBody] HealthDeclarationViewModel request)
    {
        var command = Mapper.Map<EditHealthDeclarationCommand>(request);
        return await ToActionResult(async () => await Mediator.Send(command with { Id = id }));
    }

    [Authorize(Policy = Permission.HealthDeclaration.Delete)]
    [HttpDelete("{id}")]
    public async Task<ActionResult<HealthDeclarationState>> DeleteAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new DeleteHealthDeclarationCommand { Id = id }));
}

public record HealthDeclarationViewModel
{
    [Required]
	
	public string EmployeeId { get;set; } = "";
	[Required]
	public bool? IsVaccinated { get;set; }
	[StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
	public string? Vaccine { get;set; }
	   
}
