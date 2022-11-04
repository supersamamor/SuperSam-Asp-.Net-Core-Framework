using CTI.Common.Utility.Models;
using CTI.ELMS.Application.Features.ELMS.PPlusConnectionSetup.Commands;
using CTI.ELMS.Application.Features.ELMS.PPlusConnectionSetup.Queries;
using CTI.ELMS.Core.ELMS;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using CTI.Common.API.Controllers;

namespace CTI.ELMS.API.Controllers.v1;

[ApiVersion("1.0")]
public class PPlusConnectionSetupController : BaseApiController<PPlusConnectionSetupController>
{
    [Authorize(Policy = Permission.PPlusConnectionSetup.View)]
    [HttpGet]
    public async Task<ActionResult<PagedListResponse<PPlusConnectionSetupState>>> GetAsync([FromQuery] GetPPlusConnectionSetupQuery query) =>
        Ok(await Mediator.Send(query));

    [Authorize(Policy = Permission.PPlusConnectionSetup.View)]
    [HttpGet("{id}")]
    public async Task<ActionResult<PPlusConnectionSetupState>> GetAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new GetPPlusConnectionSetupByIdQuery(id)));

    [Authorize(Policy = Permission.PPlusConnectionSetup.Create)]
    [HttpPost]
    public async Task<ActionResult<PPlusConnectionSetupState>> PostAsync([FromBody] PPlusConnectionSetupViewModel request) =>
        await ToActionResult(async () => await Mediator.Send(Mapper.Map<AddPPlusConnectionSetupCommand>(request)));

    [Authorize(Policy = Permission.PPlusConnectionSetup.Edit)]
    [HttpPut("{id}")]
    public async Task<ActionResult<PPlusConnectionSetupState>> PutAsync(string id, [FromBody] PPlusConnectionSetupViewModel request)
    {
        var command = Mapper.Map<EditPPlusConnectionSetupCommand>(request);
        return await ToActionResult(async () => await Mediator.Send(command with { Id = id }));
    }

    [Authorize(Policy = Permission.PPlusConnectionSetup.Delete)]
    [HttpDelete("{id}")]
    public async Task<ActionResult<PPlusConnectionSetupState>> DeleteAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new DeletePPlusConnectionSetupCommand { Id = id }));
}

public record PPlusConnectionSetupViewModel
{
    [Required]
	[StringLength(100, ErrorMessage = "{0} length can't be more than {1}.")]
	public string PPlusVersionName { get;set; } = "";
	[Required]
	[StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
	public string TablePrefix { get;set; } = "";
	[Required]
	[StringLength(1000, ErrorMessage = "{0} length can't be more than {1}.")]
	public string ConnectionString { get;set; } = "";
	[StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
	public string? ExhibitThemeCodes { get;set; }
	   
}
