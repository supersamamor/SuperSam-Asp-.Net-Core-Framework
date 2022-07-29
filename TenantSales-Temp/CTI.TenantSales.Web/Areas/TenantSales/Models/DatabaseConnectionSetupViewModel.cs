using CTI.Common.Web.Utility.Extensions;
using CTI.TenantSales.Web.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace CTI.TenantSales.Web.Areas.TenantSales.Models;

public record DatabaseConnectionSetupViewModel : BaseViewModel
{	
	[Display(Name = "Code")]
	[Required]
	[StringLength(20, ErrorMessage = "{0} length can't be more than {1}.")]
	public string Code { get; init; } = "";
	[Display(Name = "Name")]
	[Required]
	[StringLength(100, ErrorMessage = "{0} length can't be more than {1}.")]
	public string Name { get; init; } = "";
	[Display(Name = "Database And Server Name")]
	[Required]
	[StringLength(100, ErrorMessage = "{0} length can't be more than {1}.")]
	public string DatabaseAndServerName { get; init; } = "";
	[Display(Name = "Inhouse Database And Server Name")]
	[StringLength(100, ErrorMessage = "{0} length can't be more than {1}.")]
	public string? InhouseDatabaseAndServerName { get; init; }
	[Display(Name = "System Connection String")]
	[StringLength(1000, ErrorMessage = "{0} length can't be more than {1}.")]
	public string? SystemConnectionString { get; init; }
	[Display(Name = "System Source")]
	[Required]
	
	[DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
	public int SystemSource { get; init; }
	[Display(Name = "Exhibit Theme Codes")]
	[StringLength(1000, ErrorMessage = "{0} length can't be more than {1}.")]
	public string? ExhibitThemeCodes { get; init; }
	[Display(Name = "Disabled")]
	public bool IsDisabled { get; init; }
	
	public DateTime LastModifiedDate { get; set; }
		
	public IList<CompanyViewModel>? CompanyList { get; set; }
	
}
