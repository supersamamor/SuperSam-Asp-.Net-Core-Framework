using CTI.FAS.Application.Features.FAS.Company.Queries;
using CTI.FAS.Application.Features.FAS.EnrolledPayee.Commands;
using CTI.FAS.Application.Features.FAS.EnrolledPayee.Queries;
using CTI.FAS.Web.Areas.FAS.Models;
using CTI.FAS.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace CTI.FAS.Web.Areas.FAS.Pages.EnrolledPayee;

[Authorize(Policy = Permission.EnrolledPayee.Create)]
public class EnrollmentModel : BasePageModel<EnrollmentModel>
{
    [BindProperty]
    public IList<PayeeEnrollmentViewModel> ForEnrollmentList { get; set; } = new List<PayeeEnrollmentViewModel>();
    [BindProperty]
    public string? DownloadUrl { get; set; }
    [BindProperty]
    [Required]
    public string? Entity { get; set; }
    public PayeeEnrollmentTabNavigationPartial PayeeEnrollmentTabNavigation { get; set; } = new() { TabName = Constants.PayeeEnrollmentTabNavigation.Enrollment };
    public async Task<IActionResult> OnGet(string? entity, string? downloadUrl)
    {
        ModelState.Clear();
        Entity = entity;
        if (!ModelState.IsValid)
        {
            return Page();
        }     
        if (string.IsNullOrEmpty(entity))
        {
            entity = (await Mediatr.Send(new GetCompanyQuery())).Data.ToList().FirstOrDefault()?.Id;
            Entity = entity;
        }        
        var query = new GetPayeeForEnrollmentQuery()
        {
            Entity = entity,
        };
        if (!string.IsNullOrEmpty(entity))
        { ForEnrollmentList = Mapper.Map<IList<PayeeEnrollmentViewModel>>(await Mediatr.Send(query)); }
        DownloadUrl = downloadUrl;
        return Page();
    }
    public async Task<IActionResult> OnPost()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }
        var selectedCreditorForEnrollment = ForEnrollmentList.Where(l => l.Enabled).Select(l => l.Id).ToList();
        if (selectedCreditorForEnrollment.Count == 0)
        {
            NotyfService.Warning(Localizer["Please select atleast 1 creditor to enroll."]);
            return Page();
        }
        try
        {
            var downloadUrl = await Mediatr.Send(new EnrollPayeeCommand(selectedCreditorForEnrollment));
            NotyfService.Success(Localizer["Enrollment success."]);
            return RedirectToPage("Enrollment", new { DownloadUrl = downloadUrl, Entity });
        }
        catch (Exception ex)
        {
            NotyfService.Error(Localizer["An error has ocurred, please contact System administrator."]);
            Logger.LogError(ex, "Exception encountered");
        }
        return Page();
    }
}
