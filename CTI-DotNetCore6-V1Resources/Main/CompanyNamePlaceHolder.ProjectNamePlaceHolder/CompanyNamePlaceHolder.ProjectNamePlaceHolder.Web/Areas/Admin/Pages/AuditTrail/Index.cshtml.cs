using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataTables.AspNetCore.Mvc.Binder;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Features.AuditTrail.Queries;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Areas.Admin.Models;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Models;

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Areas.Admin.Pages.AuditTrail
{
    [Authorize(Policy = Permission.AuditTrail.View)]
    public class IndexModel : BasePageModel<IndexModel>
    {
        [DataTablesRequest]
        public DataTablesRequest? DataRequest { get; set; }

        public AuditLogViewModel AuditLog { get; set; } = new();

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostListAllAsync()
        {
            var result = await Mediatr.Send(DataRequest!.ToQuery<GetAuditLogsQuery>());
            return new JsonResult(result.Data
                .Select(e => new
                {
                    e.Id,
                    e.DateTime,
                    TimeStamp = e.DateTime.ToString("R"),
                    e.PrimaryKey,
                    e.TableName,
                    e.Type,
                    e.UserId,
                })
                .ToDataTablesResponse(DataRequest, result.TotalCount, result.MetaData.TotalItemCount));
        }
    }
}