using CTI.FAS.Application.Features.FAS.EnrolledPayee.Queries;
using CTI.FAS.Core.Constants;
using CTI.FAS.Core.FAS;
using CTI.FAS.Web.Areas.FAS.Models;
using CTI.FAS.Web.Models;
using DataTables.AspNetCore.Mvc.Binder;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;



namespace CTI.FAS.Web.Areas.FAS.Pages.EnrolledPayee;

[Authorize(Policy = Permission.EnrolledPayee.View)]
public class InActivePayeeModel : BasePageModel<InActivePayeeModel>
{
    public EnrolledPayeeViewModel EnrolledPayee { get; set; } = new();

    [DataTablesRequest]
    public DataTablesRequest? DataRequest { get; set; }
    public PayeeEnrollmentTabNavigationPartial PayeeEnrollmentTabNavigation { get; set; } = new() { TabName = Constants.PayeeEnrollmentTabNavigation.InActive };
    public IActionResult OnGet()
    {    
        return Page();
    }

    public async Task<IActionResult> OnPostListAllAsync()
    {
        var query = DataRequest!.ToQuery<GetEnrolledPayeeQuery>();
        query.Status = EnrollmentStatus.InActive;
        var result = await Mediatr.Send(query);      
        return new JsonResult(result.Data
            .Select(e => new
            {
                e.Id,
                Entity = e.Company?.EntityDisplayDescription,
				Creditor = e.Creditor?.CreditorDisplayDescription,
				e.PayeeAccountType,
                Status = Helper.EnrollmentStatusHelper.GetEnrollmentStatus(e.Status),
                e.LastModifiedDate
            })
            .ToDataTablesResponse(DataRequest, result.TotalCount, result.MetaData.TotalItemCount));
    } 
	
	public async Task<IActionResult> OnGetSelect2Data([FromQuery] Select2Request request)
    {
        var result = await Mediatr.Send(request.ToQuery<GetEnrolledPayeeQuery>(nameof(EnrolledPayeeState.Id)));
        return new JsonResult(result.ToSelect2Response(e => new Select2Result { Id = e.Id, Text = e.Id }));
    }
}
