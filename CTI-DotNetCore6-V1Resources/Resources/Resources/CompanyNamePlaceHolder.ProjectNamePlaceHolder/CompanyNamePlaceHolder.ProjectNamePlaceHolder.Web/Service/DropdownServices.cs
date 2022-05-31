Template:[InsertNewImportQueryFromDropdownService]
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
		Template:[InsertNewGetDropdownMethodFromDropdownService]
    }
}
