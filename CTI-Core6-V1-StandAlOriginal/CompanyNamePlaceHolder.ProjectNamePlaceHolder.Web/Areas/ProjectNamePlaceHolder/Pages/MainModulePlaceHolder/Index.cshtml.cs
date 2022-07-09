using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Features.ProjectNamePlaceHolder.Approval.Queries;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Features.ProjectNamePlaceHolder.MainModulePlaceHolder.Queries;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Core.ProjectNamePlaceHolder;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Areas.ProjectNamePlaceHolder.Models;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Models;
using DataTables.AspNetCore.Mvc.Binder;
using LanguageExt;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Areas.ProjectNamePlaceHolder.Pages.MainModulePlaceHolder;

[Authorize(Policy = Permission.MainModulePlaceHolder.View)]
public class IndexModel : BasePageModel<IndexModel>
{
    public MainModulePlaceHolderViewModel MainModulePlaceHolder { get; set; } = new();

    [DataTablesRequest]
    public DataTablesRequest? DataRequest { get; set; }

    public IActionResult OnGet()
    {
        return Page();
    }

    public async Task<IActionResult> OnPostListAllAsync()
    {
        var result = await Mediatr.Send(DataRequest!.ToQuery<GetMainModulePlaceHolderQuery>());
        return new JsonResult(result.Data
            .Select(e => new
            {
                e.Id,
                e.Code,
                e.LastModifiedDate,
                StatusBadge = GetApprovalStatus(e.Id),
            })
            .ToDataTablesResponse(DataRequest, result.TotalCount, result.MetaData.TotalItemCount));
    }

    public async Task<IActionResult> OnGetSelect2Data([FromQuery] Select2Request request)
    {
        var result = await Mediatr.Send(request.ToQuery<GetMainModulePlaceHolderQuery>(nameof(MainModulePlaceHolderState.Code)));
        return new JsonResult(result.ToSelect2Response(e => new Select2Result { Id = e.Id, Text = e.Code }));
    }
    private string GetApprovalStatus(string dataId)
    {
        string approvalStatus = "";
        _ = Mediatr.Send(new GetApprovalStatusByIdQuery(dataId)).Result.Select(l=> approvalStatus = l);
        switch (approvalStatus)
        {
            case ApprovalStatus.New:
                return @"<span class=""badge badge-secondary"">" + approvalStatus + "</span>";
            case ApprovalStatus.ForApproval:
                return @"<span class=""badge badge-info"">" + approvalStatus + "</span>";
            case ApprovalStatus.PartiallyApproved:
                return @"<span class=""badge badge-primary"">" + approvalStatus + "</span>";
            case ApprovalStatus.Approved:
                return @"<span class=""badge badge-success"">" + approvalStatus + "</span>";
            case ApprovalStatus.Rejected:
                return @"<span class=""badge badge-danger"">" + approvalStatus + "</span>";
            default:
                break;
        }
        return approvalStatus;
    }
}
