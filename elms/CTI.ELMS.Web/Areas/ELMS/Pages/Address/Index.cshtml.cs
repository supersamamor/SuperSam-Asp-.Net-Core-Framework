using CTI.ELMS.Web.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CTI.ELMS.Web.Areas.ELMS.Pages.Address
{
    [AllowAnonymous]
    public class IndexModel : PageModel
    {
        private readonly DropdownServices _dropdownService;
        public IndexModel(DropdownServices dropdownService)
        {
            _dropdownService = dropdownService;
        }

        public async Task<IActionResult> OnGetDropdownCitiesAsync(string province)
        {
            return await _dropdownService.GetCityList(province);
        }

        public async Task<IActionResult> OnGetDropdownBrgyAsync(string province, string city)
        {
            return await _dropdownService.GetBarangayList(province, city);
        }
    }
}
