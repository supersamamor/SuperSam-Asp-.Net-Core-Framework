using CTI.FAS.Application.Features.FAS.EnrolledPayee.Queries;
using CTI.FAS.Core.FAS;
using CTI.FAS.Web.Areas.FAS.Models;
using CTI.FAS.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace CTI.FAS.Web.Areas.FAS.Pages.EnrolledPayee;

[Authorize(Policy = Permission.EnrolledPayee.View)]
public class EnrollmentModel : BasePageModel<EnrollmentModel>
{
    public IList<EnrolledPayeeViewModel> EnrolledPayeeList { get; set; } = new List<EnrolledPayeeViewModel>();

    public PayeeEnrollmentTabNavigationPartial PayeeEnrollmentTabNavigation { get; set; } = new() { TabName = Constants.PayeeEnrollmentTabNavigation.Enrollment };
    public async Task<IActionResult> OnGet()
    {
        EnrolledPayeeList = Mapper.Map<IList<EnrolledPayeeViewModel>>(await Mediatr.Send(new GetPayeeForEnrollmentQuery()));        
        return Page();
    }
}
