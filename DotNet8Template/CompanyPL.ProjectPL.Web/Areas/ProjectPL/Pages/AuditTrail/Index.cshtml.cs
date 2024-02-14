using CompanyPL.ProjectPL.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace CompanyPL.ProjectPL.Web.Areas.ProjectPL.Pages.AuditTrail;
[Authorize]
public class IndexModel : BasePageModel<IndexModel>
{
    public async Task<IActionResult> OnGetChangesHistory(string auditlogsid)
    {
        return Partial("_ChangesHistory");
    }
}
