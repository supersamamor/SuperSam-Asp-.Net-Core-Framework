using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Template.Web.Models
{
    public class BasePageModel : PageModel
    {
        public BasePageModel(int defaultPage)
        {
            PageNumber = 1;
            defaultPageSize = defaultPage;
        }
        #region Pagination         
        [BindProperty(SupportsGet = true)]
        public int PageNumber { get; set; }
        [BindProperty(SupportsGet = true)]
        public string OrderBy { get; set; }
        [BindProperty(SupportsGet = true)]
        public string SortBy { get; set; }
        private int defaultPageSize { get; set; }
        public int PageSize
        {
            get
            {
                return defaultPageSize;
            }

            set
            {
                defaultPageSize = value > 0 ? value : defaultPageSize;
            }
        }
        #endregion         
    }
}
