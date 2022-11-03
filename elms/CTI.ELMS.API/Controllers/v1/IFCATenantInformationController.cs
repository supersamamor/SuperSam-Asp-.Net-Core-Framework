using CTI.Common.Utility.Models;
using CTI.ELMS.Application.Features.ELMS.IFCATenantInformation.Commands;
using CTI.ELMS.Application.Features.ELMS.IFCATenantInformation.Queries;
using CTI.ELMS.Core.ELMS;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using CTI.Common.API.Controllers;

namespace CTI.ELMS.API.Controllers.v1;

[ApiVersion("1.0")]
public class IFCATenantInformationController : BaseApiController<IFCATenantInformationController>
{
    [Authorize(Policy = Permission.IFCATenantInformation.View)]
    [HttpGet]
    public async Task<ActionResult<PagedListResponse<IFCATenantInformationState>>> GetAsync([FromQuery] GetIFCATenantInformationQuery query) =>
        Ok(await Mediator.Send(query));

    [Authorize(Policy = Permission.IFCATenantInformation.View)]
    [HttpGet("{id}")]
    public async Task<ActionResult<IFCATenantInformationState>> GetAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new GetIFCATenantInformationByIdQuery(id)));

    [Authorize(Policy = Permission.IFCATenantInformation.Create)]
    [HttpPost]
    public async Task<ActionResult<IFCATenantInformationState>> PostAsync([FromBody] IFCATenantInformationViewModel request) =>
        await ToActionResult(async () => await Mediator.Send(Mapper.Map<AddIFCATenantInformationCommand>(request)));

    [Authorize(Policy = Permission.IFCATenantInformation.Edit)]
    [HttpPut("{id}")]
    public async Task<ActionResult<IFCATenantInformationState>> PutAsync(string id, [FromBody] IFCATenantInformationViewModel request)
    {
        var command = Mapper.Map<EditIFCATenantInformationCommand>(request);
        return await ToActionResult(async () => await Mediator.Send(command with { Id = id }));
    }

    [Authorize(Policy = Permission.IFCATenantInformation.Delete)]
    [HttpDelete("{id}")]
    public async Task<ActionResult<IFCATenantInformationState>> DeleteAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new DeleteIFCATenantInformationCommand { Id = id }));
}

public record IFCATenantInformationViewModel
{
    [Required]
	
	public string OfferingID { get;set; } = "";
	[Required]
	
	public string TenantContractNo { get;set; } = "";
	public bool IsExhibit { get;set; }
	[Required]
	
	public string ProjectID { get;set; } = "";
	[Required]
	[StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
	public string TradeName { get;set; } = "";
	[Required]
	[StringLength(20, ErrorMessage = "{0} length can't be more than {1}.")]
	public string TINNumber { get;set; } = "";
	
	[DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
	public decimal? PaidSecurityDeposit { get;set; }
	
	[DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
	public decimal? Allowance { get;set; }
	[Required]
	[StringLength(100, ErrorMessage = "{0} length can't be more than {1}.")]
	public string TenantCategory { get;set; } = "";
	public bool IsAnchor { get;set; }
	[Required]
	[StringLength(100, ErrorMessage = "{0} length can't be more than {1}.")]
	public string TenantClassification { get;set; } = "";
	   
}
