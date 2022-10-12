using CTI.SQLReportAutoSender.Web.Models;
using System.ComponentModel.DataAnnotations;

namespace CTI.SQLReportAutoSender.Web.Areas.SQLReportAutoSender.Models;

public record ApproverSetupViewModel : BaseViewModel
{
    [Display(Name = "Table")]
    [Required]
    [StringLength(450, ErrorMessage = "{0} length can't be more than {1}.")]
    public string TableName { get; init; } = "";
    [Display(Name = "Approval Type")]
    [Required]
    [StringLength(450, ErrorMessage = "{0} length can't be more than {1}.")]
    public string ApprovalType { get; init; } = "";

    public DateTime LastModifiedDate { get; set; }
    [Display(Name = "Email Subject")]
    [Required]
    [StringLength(450, ErrorMessage = "{0} length can't be more than {1}.")]
    public string EmailSubject { get; init; } = "";
    [Display(Name = "Email Body (Html)")]
    [Required]
    public string EmailBody { get; init; } = "";
    public IList<ApproverAssignmentViewModel>? ApproverAssignmentList { get; set; }
    public string SelectedApprovers
    {
        get
        {
            if (this.ApproverAssignmentList != null)
            {
                return string.Join(",", this.ApproverAssignmentList.Select(l => l.ApproverUserId).ToList());
            }
            else
            {
                return "";
            }
        }
    }
    public string ApprovalSetupType { get; init; } = "";
}
