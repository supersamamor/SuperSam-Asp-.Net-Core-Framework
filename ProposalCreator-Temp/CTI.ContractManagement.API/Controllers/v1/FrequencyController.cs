using CTI.Common.Utility.Models;
using CTI.ContractManagement.Application.Features.ContractManagement.Frequency.Commands;
using CTI.ContractManagement.Application.Features.ContractManagement.Frequency.Queries;
using CTI.ContractManagement.Core.ContractManagement;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using CTI.Common.API.Controllers;

namespace CTI.ContractManagement.API.Controllers.v1;

[ApiVersion("1.0")]
public class FrequencyController : BaseApiController<FrequencyController>
{
    [Authorize(Policy = Permission.Frequency.View)]
    [HttpGet]
    public async Task<ActionResult<PagedListResponse<FrequencyState>>> GetAsync([FromQuery] GetFrequencyQuery query) =>
        Ok(await Mediator.Send(query));

    [Authorize(Policy = Permission.Frequency.View)]
    [HttpGet("{id}")]
    public async Task<ActionResult<FrequencyState>> GetAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new GetFrequencyByIdQuery(id)));

    [Authorize(Policy = Permission.Frequency.Create)]
    [HttpPost]
    public async Task<ActionResult<FrequencyState>> PostAsync([FromBody] FrequencyViewModel request) =>
        await ToActionResult(async () => await Mediator.Send(Mapper.Map<AddFrequencyCommand>(request)));

    [Authorize(Policy = Permission.Frequency.Edit)]
    [HttpPut("{id}")]
    public async Task<ActionResult<FrequencyState>> PutAsync(string id, [FromBody] FrequencyViewModel request)
    {
        var command = Mapper.Map<EditFrequencyCommand>(request);
        return await ToActionResult(async () => await Mediator.Send(command with { Id = id }));
    }

    [Authorize(Policy = Permission.Frequency.Delete)]
    [HttpDelete("{id}")]
    public async Task<ActionResult<FrequencyState>> DeleteAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new DeleteFrequencyCommand { Id = id }));
}

public record FrequencyViewModel
{
    [Required]
	[StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
	public string Name { get;set; } = "";
	   
}
