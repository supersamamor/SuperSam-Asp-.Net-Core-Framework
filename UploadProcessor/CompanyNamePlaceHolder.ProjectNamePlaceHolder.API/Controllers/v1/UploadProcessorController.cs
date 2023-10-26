using CompanyNamePlaceHolder.Common.Utility.Models;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Features.ProjectNamePlaceHolder.UploadProcessor.Commands;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Features.ProjectNamePlaceHolder.UploadProcessor.Queries;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Core.ProjectNamePlaceHolder;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using CompanyNamePlaceHolder.Common.API.Controllers;

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.API.Controllers.v1;

[ApiVersion("1.0")]
public class UploadProcessorController : BaseApiController<UploadProcessorController>
{
    [Authorize(Policy = Permission.UploadProcessor.View)]
    [HttpGet]
    public async Task<ActionResult<PagedListResponse<UploadProcessorState>>> GetAsync([FromQuery] GetUploadProcessorQuery query) =>
        Ok(await Mediator.Send(query));

    [Authorize(Policy = Permission.UploadProcessor.View)]
    [HttpGet("{id}")]
    public async Task<ActionResult<UploadProcessorState>> GetAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new GetUploadProcessorByIdQuery(id)));

    [Authorize(Policy = Permission.UploadProcessor.Create)]
    [HttpPost]
    public async Task<ActionResult<UploadProcessorState>> PostAsync([FromBody] UploadProcessorViewModel request) =>
        await ToActionResult(async () => await Mediator.Send(Mapper.Map<AddUploadProcessorCommand>(request)));

    [Authorize(Policy = Permission.UploadProcessor.Edit)]
    [HttpPut("{id}")]
    public async Task<ActionResult<UploadProcessorState>> PutAsync(string id, [FromBody] UploadProcessorViewModel request)
    {
        var command = Mapper.Map<EditUploadProcessorCommand>(request);
        return await ToActionResult(async () => await Mediator.Send(command with { Id = id }));
    }

    [Authorize(Policy = Permission.UploadProcessor.Delete)]
    [HttpDelete("{id}")]
    public async Task<ActionResult<UploadProcessorState>> DeleteAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new DeleteUploadProcessorCommand { Id = id }));
}

public record UploadProcessorViewModel
{
    [Required]
	[StringLength(20, ErrorMessage = "{0} length can't be more than {1}.")]
	public string FileType { get;set; } = "";
	[Required]
	[StringLength(450, ErrorMessage = "{0} length can't be more than {1}.")]
	public string Path { get;set; } = "";
	[Required]
	[StringLength(20, ErrorMessage = "{0} length can't be more than {1}.")]
	public string Status { get;set; } = "";
	public DateTime? StartDateTime { get;set; } = DateTime.Now.Date;
	public DateTime? EndDateTime { get;set; } = DateTime.Now.Date;
	[Required]
	[StringLength(50, ErrorMessage = "{0} length can't be more than {1}.")]
	public string Module { get;set; } = "";
	[Required]
	[StringLength(50, ErrorMessage = "{0} length can't be more than {1}.")]
	public string UploadType { get;set; } = "";
	   
}
