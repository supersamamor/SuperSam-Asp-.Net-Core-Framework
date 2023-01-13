using CTI.Common.Utility.Models;
using CTI.LocationApi.Application.Features.LocationApi.City.Commands;
using CTI.LocationApi.Application.Features.LocationApi.City.Queries;
using CTI.LocationApi.Core.LocationApi;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using CTI.Common.API.Controllers;

namespace CTI.LocationApi.API.Controllers.v1;

[ApiVersion("1.0")]
public class CityController : BaseApiController<CityController>
{
    [Authorize(Policy = Permission.City.View)]
    [HttpGet]
    public async Task<ActionResult<PagedListResponse<CityState>>> GetAsync([FromQuery] GetCityQuery query) =>
        Ok(await Mediator.Send(query));

    [Authorize(Policy = Permission.City.View)]
    [HttpGet("{id}")]
    public async Task<ActionResult<CityState>> GetAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new GetCityByIdQuery(id)));

    [Authorize(Policy = Permission.City.Create)]
    [HttpPost]
    public async Task<ActionResult<CityState>> PostAsync([FromBody] CityViewModel request) =>
        await ToActionResult(async () => await Mediator.Send(Mapper.Map<AddCityCommand>(request)));

    [Authorize(Policy = Permission.City.Edit)]
    [HttpPut("{id}")]
    public async Task<ActionResult<CityState>> PutAsync(string id, [FromBody] CityViewModel request)
    {
        var command = Mapper.Map<EditCityCommand>(request);
        return await ToActionResult(async () => await Mediator.Send(command with { Id = id }));
    }

    [Authorize(Policy = Permission.City.Delete)]
    [HttpDelete("{id}")]
    public async Task<ActionResult<CityState>> DeleteAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new DeleteCityCommand { Id = id }));
}

public record CityViewModel
{
    [Required]
	
	public string ProvinceId { get;set; } = "";
	[Required]
	[StringLength(50, ErrorMessage = "{0} length can't be more than {1}.")]
	public string Code { get;set; } = "";
	[Required]
	[StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
	public string Name { get;set; } = "";
	   
}
