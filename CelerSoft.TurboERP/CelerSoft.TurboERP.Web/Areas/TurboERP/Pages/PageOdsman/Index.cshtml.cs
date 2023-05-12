using CelerSoft.TurboERP.Application.Features.TurboERP.WebContent.Queries;
using CelerSoft.TurboERP.Core.TurboERP;
using CelerSoft.TurboERP.Web;
using CelerSoft.TurboERP.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Html;


namespace CelerSoft.TurboERP.Web.Areas.TurboERP.Pages.PageOdsman
{
    [AllowAnonymous]
    public class IndexModel : BasePageModel<IndexModel>
    {
        public HtmlString? PageContent { get; set; }
        public string? PageTitle { get; set; }
        public async Task OnGet(string? pageName)
        {
            if (string.IsNullOrEmpty(pageName))
            {
                pageName = WebConstants.DEFAULT_PAGENAME;
            }
            WebContentState? webContent = new();
            (await Mediatr.Send(new GetWebContentByPageNameQuery(pageName))).Select(l => webContent = l);         
            PageContent = new HtmlString(webContent?.Content);
            PageTitle = webContent?.PageTitle;
        }
    }
}
