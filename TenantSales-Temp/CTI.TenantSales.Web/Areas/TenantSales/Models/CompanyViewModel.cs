using CTI.Common.Web.Utility.Extensions;
using CTI.TenantSales.Web.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace CTI.TenantSales.Web.Areas.TenantSales.Models;

public record CompanyViewModel : BaseViewModel
{	
	[Display(Name = "Name")]
	[Required]
	[StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
	public string Name { get; init; } = "";
	[Display(Name = "Code")]
	[Required]
	[StringLength(5, ErrorMessage = "{0} length can't be more than {1}.")]
	public string Code { get; init; } = "";
	[Display(Name = "Address Line 1")]
	[StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
	public string? EntityAddress { get; init; }
	[Display(Name = "Address Line 2")]
	[StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
	public string? EntityAddressSecondLine { get; init; }
	[Display(Name = "Description")]
	[StringLength(100, ErrorMessage = "{0} length can't be more than {1}.")]
	public string? EntityDescription { get; init; }
	[Display(Name = "Short Name")]
	[StringLength(20, ErrorMessage = "{0} length can't be more than {1}.")]
	public string? EntityShortName { get; init; }
	[Display(Name = "Disabled")]
	public bool IsDisabled { get; init; }
	[Display(Name = "TINNo")]
	[StringLength(17, ErrorMessage = "{0} length can't be more than {1}.")]
	public string? TINNo { get; init; }
	[Display(Name = "Database Connection Setup")]
	[Required]
	
	public string DatabaseConnectionSetupId { get; init; } = "";
	public string?  ForeignKeyDatabaseConnectionSetup { get; set; }
	
	public DateTime LastModifiedDate { get; set; }
	public DatabaseConnectionSetupViewModel? DatabaseConnectionSetup { get; init; }
		
	public IList<ProjectViewModel>? ProjectList { get; set; }
	
}
