using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Areas.Admin.Models;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Areas.Admin.Queries.Users;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Models;
using DataTables.AspNetCore.Mvc.Binder;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Areas.Admin.Pages.Users
{
    [Authorize(Policy = Permission.Users.View)]
    public class IndexModel : BasePageModel<IndexModel>
    {
        [DataTablesRequest]
        public DataTablesRequest? DataRequest { get; set; }

        public UserViewModel? UserModel { get; set; }

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostListAllAsync()
        {
            var result = await Mediatr.Send(DataRequest!.ToQuery<GetUsersQuery>());
            return new JsonResult(result.Data
                .Select(e => new
                {
                    e.Id,
                    e.Name,
                    e.Email,
                    e.IsActive
                })
                .ToDataTablesResponse(DataRequest, result.TotalCount, result.MetaData.TotalItemCount));
        }
    }

    public record UserViewModel
    {
        public string? Id { get; set; } = Guid.NewGuid().ToString();
        public string? Name { get; set; }
        public string? Email { get; set; }
        [Display(Name = "Status")]
        public bool IsActive { get; set; } = false;
    }
}
