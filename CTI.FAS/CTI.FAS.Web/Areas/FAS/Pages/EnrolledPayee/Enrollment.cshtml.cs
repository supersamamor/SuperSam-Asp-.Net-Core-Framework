using CTI.FAS.Application.Features.FAS.EnrolledPayee.Commands;
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
    [BindProperty]
    public IList<PayeeEnrollmentViewModel> ForEnrollmentList { get; set; } = new List<PayeeEnrollmentViewModel>();

    public PayeeEnrollmentTabNavigationPartial PayeeEnrollmentTabNavigation { get; set; } = new() { TabName = Constants.PayeeEnrollmentTabNavigation.Enrollment };
    public async Task<IActionResult> OnGet()
    {
        ForEnrollmentList = Mapper.Map<IList<PayeeEnrollmentViewModel>>(await Mediatr.Send(new GetPayeeForEnrollmentQuery()));
        return Page();
    }
    public async Task<IActionResult> OnPost()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }
        try
        {
            await Mediatr.Send(new EnrollPayeeCommand(ForEnrollmentList.Where(l => l.Enabled).Select(l => l.Id).ToList()));
            NotyfService.Success(Localizer["Enrollment success."]);
            return RedirectToPage("Enrollment");
        }
        catch (Exception ex)
        {
            NotyfService.Error(Localizer["An error has ocurred, please contact System administrator."]);
            Logger.LogError(ex, "Exception encountered");
        }
        return Page();
    }
}
