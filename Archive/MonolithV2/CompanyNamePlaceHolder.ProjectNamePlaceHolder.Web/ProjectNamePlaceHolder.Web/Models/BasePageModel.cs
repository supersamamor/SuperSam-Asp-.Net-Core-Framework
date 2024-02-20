using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ProjectNamePlaceHolder.Web.Models
{
    public class BasePageModel : PageModel
    {
        public BasePageModel(int defaultPage)
        {
            PageNumber = 1;
            DefaultPageSize = defaultPage;
        }
        #region Pagination         
        [BindProperty(SupportsGet = true)]
        public int PageNumber { get; set; }
        [BindProperty(SupportsGet = true)]
        public string OrderBy { get; set; }
        [BindProperty(SupportsGet = true)]
        public string SortBy { get; set; }
        private int DefaultPageSize { get; set; }
        public int PageSize
        {
            get
            {
                return DefaultPageSize;
            }

            set
            {
                DefaultPageSize = value > 0 ? value : DefaultPageSize;
            }
        }
        #endregion         
    }
}
