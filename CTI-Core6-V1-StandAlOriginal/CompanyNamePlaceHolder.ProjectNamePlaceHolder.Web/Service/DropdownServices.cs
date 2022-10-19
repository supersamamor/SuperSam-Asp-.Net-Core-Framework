using Microsoft.AspNetCore.Mvc.Rendering;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Infrastructure.Data;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Core.AreaPlaceHolder;
using CompanyNamePlaceHolder.Common.Data;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Areas.Admin.Queries.Users;
using MediatR;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Areas.Admin.Queries.Roles;

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Service
{
    public class DropdownServices
    {
		private readonly ApplicationContext _context;
		private readonly IMediator _mediaTr;

		public DropdownServices(ApplicationContext context, IMediator mediaTr)
		{            
			_context = context;
			_mediaTr = mediaTr;
		}       
		public SelectList GetParentModuleList(string id)
		{
			return _context.GetSingle<ParentModuleState>(e => e.Id == id, new()).Result.Match(
				Some: e => new SelectList(new List<SelectListItem> { new() { Value = e.Id, Text = e.Name } }, "Value", "Text", e.Id),
				None: () => new SelectList(new List<SelectListItem>(), "Value", "Text")
			);
		}
		public SelectList GetMainModuleList(string id)
		{
			return _context.GetSingle<MainModuleState>(e => e.Id == id, new()).Result.Match(
				Some: e => new SelectList(new List<SelectListItem> { new() { Value = e.Id, Text = e.Id } }, "Value", "Text", e.Id),
				None: () => new SelectList(new List<SelectListItem>(), "Value", "Text")
			);
		}
		
		public async Task<IEnumerable<SelectListItem>> GetUserList(string currentSelectedApprover, IList<string> allSelectedApprovers)
		{
			return (await _mediaTr.Send(new GetApproversQuery(currentSelectedApprover, allSelectedApprovers))).Data.Select(l => new SelectListItem { Value = l.Id, Text = l.Name });
		}
		public async Task<IEnumerable<SelectListItem>> GetRoleApproverList(string currentSelectedApprover, IList<string> allSelectedApprovers)
		{
			return (await _mediaTr.Send(new GetApproverRolesQuery(currentSelectedApprover, allSelectedApprovers))).Data.Select(l => new SelectListItem { Value = l.Id, Text = l.Name });
		}
	}
}
