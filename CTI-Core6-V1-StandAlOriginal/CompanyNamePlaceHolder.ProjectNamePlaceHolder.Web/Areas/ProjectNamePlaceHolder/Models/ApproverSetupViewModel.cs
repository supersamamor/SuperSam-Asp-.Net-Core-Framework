using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Models;
using System.ComponentModel.DataAnnotations;

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Areas.ProjectNamePlaceHolder.Models;

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
}
