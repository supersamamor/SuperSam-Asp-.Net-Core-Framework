using DataTables.AspNetCore.Mvc.Binder;
using LanguageExt;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Core;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Core.AreaPlaceHolder;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Infrastructure.Data;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Areas.AreaPlaceHolder.Models;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Common.Extensions;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using static LanguageExt.Prelude;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Features.AreaPlaceHolder.MainModulePlaceHolder.Queries;

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Areas.AreaPlaceHolder.Pages.MainModulePlaceHolder
{
    [Authorize(Policy = Permission.MainModulePlaceHolder.View)]
    public class IndexModel : BaseAreaPlaceHolderPageModel<IndexModel>
    {
        public MainModulePlaceHolderViewModel MainModulePlaceHolder { get; set; } = new();

        [DataTablesRequest]
        public DataTablesRequest? DataRequest { get; set; }

        [BindProperty]
        public FilterViewModel Filters { get; set; } = new();

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostListAllAsync()
        {
            var query = DataRequest!.ToQuery<GetMainModulePlaceHolderQuery>();
            query.Status = Filters.Status;
            query.Brand = Filters.Brand;
            var result = await Mediatr.Send(query);
            return new JsonResult(result.Data
                .Select(e => new
                {
                    e.Id,
                    e.Code,                 
                    e.LastModifiedDate
                })
                .ToDataTablesResponse(DataRequest, result.TotalCount, result.MetaData.TotalItemCount));
        }

        public async Task<IActionResult> OnGetData([FromQuery] GetMainModulePlaceHolderQuery query) =>
            new JsonResult(await Mediatr.Send(query));
    }

    public record FilterViewModel
    {
        [Display(Name = "Status")]
        public string Status { get; set; } = "";

        [Display(Name = "Brand")]
        public string Brand { get; set; } = "";
    }
}
