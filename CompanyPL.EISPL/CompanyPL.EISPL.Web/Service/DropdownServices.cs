using Microsoft.AspNetCore.Mvc.Rendering;
using CompanyPL.EISPL.Infrastructure.Data;
using CompanyPL.EISPL.Core.EISPL;
using CompanyPL.Common.Data;
using CompanyPL.EISPL.Web.Areas.Admin.Queries.Users;
using MediatR;
using CompanyPL.EISPL.Web.Areas.Admin.Queries.Roles;

namespace CompanyPL.EISPL.Web.Service
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
		public SelectList GetPLEmployeeList(string? id)
		{
			return _context.GetSingle<PLEmployeeState>(e => e.Id == id, new()).Result.Match(
				Some: e => new SelectList(new List<SelectListItem> { new() { Value = e.Id, Text = e.PLEmployeeCode } }, "Value", "Text", e.Id),
				None: () => new SelectList(new List<SelectListItem>(), "Value", "Text")
			);
		}
		
		
    }
}
