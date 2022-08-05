using System.ComponentModel.DataAnnotations;

namespace CTI.TenantSales.Web.Areas.Sales.Models
{
    public class TenantSalesModel
    {
        public DateTime DateFrom { get; set; } = DateTime.Now.Date;
        public DateTime DateTo { get; set; } = DateTime.Now.Date;
        [Required]
        [Display(Name = "Project")]
        public string? ProjectId { get; set; }
        public string? LevelId { get; set; }
        public string? TenantId { get; set; }
        public string? SalesCategoryCode { get; set; }
        public string? FilePath { get; set; }
    }
}
