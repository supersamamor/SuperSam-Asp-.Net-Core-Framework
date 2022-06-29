using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Features.ProjectNamePlaceHolder.MainModulePlaceHolder.Queries;

using MediatR;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Service
{
    public class DropdownServices
    {
        private readonly IMediator _mediaTr;  
        public DropdownServices(IMediator mediaTr)
        {
            _mediaTr = mediaTr;       
        }      
		public async Task<IEnumerable<SelectListItem>> GetMainModulePlaceHolderList()
		{
			return (await _mediaTr.Send(new GetMainModulePlaceHolderQuery())).Data.Select(l => new SelectListItem { Value = l.Id, Text = l.Code });
		}
				
    }
}
