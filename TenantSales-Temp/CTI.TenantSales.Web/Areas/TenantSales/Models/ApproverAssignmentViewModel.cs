using CTI.TenantSales.Web.Models;
using System.ComponentModel.DataAnnotations;

namespace CTI.TenantSales.Web.Areas.TenantSales.Models;

public record ApproverAssignmentViewModel : BaseViewModel
{
    [Display(Name = "Approver")]
    [Required]
    [StringLength(250, ErrorMessage = "{0} length can't be more than {1}.")]
    public string ApproverUserId { get; init; } = "";
    [Display(Name = "Approver Setup")]
    [Required]
    public string ApproverSetupId { get; init; } = "";
    [Display(Name = "Sequence")]
    [Required]
    public int Sequence { get; set; }

    public DateTime LastModifiedDate { get; set; }
    public ApproverSetupViewModel? ApproverSetup { get; init; }


}
