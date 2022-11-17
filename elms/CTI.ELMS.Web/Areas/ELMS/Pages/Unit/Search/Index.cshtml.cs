using CTI.ELMS.Application.Features.ELMS.Unit.Queries;
using CTI.ELMS.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace CTI.ELMS.Web.Areas.ELMS.Pages.Unit.Search
{
    [Authorize(Policy = Permission.Unit.View)]
    public class IndexModel : BasePageModel<IndexModel>
    {
        public IEnumerable<AvailableUnitModel> AvailableUnits { get; set; } = new List<AvailableUnitModel>();

        public async Task<IActionResult> OnGet(string projectId, string[] selectedUnits, string? searchKey = null, DateTime? commencementDate = null)
        {
            AvailableUnits = await Mediatr.Send(new GetAvailableUnitQuery { ProjectId = projectId, SelectedUnits = selectedUnits, SearchKey = searchKey, CommencementDate = commencementDate });
            return Partial("_ViewAvailableUnits", AvailableUnits);
        }

        public async Task<IActionResult> OnGetFull(string projectId)
        {
            AvailableUnits = await Mediatr.Send(new GetAvailableUnitQuery { ProjectId = projectId });
            return Page();
        }
    }
}
