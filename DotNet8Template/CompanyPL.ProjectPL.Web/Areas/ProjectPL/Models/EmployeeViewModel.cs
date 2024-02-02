using CompanyPL.Common.Web.Utility.Extensions;
using CompanyPL.ProjectPL.Web.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace CompanyPL.ProjectPL.Web.Areas.ProjectPL.Models;

public record EmployeeViewModel : BaseViewModel
{	
	[Display(Name = "DateSample")]
	public DateTime? DateSample { get; init; } = DateTime.Now.Date;
	[Display(Name = "RadioButtonSample")]
	[Required]
	public bool? RadioButtonSample { get; init; }
	[Display(Name = "DecimalSample")]
	
	[DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
	public decimal? DecimalSample { get; init; }
	[Display(Name = "IntegerSample")]
	public int? IntegerSample { get; init; }
	[Display(Name = "SampleParent")]
	[Required]
	
	public string SampleParentId { get; init; } = "";
	public string?  ReferenceFieldSampleParentId { get; set; }
	[Display(Name = "Employee Code")]
	[Required]
	[StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
	public string EmployeeCode { get; init; } = "";
	[Display(Name = "Middle Name")]
	[Required]
	[StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
	public string MiddleName { get; init; } = "";
	[Display(Name = "First Name")]
	[Required]
	[StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
	public string FirstName { get; init; } = "";
	[Display(Name = "Last Name")]
	[Required]
	[StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
	public string LastName { get; init; } = "";
	[Display(Name = "DateTimeSample")]
	public DateTime? DateTimeSample { get; init; } = DateTime.Now.Date;
	[Display(Name = "BooleanSample")]
	public bool BooleanSample { get; init; }
	
	public DateTime LastModifiedDate { get; set; }
	public SampleParentViewModel? SampleParent { get; init; }
		
	public IList<ContactInformationViewModel>? ContactInformationList { get; set; }
	public IList<HealthDeclarationViewModel>? HealthDeclarationList { get; set; }
	
}
