using CTI.Common.Web.Utility.Extensions;
using CTI.ELMS.Web.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace CTI.ELMS.Web.Areas.ELMS.Models;

public record ProjectViewModel : BaseViewModel
{	
	[Display(Name = "Project Name")]
	[Required]
	[StringLength(100, ErrorMessage = "{0} length can't be more than {1}.")]
	public string ProjectName { get; init; } = "";
	[Display(Name = "Database Source")]
	[StringLength(30, ErrorMessage = "{0} length can't be more than {1}.")]
	public string? DatabaseSource { get; init; }
	[Display(Name = "Entity")]
	
	public string? EntityGroupId { get; init; }
	public string?  ForeignKeyEntityGroup { get; set; }
	[Display(Name = "IFCA Project Code")]
	[StringLength(20, ErrorMessage = "{0} length can't be more than {1}.")]
	public string? IFCAProjectCode { get; init; }
	[Display(Name = "Payable To")]
	[Required]
	[StringLength(100, ErrorMessage = "{0} length can't be more than {1}.")]
	public string PayableTo { get; init; } = "";
	[Display(Name = "Project Address")]
	[Required]
	[StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
	public string ProjectAddress { get; init; } = "";
	[Display(Name = "Location")]
	[StringLength(100, ErrorMessage = "{0} length can't be more than {1}.")]
	public string? Location { get; init; }
	[Display(Name = "Project Name / AN Section")]
	[Required]
	[StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
	public string ProjectNameANSection { get; init; } = "";
	[Display(Name = "Land Area")]
	
	[DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
	public decimal? LandArea { get; init; }
	[Display(Name = "GLA")]
	
	[DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
	public decimal? GLA { get; init; }
	[Display(Name = "Is Disabled")]
	public bool IsDisabled { get; init; }
	[Display(Name = "Signatory Name")]
	[Required]
	[StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
	public string SignatoryName { get; init; } = "";
	[Display(Name = "Signatory Position")]
	[Required]
	[StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
	public string SignatoryPosition { get; init; } = "";
	[Display(Name = "AN Signatory Name")]
	[Required]
	[StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
	public string ANSignatoryName { get; init; } = "";
	[Display(Name = "AN Signatory Position")]
	[Required]
	[StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
	public string ANSignatoryPosition { get; init; } = "";
	[Display(Name = "Contract Signatory")]
	[Required]
	[StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
	public string ContractSignatory { get; init; } = "";
	[Display(Name = "Contract Signatory Position")]
	[Required]
	[StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
	public string ContractSignatoryPosition { get; init; } = "";
	[Display(Name = "Contract Signatory Proof of Identity")]
	[Required]
	[StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
	public string ContractSignatoryProofOfIdentity { get; init; } = "";
	[Display(Name = "Contract Signatory Witness")]
	[StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
	public string? ContractSignatoryWitness { get; init; }
	[Display(Name = "Contract Signatory Witness Position")]
	[StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
	public string? ContractSignatoryWitnessPosition { get; init; }
	[Display(Name = "Outside FC")]
	public bool OutsideFC { get; init; }
	[Display(Name = "Project Greetings Section")]
	[StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
	public string? ProjectGreetingsSection { get; init; }
	[Display(Name = "Project Short Name")]
	[StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
	public string? ProjectShortName { get; init; }
	[Display(Name = "Signature Upper")]
	[StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
	public string? SignatureUpper { get; init; }
	[Display(Name = "Signature Lower")]
	[StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
	public string? SignatureLower { get; init; }
	[Display(Name = "Has Association Dues")]
	public bool HasAssociationDues { get; init; }
	
	public DateTime LastModifiedDate { get; set; }
	public EntityGroupViewModel? EntityGroup { get; init; }
		
	public IList<UserProjectAssignmentViewModel>? UserProjectAssignmentList { get; set; }
	public IList<UnitViewModel>? UnitList { get; set; }
	public IList<UnitBudgetViewModel>? UnitBudgetList { get; set; }
	public IList<ActivityViewModel>? ActivityList { get; set; }
	public IList<OfferingViewModel>? OfferingList { get; set; }
	public IList<OfferingViewModel>? OfferingHistoryList { get; set; }
	public IList<IFCATenantInformationViewModel>? IFCATenantInformationList { get; set; }
	public IList<IFCAARLedgerViewModel>? IFCAARLedgerList { get; set; }
	public IList<IFCAARAllocationViewModel>? IFCAARAllocationList { get; set; }
	public IList<ReportTableCollectionDetailViewModel>? ReportTableCollectionDetailList { get; set; }
	
}
