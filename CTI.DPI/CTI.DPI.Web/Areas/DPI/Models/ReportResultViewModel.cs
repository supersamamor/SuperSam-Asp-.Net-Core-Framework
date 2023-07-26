using CTI.DPI.Application.DTOs;

namespace CTI.DPI.Web.Areas.DPI.Models
{
    public class ReportResultViewModel
    {
        public string? ReportId { get; set; }
        public string? ReportName { get; set; }
        public string? Result { get; set; }
        public string ReportOrChartType { get; set; } = "";
        public IList<ReportQueryFilterViewModel> Filters { get; set; } = new List<ReportQueryFilterViewModel>();
    }
}
