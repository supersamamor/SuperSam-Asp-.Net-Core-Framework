using CTI.Common.Utility.Models;
using CTI.LocationApi.Application.Features.LocationApi.Location.Commands;
using CTI.LocationApi.Application.Features.LocationApi.Location.Queries;
using CTI.LocationApi.Core.LocationApi;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using CTI.Common.API.Controllers;

namespace CTI.LocationApi.API.Controllers.v1;

[ApiVersion("1.0")]
public class LocationController : BaseApiController<LocationController>
{
    [Authorize(Policy = Permission.Location.View)]
    [HttpGet]
    public async Task<ActionResult<PagedListResponse<LocationState>>> GetAsync([FromQuery] GetLocationQuery query) =>
        Ok(await Mediator.Send(query));

    [Authorize(Policy = Permission.Location.View)]
    [HttpGet("{id}")]
    public async Task<ActionResult<LocationState>> GetAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new GetLocationByIdQuery(id)));

    [Authorize(Policy = Permission.Location.Create)]
    [HttpPost]
    public async Task<ActionResult<LocationState>> PostAsync([FromBody] LocationViewModel request) =>
        await ToActionResult(async () => await Mediator.Send(Mapper.Map<AddLocationCommand>(request)));

    [Authorize(Policy = Permission.Location.Edit)]
    [HttpPut("{id}")]
    public async Task<ActionResult<LocationState>> PutAsync(string id, [FromBody] LocationViewModel request)
    {
        var command = Mapper.Map<EditLocationCommand>(request);
        return await ToActionResult(async () => await Mediator.Send(command with { Id = id }));
    }

    [Authorize(Policy = Permission.Location.Delete)]
    [HttpDelete("{id}")]
    public async Task<ActionResult<LocationState>> DeleteAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new DeleteLocationCommand { Id = id }));
}

public record LocationViewModel
{
    [Required]
	[StringLength(50, ErrorMessage = "{0} length can't be more than {1}.")]
	public string BarangayCode { get;set; } = "";
	[Required]
	[StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
	public string Barangay { get;set; } = "";
	[Required]
	[StringLength(50, ErrorMessage = "{0} length can't be more than {1}.")]
	public string CityCode { get;set; } = "";
	[Required]
	[StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
	public string City { get;set; } = "";
	[Required]
	[StringLength(50, ErrorMessage = "{0} length can't be more than {1}.")]
	public string ProvinceCode { get;set; } = "";
	[Required]
	[StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
	public string Province { get;set; } = "";
	[Required]
	[StringLength(50, ErrorMessage = "{0} length can't be more than {1}.")]
	public string RegionCode { get;set; } = "";
	[Required]
	[StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
	public string Region { get;set; } = "";
	[Required]
	
	public string Full { get;set; } = "";
	   
}
