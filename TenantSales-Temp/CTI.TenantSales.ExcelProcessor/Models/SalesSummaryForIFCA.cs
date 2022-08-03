using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTI.TenantSales.ExcelProcessor.Models
{
    public class SalesSummaryForIFCA
    {
        public SalesSummaryForIFCA( string databaseCompanyProjectRoute, DateTime dateFrom, DateTime dateTo)
        {
            DatabaseCompanyProjectRoute = databaseCompanyProjectRoute;
            DateFrom = dateFrom;
            DateTo = dateTo;        
        }
        public string DatabaseCompanyProjectRoute { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }      
    }
}
