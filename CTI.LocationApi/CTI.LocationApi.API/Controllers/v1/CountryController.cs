using CTI.Common.Utility.Models;
using CTI.LocationApi.Application.Features.LocationApi.Country.Commands;
using CTI.LocationApi.Application.Features.LocationApi.Country.Queries;
using CTI.LocationApi.Core.LocationApi;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using CTI.Common.API.Controllers;

namespace CTI.LocationApi.API.Controllers.v1;

[ApiVersion("1.0")]
public class CountryController : BaseApiController<CountryController>
{
    [Authorize(Policy = Permission.Country.View)]
    [HttpGet]
    public async Task<ActionResult<PagedListResponse<CountryState>>> GetAsync([FromQuery] GetCountryQuery query) =>
        Ok(await Mediator.Send(query));

    [Authorize(Policy = Permission.Country.View)]
    [HttpGet("{id}")]
    public async Task<ActionResult<CountryState>> GetAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new GetCountryByIdQuery(id)));

    [Authorize(Policy = Permission.Country.Create)]
    [HttpPost]
    public async Task<ActionResult<CountryState>> PostAsync([FromBody] CountryViewModel request) =>
        await ToActionResult(async () => await Mediator.Send(Mapper.Map<AddCountryCommand>(request)));

    [Authorize(Policy = Permission.Country.Edit)]
    [HttpPut("{id}")]
    public async Task<ActionResult<CountryState>> PutAsync(string id, [FromBody] CountryViewModel request)
    {
        var command = Mapper.Map<EditCountryCommand>(request);
        return await ToActionResult(async () => await Mediator.Send(command with { Id = id }));
    }

    [Authorize(Policy = Permission.Country.Delete)]
    [HttpDelete("{id}")]
    public async Task<ActionResult<CountryState>> DeleteAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new DeleteCountryCommand { Id = id }));
}

public record CountryViewModel
{
    [Required]
	[StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
	public string Name { get;set; } = "";
	[Required]
	[StringLength(50, ErrorMessage = "{0} length can't be more than {1}.")]
	public string Code { get;set; } = "";
	   
}
