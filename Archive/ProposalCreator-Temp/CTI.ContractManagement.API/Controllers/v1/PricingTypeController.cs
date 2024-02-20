using CTI.Common.Utility.Models;
using CTI.ContractManagement.Application.Features.ContractManagement.PricingType.Commands;
using CTI.ContractManagement.Application.Features.ContractManagement.PricingType.Queries;
using CTI.ContractManagement.Core.ContractManagement;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using CTI.Common.API.Controllers;

namespace CTI.ContractManagement.API.Controllers.v1;

[ApiVersion("1.0")]
public class PricingTypeController : BaseApiController<PricingTypeController>
{
    [Authorize(Policy = Permission.PricingType.View)]
    [HttpGet]
    public async Task<ActionResult<PagedListResponse<PricingTypeState>>> GetAsync([FromQuery] GetPricingTypeQuery query) =>
        Ok(await Mediator.Send(query));

    [Authorize(Policy = Permission.PricingType.View)]
    [HttpGet("{id}")]
    public async Task<ActionResult<PricingTypeState>> GetAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new GetPricingTypeByIdQuery(id)));

    [Authorize(Policy = Permission.PricingType.Create)]
    [HttpPost]
    public async Task<ActionResult<PricingTypeState>> PostAsync([FromBody] PricingTypeViewModel request) =>
        await ToActionResult(async () => await Mediator.Send(Mapper.Map<AddPricingTypeCommand>(request)));

    [Authorize(Policy = Permission.PricingType.Edit)]
    [HttpPut("{id}")]
    public async Task<ActionResult<PricingTypeState>> PutAsync(string id, [FromBody] PricingTypeViewModel request)
    {
        var command = Mapper.Map<EditPricingTypeCommand>(request);
        return await ToActionResult(async () => await Mediator.Send(command with { Id = id }));
    }

    [Authorize(Policy = Permission.PricingType.Delete)]
    [HttpDelete("{id}")]
    public async Task<ActionResult<PricingTypeState>> DeleteAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new DeletePricingTypeCommand { Id = id }));
}

public record PricingTypeViewModel
{
    [Required]
	[StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
	public string Name { get;set; } = "";
	   
}
