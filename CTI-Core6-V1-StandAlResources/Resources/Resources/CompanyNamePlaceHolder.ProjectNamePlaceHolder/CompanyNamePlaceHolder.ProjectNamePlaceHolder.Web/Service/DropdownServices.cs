using Microsoft.AspNetCore.Mvc.Rendering;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Infrastructure.Data;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Core.AreaPlaceHolder;
using CompanyNamePlaceHolder.Common.Data;

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Service
{
    public class DropdownServices
    {
        private readonly ApplicationContext _context;
        public DropdownServices( ApplicationContext context)
        {            
            _context = context;
        }     
		Template:[InsertNewGetDropdownMethodFromDropdownService]
		Template:[ApprovalUserListDropdown]
    }
}
