using CompanyPL.Common.Utility.Models;
using CompanyPL.EISPL.Application.Features.EISPL.PLHealthDeclaration.Commands;
using CompanyPL.EISPL.Application.Features.EISPL.PLHealthDeclaration.Queries;
using CompanyPL.EISPL.Core.EISPL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using CompanyPL.Common.API.Controllers;

namespace CompanyPL.EISPL.API.Controllers.v1;

[ApiVersion("1.0")]
public class PLHealthDeclarationController : BaseApiController<PLHealthDeclarationController>
{
    [Authorize(Policy = Permission.PLHealthDeclaration.View)]
    [HttpGet]
    public async Task<ActionResult<PagedListResponse<PLHealthDeclarationState>>> GetAsync([FromQuery] GetPLHealthDeclarationQuery query) =>
        Ok(await Mediator.Send(query));

    [Authorize(Policy = Permission.PLHealthDeclaration.View)]
    [HttpGet("{id}")]
    public async Task<ActionResult<PLHealthDeclarationState>> GetAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new GetPLHealthDeclarationByIdQuery(id)));

    [Authorize(Policy = Permission.PLHealthDeclaration.Create)]
    [HttpPost]
    public async Task<ActionResult<PLHealthDeclarationState>> PostAsync([FromBody] PLHealthDeclarationViewModel request) =>
        await ToActionResult(async () => await Mediator.Send(Mapper.Map<AddPLHealthDeclarationCommand>(request)));

    [Authorize(Policy = Permission.PLHealthDeclaration.Edit)]
    [HttpPut("{id}")]
    public async Task<ActionResult<PLHealthDeclarationState>> PutAsync(string id, [FromBody] PLHealthDeclarationViewModel request)
    {
        var command = Mapper.Map<EditPLHealthDeclarationCommand>(request);
        return await ToActionResult(async () => await Mediator.Send(command with { Id = id }));
    }

    [Authorize(Policy = Permission.PLHealthDeclaration.Delete)]
    [HttpDelete("{id}")]
    public async Task<ActionResult<PLHealthDeclarationState>> DeleteAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new DeletePLHealthDeclarationCommand { Id = id }));
}

public record PLHealthDeclarationViewModel
{
    [StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
	public string? PLVaccine { get;set; }
	[Required]
	public bool? PLIsVaccinated { get;set; }
	[Required]
	
	public string PLEmployeeId { get;set; } = "";
	   
}
