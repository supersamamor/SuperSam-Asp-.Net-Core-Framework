using CTI.TenantSales.Scheduler.Models;
namespace CTI.TenantSales.Scheduler.Repository.SalesFileProcessing
{
    public interface IProcessingMethod
    {
        POSSales ProcessSalesFile(StreamReader _fileStream, string fileName);
        POSSales ValidateSalesCategory(IList<string> tenantSalesCategoryList, POSSales salesList);
    }
}
