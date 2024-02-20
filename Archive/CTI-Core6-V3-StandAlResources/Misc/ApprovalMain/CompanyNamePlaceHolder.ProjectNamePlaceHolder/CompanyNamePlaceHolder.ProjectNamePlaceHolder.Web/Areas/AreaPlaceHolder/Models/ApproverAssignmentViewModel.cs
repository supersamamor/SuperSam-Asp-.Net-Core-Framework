using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Models;
using System.ComponentModel.DataAnnotations;

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Areas.AreaPlaceHolder.Models;

public record ApproverAssignmentViewModel : BaseViewModel
{
    [Display(Name = "Approver")]
    [StringLength(250, ErrorMessage = "{0} length can't be more than {1}.")]
    public string? ApproverUserId { get; init; } = "";
    [Display(Name = "Role Approver")]  
    [StringLength(250, ErrorMessage = "{0} length can't be more than {1}.")]
    public string? ApproverRoleId { get; init; } = "";
    [Display(Name = "Approver Setup")]
    [Required]
    public string ApproverSetupId { get; init; } = "";
    [Display(Name = "Sequence")]
    [Required]
    public int Sequence { get; set; }

    public DateTime LastModifiedDate { get; set; }
    public ApproverSetupViewModel? ApproverSetup { get; init; }
    public string ApproverType { get; init; } = Core.ProjectNamePlaceHolder.ApproverTypes.User;


}
