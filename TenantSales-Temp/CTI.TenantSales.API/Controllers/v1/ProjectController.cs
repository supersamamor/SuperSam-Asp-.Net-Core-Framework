using CTI.Common.Utility.Models;
using CTI.TenantSales.Application.Features.TenantSales.Project.Commands;
using CTI.TenantSales.Application.Features.TenantSales.Project.Queries;
using CTI.TenantSales.Core.TenantSales;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using CTI.Common.API.Controllers;

namespace CTI.TenantSales.API.Controllers.v1;

[ApiVersion("1.0")]
public class ProjectController : BaseApiController<ProjectController>
{
    [Authorize(Policy = Permission.Project.View)]
    [HttpGet]
    public async Task<ActionResult<PagedListResponse<ProjectState>>> GetAsync([FromQuery] GetProjectQuery query) =>
        Ok(await Mediator.Send(query));

    [Authorize(Policy = Permission.Project.View)]
    [HttpGet("{id}")]
    public async Task<ActionResult<ProjectState>> GetAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new GetProjectByIdQuery(id)));

    [Authorize(Policy = Permission.Project.Create)]
    [HttpPost]
    public async Task<ActionResult<ProjectState>> PostAsync([FromBody] ProjectViewModel request) =>
        await ToActionResult(async () => await Mediator.Send(Mapper.Map<AddProjectCommand>(request)));

    [Authorize(Policy = Permission.Project.Edit)]
    [HttpPut("{id}")]
    public async Task<ActionResult<ProjectState>> PutAsync(string id, [FromBody] ProjectViewModel request)
    {
        var command = Mapper.Map<EditProjectCommand>(request);
        return await ToActionResult(async () => await Mediator.Send(command with { Id = id }));
    }

    [Authorize(Policy = Permission.Project.Delete)]
    [HttpDelete("{id}")]
    public async Task<ActionResult<ProjectState>> DeleteAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new DeleteProjectCommand { Id = id }));
}

public record ProjectViewModel
{
    [Required]
	
	public string CompanyId { get;set; } = "";
	[Required]
	[StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
	public string Name { get;set; } = "";
	[Required]
	[StringLength(20, ErrorMessage = "{0} length can't be more than {1}.")]
	public string Code { get;set; } = "";
	[StringLength(100, ErrorMessage = "{0} length can't be more than {1}.")]
	public string? PayableTo { get;set; }
	[StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
	public string? ProjectAddress { get;set; }
	[StringLength(100, ErrorMessage = "{0} length can't be more than {1}.")]
	public string? Location { get;set; }
	[StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
	public string? ProjectNameANSection { get;set; }
	[Required]
	
	[DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
	public decimal LandArea { get;set; }
	[Required]
	
	[DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
	public decimal GLA { get;set; }
	public bool IsDisabled { get;set; }
	[StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
	public string? SignatoryName { get;set; }
	[StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
	public string? SignatoryPosition { get;set; }
	[StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
	public string? ANSignatoryName { get;set; }
	[StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
	public string? ANSignatoryPosition { get;set; }
	[StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
	public string? ContractSignatory { get;set; }
	[StringLength(50, ErrorMessage = "{0} length can't be more than {1}.")]
	public string? ContractSignatoryPosition { get;set; }
	[StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
	public string? ContractSignatoryProofOfIdentity { get;set; }
	[StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
	public string? ContractSignatoryWitness { get;set; }
	[StringLength(50, ErrorMessage = "{0} length can't be more than {1}.")]
	public string? ContractSignatoryWitnessPosition { get;set; }
	[Required]
	public bool OutsideFC { get;set; }
	[StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
	public string? ProjectGreetingsSection { get;set; }
	[StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
	public string? ProjectShortName { get;set; }
	[StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
	public string? SignatureUpper { get;set; }
	[StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
	public string? SignatureLower { get;set; }
	[Required]
	public bool HasAssociationDues { get;set; }
	[StringLength(100, ErrorMessage = "{0} length can't be more than {1}.")]
	public string? SalesUploadFolder { get;set; }
	[Required]
	public bool EnableMeterReadingApp { get;set; }
	[StringLength(50, ErrorMessage = "{0} length can't be more than {1}.")]
	public string? CurrencyCode { get;set; }
	public int? CurrencyRate { get;set; }
	public int? GasCutOff { get;set; }
	public int? PowerCutOff { get;set; }
	public int? WaterCutOff { get;set; }
	   
}
