
namespace CTI.TenantSales.PdfGenerator.Models
{
    public class SalesGrowthDataModel
    {
        public SalesGrowthDataModel(IList<Theme> salesGrowthData)
        {
            this.Theme = salesGrowthData;
        }
        public IList<Theme> Theme { get; set; } = new List<Theme>();
    }
    public class Theme
    {
    }
}
