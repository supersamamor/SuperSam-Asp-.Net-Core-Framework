using CTI.FAS.Application.Features.FAS.Creditor.Queries;
using CTI.FAS.Application.Features.FAS.EnrolledPayee.Commands;
using CTI.FAS.Web.Areas.FAS.Models;
using CTI.FAS.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CTI.FAS.Web.Areas.FAS.Pages.EnrolledPayee;

[Authorize(Policy = Permission.EnrolledPayee.Create)]
public class AddModel : BasePageModel<AddModel>
{
    [BindProperty]
    public EnrolledPayeeViewModel EnrolledPayee { get; set; } = new();
    [BindProperty]
    public string? RemoveSubDetailId { get; set; }
    [BindProperty]
    public string? AsyncAction { get; set; }
    public IActionResult OnGet(string? entity)
    {
        if (!string.IsNullOrEmpty(entity))
        {
            EnrolledPayee.CompanyId = entity;
        }
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
            await Mediatr.Send(Mapper.Map<AddEnrolledPayeeCommand>(EnrolledPayee));
            NotyfService.Success(Localizer["Successfully saved the data."]);
            return RedirectToPage("Enrollment", new { Entity = EnrolledPayee.CompanyId });
        }
        catch (Exception ex)
        {
            NotyfService.Error(Localizer["An error has ocurred, please contact System administrator."]);
            Logger.LogError(ex, "Exception encountered");
        }
        return Page();
    }
    public async Task<IActionResult> OnPostChangeFormValue()
    {
        ModelState.Clear();
        if (AsyncAction == "AddEnrolledPayeeEmail")
        {
            return AddEnrolledPayeeEmail();
        }
        if (AsyncAction == "RemoveEnrolledPayeeEmail")
        {
            return RemoveEnrolledPayeeEmail();
        }
        if (AsyncAction == "GetDefaultEmail")
        {
            return await GetDefaultEmail();
        }
        return Partial("_InputFieldsPartial", EnrolledPayee);
    }

    private IActionResult AddEnrolledPayeeEmail()
    {
        ModelState.Clear();
        if (EnrolledPayee!.EnrolledPayeeEmailList == null) { EnrolledPayee!.EnrolledPayeeEmailList = new List<EnrolledPayeeEmailViewModel>(); }
        EnrolledPayee!.EnrolledPayeeEmailList!.Add(new EnrolledPayeeEmailViewModel() { EnrolledPayeeId = EnrolledPayee.Id });
        return Partial("_InputFieldsPartial", EnrolledPayee);
    }
    private IActionResult RemoveEnrolledPayeeEmail()
    {
        ModelState.Clear();
        EnrolledPayee.EnrolledPayeeEmailList = EnrolledPayee!.EnrolledPayeeEmailList!.Where(l => l.Id != RemoveSubDetailId).ToList();
        return Partial("_InputFieldsPartial", EnrolledPayee);
    }
    private async Task<IActionResult> GetDefaultEmail()
    {
        ModelState.Clear();
        _ = (await Mediatr.Send(new GetCreditorByIdQuery(EnrolledPayee.CreditorId))).Select(l => EnrolledPayee.Email = l.Email);
        return Partial("_InputFieldsPartial", EnrolledPayee);
    }
}
