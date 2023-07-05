using Microsoft.AspNetCore.Mvc.Rendering;
using CNPlaceHolder.PNPlaceHolder.Infrastructure.Data;
using CNPlaceHolder.PNPlaceHolder.Core.PNPlaceHolder;
using CNPlaceHolder.Common.Data;
using CNPlaceHolder.PNPlaceHolder.Web.Areas.Admin.Queries.Users;
using MediatR;
using CNPlaceHolder.PNPlaceHolder.Web.Areas.Admin.Queries.Roles;

namespace CNPlaceHolder.PNPlaceHolder.Web.Service
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
		
		
    }
}
