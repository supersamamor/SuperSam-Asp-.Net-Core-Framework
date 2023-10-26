using CompanyNamePlaceHolder.Common.Utility.Models;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Features.ProjectNamePlaceHolder.UploadStaging.Commands;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Features.ProjectNamePlaceHolder.UploadStaging.Queries;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Core.ProjectNamePlaceHolder;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using CompanyNamePlaceHolder.Common.API.Controllers;

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.API.Controllers.v1;

[ApiVersion("1.0")]
public class UploadStagingController : BaseApiController<UploadStagingController>
{
    [Authorize(Policy = Permission.UploadStaging.View)]
    [HttpGet]
    public async Task<ActionResult<PagedListResponse<UploadStagingState>>> GetAsync([FromQuery] GetUploadStagingQuery query) =>
        Ok(await Mediator.Send(query));

    [Authorize(Policy = Permission.UploadStaging.View)]
    [HttpGet("{id}")]
    public async Task<ActionResult<UploadStagingState>> GetAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new GetUploadStagingByIdQuery(id)));

    [Authorize(Policy = Permission.UploadStaging.Create)]
    [HttpPost]
    public async Task<ActionResult<UploadStagingState>> PostAsync([FromBody] UploadStagingViewModel request) =>
        await ToActionResult(async () => await Mediator.Send(Mapper.Map<AddUploadStagingCommand>(request)));

    [Authorize(Policy = Permission.UploadStaging.Edit)]
    [HttpPut("{id}")]
    public async Task<ActionResult<UploadStagingState>> PutAsync(string id, [FromBody] UploadStagingViewModel request)
    {
        var command = Mapper.Map<EditUploadStagingCommand>(request);
        return await ToActionResult(async () => await Mediator.Send(command with { Id = id }));
    }

    [Authorize(Policy = Permission.UploadStaging.Delete)]
    [HttpDelete("{id}")]
    public async Task<ActionResult<UploadStagingState>> DeleteAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new DeleteUploadStagingCommand { Id = id }));
}

public record UploadStagingViewModel
{
    [Required]
	
	public string UploadProcessorId { get;set; } = "";
	[Required]
	
	public string Data { get;set; } = "";
	[Required]
	[StringLength(50, ErrorMessage = "{0} length can't be more than {1}.")]
	public string Status { get;set; } = "";
	public DateTime? ProcessedDateTime { get;set; } = DateTime.Now.Date;
	   
}
