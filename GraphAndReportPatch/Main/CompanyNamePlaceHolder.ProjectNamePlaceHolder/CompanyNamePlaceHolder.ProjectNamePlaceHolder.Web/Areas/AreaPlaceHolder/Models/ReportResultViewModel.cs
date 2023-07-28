using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.DTOs;

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Areas.AreaPlaceHolder.Models
{
    public class ReportResultViewModel
    {
        public string? ReportId { get; set; }
        public string? ReportName { get; set; }
        public string? Results { get; set; }
        public string? Labels { get; set; }
        public string? Colors { get; set; }
        public string ReportOrChartType { get; set; } = "";
        public IList<ReportQueryFilterViewModel> Filters { get; set; } = new List<ReportQueryFilterViewModel>();
        public string? DomId
        {
            get { return this.ReportId?.Replace("-",""); }
        }
    }
}
