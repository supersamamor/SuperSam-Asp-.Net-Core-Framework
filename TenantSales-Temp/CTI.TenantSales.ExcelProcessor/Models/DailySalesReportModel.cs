using CTI.TenantSales.Core.TenantSales;
namespace CTI.TenantSales.ExcelProcessor.Models
{
    public class DailySalesReportModel
    {  
        public DailySalesReportModel(DateTime dateFrom, DateTime dateTo)
        {
            DateFrom = dateFrom;
            DateTo = dateTo;          
        }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
    }
}
