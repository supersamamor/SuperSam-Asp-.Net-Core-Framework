using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTI.TenantSales.ExcelProcessor.Models
{
    public class SalesSummaryForIFCAItem
    {
        public string TenantCode { get; set; } = "";
        public string ProjectCode { get; set; } = "";
        public string EntityCode { get; set; } = "";
        public DateTime SalesDate { get; set; }
        public string SalesCategory { get; set; } = "";
        public string CurrencyCode { get; set; } = "";
        public decimal? CurrencyRate { get; set; }
        public string Status { get; set; } = "";
        public DateTime? AuditDate { get; set; }
        public string AuditUser { get; set; } = "";
        public string TenantName { get; set; } = "";
        public decimal TotalMonthlySales { get; set; }
    }
}
