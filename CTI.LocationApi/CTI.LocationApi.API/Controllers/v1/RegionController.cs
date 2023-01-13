using CTI.Common.Utility.Models;
using CTI.LocationApi.Application.Features.LocationApi.Region.Commands;
using CTI.LocationApi.Application.Features.LocationApi.Region.Queries;
using CTI.LocationApi.Core.LocationApi;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using CTI.Common.API.Controllers;

namespace CTI.LocationApi.API.Controllers.v1;

[ApiVersion("1.0")]
public class RegionController : BaseApiController<RegionController>
{
    [Authorize(Policy = Permission.Region.View)]
    [HttpGet]
    public async Task<ActionResult<PagedListResponse<RegionState>>> GetAsync([FromQuery] GetRegionQuery query) =>
        Ok(await Mediator.Send(query));

    [Authorize(Policy = Permission.Region.View)]
    [HttpGet("{id}")]
    public async Task<ActionResult<RegionState>> GetAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new GetRegionByIdQuery(id)));

    [Authorize(Policy = Permission.Region.Create)]
    [HttpPost]
    public async Task<ActionResult<RegionState>> PostAsync([FromBody] RegionViewModel request) =>
        await ToActionResult(async () => await Mediator.Send(Mapper.Map<AddRegionCommand>(request)));

    [Authorize(Policy = Permission.Region.Edit)]
    [HttpPut("{id}")]
    public async Task<ActionResult<RegionState>> PutAsync(string id, [FromBody] RegionViewModel request)
    {
        var command = Mapper.Map<EditRegionCommand>(request);
        return await ToActionResult(async () => await Mediator.Send(command with { Id = id }));
    }

    [Authorize(Policy = Permission.Region.Delete)]
    [HttpDelete("{id}")]
    public async Task<ActionResult<RegionState>> DeleteAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new DeleteRegionCommand { Id = id }));
}

public record RegionViewModel
{
    [Required]
	
	public string CountryId { get;set; } = "";
	[Required]
	[StringLength(50, ErrorMessage = "{0} length can't be more than {1}.")]
	public string Code { get;set; } = "";
	[Required]
	[StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
	public string Name { get;set; } = "";
	   
}
