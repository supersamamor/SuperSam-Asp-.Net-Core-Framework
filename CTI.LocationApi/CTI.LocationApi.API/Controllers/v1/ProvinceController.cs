using CTI.Common.Utility.Models;
using CTI.LocationApi.Application.Features.LocationApi.Province.Commands;
using CTI.LocationApi.Application.Features.LocationApi.Province.Queries;
using CTI.LocationApi.Core.LocationApi;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using CTI.Common.API.Controllers;

namespace CTI.LocationApi.API.Controllers.v1;

[ApiVersion("1.0")]
public class ProvinceController : BaseApiController<ProvinceController>
{
    [Authorize(Policy = Permission.Province.View)]
    [HttpGet]
    public async Task<ActionResult<PagedListResponse<ProvinceState>>> GetAsync([FromQuery] GetProvinceQuery query) =>
        Ok(await Mediator.Send(query));

    [Authorize(Policy = Permission.Province.View)]
    [HttpGet("{id}")]
    public async Task<ActionResult<ProvinceState>> GetAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new GetProvinceByIdQuery(id)));

    [Authorize(Policy = Permission.Province.Create)]
    [HttpPost]
    public async Task<ActionResult<ProvinceState>> PostAsync([FromBody] ProvinceViewModel request) =>
        await ToActionResult(async () => await Mediator.Send(Mapper.Map<AddProvinceCommand>(request)));

    [Authorize(Policy = Permission.Province.Edit)]
    [HttpPut("{id}")]
    public async Task<ActionResult<ProvinceState>> PutAsync(string id, [FromBody] ProvinceViewModel request)
    {
        var command = Mapper.Map<EditProvinceCommand>(request);
        return await ToActionResult(async () => await Mediator.Send(command with { Id = id }));
    }

    [Authorize(Policy = Permission.Province.Delete)]
    [HttpDelete("{id}")]
    public async Task<ActionResult<ProvinceState>> DeleteAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new DeleteProvinceCommand { Id = id }));
}

public record ProvinceViewModel
{
    [Required]
	
	public string RegionId { get;set; } = "";
	[Required]
	[StringLength(50, ErrorMessage = "{0} length can't be more than {1}.")]
	public string Code { get;set; } = "";
	[Required]
	[StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
	public string Name { get;set; } = "";
	   
}
