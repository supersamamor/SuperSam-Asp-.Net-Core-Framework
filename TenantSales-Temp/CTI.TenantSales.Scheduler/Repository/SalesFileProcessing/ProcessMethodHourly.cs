using CTI.TenantSales.Scheduler.Models;

namespace CTI.TenantSales.Scheduler.Repository.SalesFileProcessing
{
    public class ProcessMethodHourly : IProcessingMethod
    {
        public POSSales ProcessSalesFile(StreamReader _fileStream, string fileName)
        {
            POSSales posSales = new();
            SalesItem _tenantPOSSales = new();
            decimal validateSalesAmount = 0;
            decimal validateNoOfSalesTransactions = 0;
            DateTime? salesDate = null;
            while (!(_fileStream.EndOfStream))
            {
                var _string = _fileStream.ReadLine();
                string _fileRowString = "";
                if (string.IsNullOrEmpty(_string))
                {
                    _fileRowString = "0";
                    break;
                }
                else
                {
                    _fileRowString = _string[..2];
                }
                var _fileRowNo = 0;
                try
                {
                    _fileRowNo = Convert.ToInt32(_fileRowString);
                }
                catch
                {
                    throw new InvalidOperationException("");
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
                        salesDate = new DateTime(Convert.ToInt32(_string.Substring(6, 4)),
                                                               Convert.ToInt32(_string.Substring(2, 2)),
                                                               Convert.ToInt32(_string.Substring(4, 2)));
                        _tenantPOSSales.SalesDate = (DateTime)salesDate;
                        break;
                    case 4:
                        //New Instance
                        _tenantPOSSales = new SalesItem { };
                        _tenantPOSSales.HourCode = Convert.ToInt32(_string[2..]);
                        break;
                    case 5:
                        _tenantPOSSales.SalesAmount = Convert.ToDecimal(_string[2..]) / 100;
                        break;
                    case 6:
                        //Add
                        _tenantPOSSales.NoOfSalesTransactions = Convert.ToDecimal(_string[2..]);
                        posSales.SalesList.Add(_tenantPOSSales);
                        break;
                    case 7:
                        validateSalesAmount = Convert.ToDecimal(_string[2..]) / 100;
                        break;
                    case 8:
                        validateNoOfSalesTransactions = Convert.ToDecimal(_string[2..]) / 100;
                        break;
                    default:
                        break;
                }
            }
            posSales.SalesList.ToList().ForEach(c => c.FileName = fileName);
            posSales.SalesList.ToList().ForEach(c => c.SalesType = Convert.ToInt32(Core.Constants.SalesTypeEnum.Hourly));
            posSales.SalesList.ToList().ForEach(c => c.SalesDate = (DateTime)salesDate!);
            posSales.SalesList.ToList().ForEach(c => c.IsAutoCompute = false);
            posSales.SalesList.ToList().ForEach(c => c.ValidationStatus = Convert.ToInt32(Core.Constants.ValidationStatusEnum.Passed));
            posSales.SalesList.ToList().ForEach(c => c.ValidationRemarks = null);
            //posSales.SalesList.ToList().ForEach(c => c.ValidateSalesAmount = validateSalesAmount);
            //posSales.SalesList.ToList().ForEach(c => c.ValidateNoOfSalesTransactions = validateNoOfSalesTransactions);
            return posSales;
        }
        public POSSales ValidateSalesCategory(IList<string> tenantSalesCategoryList, POSSales salesList) { return salesList; }
    }
}
