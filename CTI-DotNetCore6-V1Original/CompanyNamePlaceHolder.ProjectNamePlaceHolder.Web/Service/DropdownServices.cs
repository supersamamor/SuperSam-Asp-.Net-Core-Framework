using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Features.ProjectNamePlaceHolder.ParentModule.Queries;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Features.ProjectNamePlaceHolder.MainModule.Queries;

using MediatR;
using Microsoft.AspNetCore.Mvc.Rendering;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Areas.Admin.Queries.Users;

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Service
{
    public class DropdownServices
    {
        private readonly IMediator _mediaTr;  
        public DropdownServices(IMediator mediaTr)
        {
            _mediaTr = mediaTr;       
        }      
		public async Task<IEnumerable<SelectListItem>> GetParentModuleList()
		{
			return (await _mediaTr.Send(new GetParentModuleQuery())).Data.Select(l => new SelectListItem { Value = l.Id, Text = l.Name });
		}
		public async Task<IEnumerable<SelectListItem>> GetMainModuleList()
		{
			return (await _mediaTr.Send(new GetMainModuleQuery())).Data.Select(l => new SelectListItem { Value = l.Id, Text = l.Id });
		}
		
		public async Task<IEnumerable<SelectListItem>> GetUserList(string currentSelectedApprover, IList<string> allSelectedApprovers)
		{
			return (await _mediaTr.Send(new GetApproversQuery(currentSelectedApprover, allSelectedApprovers))).Data.Select(l => new SelectListItem { Value = l.Id, Text = l.Name });
		}
    }
}
