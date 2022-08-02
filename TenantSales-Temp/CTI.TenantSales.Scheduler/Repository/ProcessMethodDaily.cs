using CTI.TenantSales.Core.TenantSales;
using CTI.TenantSales.Scheduler.Models;
namespace CTI.TenantSales.Scheduler.Repository
{
    public class ProcessMethodDaily : IProcessingMethod
    {
        public POSSales ProcessSalesFile(StreamReader _fileStream, string fileName)
        {
            POSSales posSales = new();
            TenantPOSSalesState _tenantPOSSales = new() { };
            _tenantPOSSales.SalesAmount = 0;
            _tenantPOSSales.IsAutoCompute = false;
            _tenantPOSSales.FileName = fileName;
            _tenantPOSSales.SalesType = Convert.ToInt32(Core.Constants.SalesTypeEnum.Daily);
            _tenantPOSSales.ValidationStatus = Convert.ToInt32(Core.Constants.ValidationStatusEnum.Passed);
            _tenantPOSSales.ValidationRemarks = null;
            while (!(_fileStream.EndOfStream))
            {
                string _string = _fileStream!.ReadLine()!;
                string _fileRowString = "0";
                if (!string.IsNullOrEmpty(_string))
                {
                    _fileRowString = _string[..2];                  
                }
                int _fileRowNo;
                try
                {
                    _fileRowNo = Convert.ToInt32(_fileRowString);
                }
                catch
                {
                    throw new InvalidOperationException("Row number is invalid.");
                }
                switch (_fileRowNo)
                {
                    case 1:
                        posSales.TenantCode = _string[2..].Trim();
                        break;
                    case 2:
                        posSales.POSCode = _string[2..].Trim();
                        break;
                    case 3:
                        _tenantPOSSales.SalesDate = new DateTime(Convert.ToInt32(_string!.Substring(6, 4)),
                                                                Convert.ToInt32(_string.Substring(2, 2)),
                                                                Convert.ToInt32(_string.Substring(4, 2)));
                        break;
                    case 4:
                        _tenantPOSSales.OldAccumulatedTotal = Convert.ToDecimal(_string![2..]) / 100;
                        break;
                    case 5:
                        _tenantPOSSales.NewAccumulatedTotal = Convert.ToDecimal(_string![2..]) / 100;
                        break;
                    case 6:
                        _tenantPOSSales.TaxableSalesAmount = Convert.ToDecimal(_string![2..]) / 100;
                        break;
                    case 7:
                        _tenantPOSSales.NonTaxableSalesAmount = Convert.ToDecimal(_string![2..]) / 100;
                        break;
                    case 8:
                        _tenantPOSSales.SeniorCitizenDiscount = Convert.ToDecimal(_string![2..]) / 100;
                        break;
                    case 9:
                        _tenantPOSSales.PromoDiscount = Convert.ToDecimal(_string![2..]) / 100;
                        break;
                    case 10:
                        _tenantPOSSales.OtherDiscount = Convert.ToDecimal(_string![2..]) / 100;
                        break;
                    case 11:
                        _tenantPOSSales.RefundDiscount = Convert.ToDecimal(_string![2..]) / 100;
                        break;
                    case 12:
                        _tenantPOSSales.VoidAmount = Convert.ToDecimal(_string![2..]) / 100;
                        break;
                    case 13:
                        _tenantPOSSales.ControlNumber = Convert.ToInt32(_string![2..]);
                        break;
                    case 14:
                        _tenantPOSSales.NoOfSalesTransactions = Convert.ToDecimal(_string![2..]);
                        break;
                    case 15:
                        _tenantPOSSales.NoOfTransactions = Convert.ToDecimal(_string![2..]);
                        break;
                    case 16:
                        _tenantPOSSales.SalesCategory = _string![2..].Trim();
                        break;
                    case 17:
                        _tenantPOSSales.TotalNetSales = Convert.ToDecimal(_string![2..]) / 100;
                        break;
                    case 18:
                        _tenantPOSSales.TotalTax = Convert.ToDecimal(_string![2..]) / 100;
                        break;
                    case 19:
                        _tenantPOSSales.TotalServiceCharge = Convert.ToDecimal(_string![2..]) / 100;
                        break;
                    case 20:
                        _tenantPOSSales.AdjustmentAmount = Convert.ToDecimal(_string![2..]) / 100;
                        break;
                    default:
                        break;
                }
            }
            posSales.SalesList.Add(_tenantPOSSales);
            return posSales;
        }
        public POSSales ValidateSalesCategory(IList<string> tenantSalesCategoryList, POSSales salesList)
        {
            if (!tenantSalesCategoryList.Contains(salesList.SalesList.FirstOrDefault()!.SalesCategory!))
            {
                throw new Exception("Sales category is not valid!");
            }
            return salesList;
        }
    }
}
