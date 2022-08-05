namespace CTI.TenantSales.Web.Areas.TenantSales.Models
{
    public class TenantProfileNavigationModel
    {
        public TenantProfileNavigationModel(string tenantId, string activeButton,
             DateTime? dateFrom = null, DateTime? dateTo = null,
             string? salesCategory = null, string? posCode = null, DateTime? salesDate = null)
        {
            this.TenantId = tenantId;
            this.ActiveButton = activeButton;
            if (dateFrom != null && dateTo != null)
            {
                this.DateFrom = (DateTime)dateFrom;
                this.DateTo = (DateTime)dateTo;
            }         
            if (!string.IsNullOrEmpty(salesCategory)
                && !string.IsNullOrEmpty(posCode)
                && salesDate != null)
            {
                this.SelecteSalesCategoryDatePosCode = "Sales Category : " + salesCategory + " / POS Code : " + posCode + " / Sales Date : " + ((DateTime)salesDate).ToString("MM/dd/yyyy");
            }
        }
        public string TenantId { get; set; } = "";
        public string ActiveButton { get; set; } = "";
        public string SelecteSalesCategoryDatePosCode { get; set; } = "";
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
    }
}
