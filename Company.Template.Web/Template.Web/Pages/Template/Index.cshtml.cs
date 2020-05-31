using Correlate;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Threading;
using System.Threading.Tasks;
using Template.Web.ApiServices.Template;
using Template.Web.Extensions;
using Template.Web.Models;
using Template.Web.Models.Template;
using X.PagedList;

namespace Template.Web.Pages.Template
{
    public class IndexModel : BasePageModel
    {
        private readonly TemplateAPIService _service;
        private readonly ILogger _logger;
        private readonly ICorrelationContextAccessor _correlationContext;

        public IndexModel(TemplateAPIService service, IOptions<TemplateWebConfig> appSetting, 
            ILogger<EditModel> logger, ICorrelationContextAccessor correlationContext) : base(appSetting.Value.PageSize)
        {
            _service = service;
            _logger = logger;
            _correlationContext = correlationContext;
        }

        public IPagedList<TemplateModel> TemplateList { get;set; }
        [BindProperty(SupportsGet = true)]
        public string SearchKey { get; set; }

        public async Task OnGetAsync()
        {           
            try
            {
                await GetTemplateListAsync();
            }
            catch (Exception ex)
            {
                TempData["Error"] = _logger.CustomErrorLogger(ex, _correlationContext, nameof(OnGetAsync));
            }               
        }

        private async Task GetTemplateListAsync() {
            var paginatedTemplate = await _service.GetTemplateListAsync(SearchKey,OrderBy, SortBy, PageNumber, PageSize, new CancellationToken());
            TemplateList = paginatedTemplate;
        }
    }
}
