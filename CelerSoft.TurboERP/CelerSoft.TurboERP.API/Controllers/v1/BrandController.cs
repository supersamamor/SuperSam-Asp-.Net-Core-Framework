using CelerSoft.Common.Utility.Models;
using CelerSoft.TurboERP.Application.Features.TurboERP.Brand.Commands;
using CelerSoft.TurboERP.Application.Features.TurboERP.Brand.Queries;
using CelerSoft.TurboERP.Core.TurboERP;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using CelerSoft.Common.API.Controllers;

namespace CelerSoft.TurboERP.API.Controllers.v1;

[ApiVersion("1.0")]
public class BrandController : BaseApiController<BrandController>
{
    [Authorize(Policy = Permission.Brand.View)]
    [HttpGet]
    public async Task<ActionResult<PagedListResponse<BrandState>>> GetAsync([FromQuery] GetBrandQuery query) =>
        Ok(await Mediator.Send(query));

    [Authorize(Policy = Permission.Brand.View)]
    [HttpGet("{id}")]
    public async Task<ActionResult<BrandState>> GetAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new GetBrandByIdQuery(id)));

    [Authorize(Policy = Permission.Brand.Create)]
    [HttpPost]
    public async Task<ActionResult<BrandState>> PostAsync([FromBody] BrandViewModel request) =>
        await ToActionResult(async () => await Mediator.Send(Mapper.Map<AddBrandCommand>(request)));

    [Authorize(Policy = Permission.Brand.Edit)]
    [HttpPut("{id}")]
    public async Task<ActionResult<BrandState>> PutAsync(string id, [FromBody] BrandViewModel request)
    {
        var command = Mapper.Map<EditBrandCommand>(request);
        return await ToActionResult(async () => await Mediator.Send(command with { Id = id }));
    }

    [Authorize(Policy = Permission.Brand.Delete)]
    [HttpDelete("{id}")]
    public async Task<ActionResult<BrandState>> DeleteAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new DeleteBrandCommand { Id = id }));
}

public record BrandViewModel
{
    [Required]
	[StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
	public string Name { get;set; } = "";
	   
}
