using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.ProjectNamePlaceHolder.MainModulePlaceHolder.Queries;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Areas.ProjectNamePlaceHolder.Models;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Models;
using DataTables.AspNetCore.Mvc.Binder;
using LanguageExt;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using static LanguageExt.Prelude;

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Areas.ProjectNamePlaceHolder.Pages.MainModulePlaceHolder
{
    [Authorize(Policy = Permission.MainModulePlaceHolder.View)]
    public class IndexModel : BasePageModel<IndexModel>
    {
        [DataTablesRequest]
        public DataTablesRequest? DataRequest { get; set; }

        [BindProperty]
        public MainModulePlaceHolderViewModel? MainModulePlaceHolder { get; set; }

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostListAllAsync()
        {
            var result = await Mediatr.Send(DataRequest!.ToQuery<GetMainModulePlaceHolderListQuery>());
            return new JsonResult(result.Data
                .Select(e => new
                {
                    e.Id,
					e.Code,

                    e.LastModifiedDate
                })
                .ToDataTablesResponse(DataRequest, result.TotalCount, result.MetaData.TotalItemCount));
        }
    }
}
