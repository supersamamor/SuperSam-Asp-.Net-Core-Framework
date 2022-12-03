using CTI.Common.Utility.Models;
using CTI.FAS.Application.Features.FAS.UserEntity.Commands;
using CTI.FAS.Application.Features.FAS.UserEntity.Queries;
using CTI.FAS.Core.FAS;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using CTI.Common.API.Controllers;

namespace CTI.FAS.API.Controllers.v1;

[ApiVersion("1.0")]
public class UserEntityController : BaseApiController<UserEntityController>
{
    [Authorize(Policy = Permission.UserEntity.View)]
    [HttpGet]
    public async Task<ActionResult<PagedListResponse<UserEntityState>>> GetAsync([FromQuery] GetUserEntityQuery query) =>
        Ok(await Mediator.Send(query));

    [Authorize(Policy = Permission.UserEntity.View)]
    [HttpGet("{id}")]
    public async Task<ActionResult<UserEntityState>> GetAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new GetUserEntityByIdQuery(id)));

    [Authorize(Policy = Permission.UserEntity.Create)]
    [HttpPost]
    public async Task<ActionResult<UserEntityState>> PostAsync([FromBody] UserEntityViewModel request) =>
        await ToActionResult(async () => await Mediator.Send(Mapper.Map<AddUserEntityCommand>(request)));

    [Authorize(Policy = Permission.UserEntity.Edit)]
    [HttpPut("{id}")]
    public async Task<ActionResult<UserEntityState>> PutAsync(string id, [FromBody] UserEntityViewModel request)
    {
        var command = Mapper.Map<EditUserEntityCommand>(request);
        return await ToActionResult(async () => await Mediator.Send(command with { Id = id }));
    }

    [Authorize(Policy = Permission.UserEntity.Delete)]
    [HttpDelete("{id}")]
    public async Task<ActionResult<UserEntityState>> DeleteAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new DeleteUserEntityCommand { Id = id }));
}

public record UserEntityViewModel
{
    [Required]
	[StringLength(50, ErrorMessage = "{0} length can't be more than {1}.")]
	public string PplusUserId { get;set; } = "";
	[Required]
	
	public string CompanyId { get;set; } = "";
	   
}
