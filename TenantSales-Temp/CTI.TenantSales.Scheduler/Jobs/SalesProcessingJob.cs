using CTI.TenantSales.Core.Constants;
using CTI.TenantSales.Core.TenantSales;
using CTI.TenantSales.Infrastructure.Data;
using CTI.TenantSales.Scheduler.Helper;
using CTI.TenantSales.Scheduler.Models;
using CTI.TenantSales.Scheduler.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Quartz;

namespace CTI.TenantSales.Scheduler.Jobs
{
    public class SalesProcessingJob : IJob
    {
        private readonly ApplicationContext _context;
        private readonly ILogger<ApprovalNotificationJob> _logger;
        private readonly SalesFileHelper _salesFileHelper;
        private readonly string _salesUploadBasePath;
        private readonly string _salesUploadSuccessPath;
        private readonly string _salesUploadErrorPath;
        private readonly bool _disableHourly;
        private readonly int _cutOffFrom;
        private readonly int _cutOffTo;
        public SalesProcessingJob(ApplicationContext context, ILogger<ApprovalNotificationJob> logger, SalesFileHelper salesFileHelper,
            IConfiguration configuration)
        {
            _context = context;
            _logger = logger;
            _salesFileHelper = salesFileHelper;
            _salesUploadBasePath = configuration.GetValue<string>("SalesUploadPath:Base");
            _salesUploadSuccessPath = configuration.GetValue<string>("SalesUploadPath:SuccessPath");
            _salesUploadErrorPath = configuration.GetValue<string>("SalesUploadPath:ErrorPath");
            _disableHourly = configuration.GetValue<bool>("DisableHourly");
            _cutOffFrom = configuration.GetValue<int>("CutOff:From");
            _cutOffTo = configuration.GetValue<int>("CutOff:To");
        }
        public async Task Execute(IJobExecutionContext context)
        {
            await ProcessSalesFile();
        }

        private async Task ProcessSalesFile()
        {
            var projectList = await GetProjectList();
            if (projectList != null)
            {
                foreach (var projectItem in projectList)
                {
                    if (projectItem.SalesUploadFolder != null)
                    {
                        // Get all subdirectories
                        string[] subdirectoryEntries = { "" };
                        System.IO.Directory.CreateDirectory(_salesUploadBasePath + @"\" + projectItem.SalesUploadFolder);
                        subdirectoryEntries = Directory.GetDirectories(_salesUploadBasePath + @"\" + projectItem.SalesUploadFolder);
                        // Loop through them to see if they have any other subdirectories
                        foreach (string subdirectory in subdirectoryEntries)
                        {
                            FileInfo[] Files = SalesFileHelper.GetSalesFile(subdirectory);
                            POSSales _tenantPOSSalesList = new();
                            foreach (FileInfo file in Files)
                            {
                                string fileDirectory = file.DirectoryName + @"\" + file.Name;
                                string fileCode = "";
                                string fileName = "";
                                try
                                {
                                    fileCode = file.Name.Substring(1, 4);
                                    fileName = file.Name;
                                    string _salesType = file.Name[..1];
                                    TenantPOSState? pos = new();
                                    TenantState? tenant = new();
                                    using (FileStream? fileStream = File.OpenRead(fileDirectory))
                                    {
                                        var _streamReader = new StreamReader(fileStream);
                                        if (_disableHourly == true && _salesType == Core.Constants.SalesType.SALESTYPE_HOURLY) { continue; }
                                        using var processRepo = new ProcessingMethodFactory(_salesType);
                                        _tenantPOSSalesList = processRepo.ProcessingMethod!.ProcessSalesFile(_streamReader, fileDirectory);
                                        pos = await GetPOS(projectItem.Id, _tenantPOSSalesList.TenantCode, _tenantPOSSalesList.POSCode);
                                        tenant = pos?.Tenant;
                                        //Set CompanyId, ProjectCode, TenantId, TenantCode, TenantPOSCode, Pos ID of List                          
                                        _ = _tenantPOSSalesList.SalesList.Select(c => { c.TenantPOSId = pos!.Id; return c; }).ToList();
                                        //Validate Sales Category
                                        _tenantPOSSalesList = processRepo.ProcessingMethod.ValidateSalesCategory(tenant!.SalesCategoryList!.Select(l => l.Code).ToList()
                                            , _tenantPOSSalesList);
                                    }
                                    //Save Sales File
                                    var existingSales = GetPOSSales(pos!.Id, SalesType.SalesTypeToInt(_salesType), _tenantPOSSalesList.SalesList.FirstOrDefault()!.SalesDate, _tenantPOSSalesList.SalesList.FirstOrDefault()!.SalesCategory!);
                                    if (existingSales != null)
                                    {
                                        await UpdateFailedPOSSales(_tenantPOSSalesList);
                                    }
                                    else
                                    {
                                        await SavePosSales(_tenantPOSSalesList);
                                    }
                                    //Move To Success			
                                    _salesFileHelper.MoveSalesFile(fileDirectory, _salesUploadBasePath
                                                                    + @"\" + _salesUploadSuccessPath
                                                                    + @"\" + projectItem.SalesUploadFolder
                                                                    + @"\" + fileCode
                                                                    , @"\" + fileName);
                                }
                                catch (Exception ex)
                                {
                                    _logger.LogError(ex, "FileName : {FileDirectory} - Exception : {Message}", fileDirectory, ex.Message.ToString());
                                    //Move To Error	
                                    _salesFileHelper.MoveSalesFile(fileDirectory, _salesUploadBasePath
                                                                    + @"\" + _salesUploadErrorPath
                                                                    + @"\" + projectItem.SalesUploadFolder
                                                                    + @"\" + fileCode
                                                                    , fileName);
                                    System.Console.WriteLine("Failed saving the file from : " + fileDirectory + "/Exception : " + ex.Message.ToString());
                                }
                            }
                        }
                    }
                    else
                    {
                        _logger.LogInformation("Information: Project folder not found({SalesUploadFolder})", projectItem.SalesUploadFolder);
                        System.Console.WriteLine("Date : " + DateTime.Now + " / Project folder not found(" + projectItem.SalesUploadFolder + ")");
                    }
                }
            }
        }

        private async Task ValidateDailyTenantSales(DateTime? _salesDate, string? tenantCode)
        {
            var dateToValidate = DateTime.Now.Date.AddDays(-1);
            DateTime? dateFrom = null;
            DateTime? dateTo = null;
            if (_salesDate != null)
            {
                dateToValidate = (DateTime)_salesDate;
            }
            if (dateFrom == null)
            {
                dateFrom = new DateTime(dateToValidate.AddMonths(-1).Year, dateToValidate.AddMonths(-1).Month, _cutOffFrom);
            }
            if (dateTo == null)
            {
                dateTo = new DateTime(dateToValidate.Year, dateToValidate.Month, _cutOffTo);
            }
            var salesType = Convert.ToInt32(SalesTypeEnum.Daily);
            //Get Active Projects
            var projectList = await GetProjectList();
            if (projectList != null)
            {
                foreach (var projectItem in projectList)
                {
                    //Get Active Tenant Per Projects   
                    var activeTenants = await GetActiveTenants(projectItem.Id, tenantCode);
                    if (activeTenants != null)
                    {
                        foreach (var tenantItem in activeTenants)
                        {
                            if (tenantItem?.TenantPOSList?.Count > 0 && tenantItem?.SalesCategoryList?.Count > 0)
                            {
                                //Fetch only active POS
                                foreach (var posItem in tenantItem.TenantPOSList!.Where(l => l.IsDisabled == false))
                                {
                                    //Fetch only active sales category
                                    foreach (var salesCategoryCode in tenantItem!.SalesCategoryList!.Where(l => l.IsDisabled == false))
                                    {
                                        var salesItem = GetPOSSales(posItem.Id, salesType, dateToValidate, salesCategoryCode!.Code);
                                        //If no existing Sales for the end of the day, insert a sale with Zero, Null and Default values..
                                        if (salesItem == null)
                                        {
                                            TenantPOSSalesState saleItem = new()
                                            {
                                                SalesType = salesType,
                                                HourCode = 0,
                                                SalesCategory = salesCategoryCode!.Code,
                                                SalesDate = dateToValidate,
                                                IsAutoCompute = false,
                                                SalesAmount = 0,
                                                OldAccumulatedTotal = 0,
                                                NewAccumulatedTotal = 0,
                                                TaxableSalesAmount = 0,
                                                NonTaxableSalesAmount = 0,
                                                SeniorCitizenDiscount = 0,
                                                PromoDiscount = 0,
                                                OtherDiscount = 0,
                                                RefundDiscount = 0,
                                                VoidAmount = 0,
                                                AdjustmentAmount = 0,
                                                TotalServiceCharge = 0,
                                                TotalTax = 0,
                                                NoOfSalesTransactions = 0,
                                                NoOfTransactions = 0,
                                                TotalNetSales = 0,
                                                ControlNumber = 0,
                                                FileName = null,
                                                ValidationStatus = Convert.ToInt32(ValidationStatusEnum.Failed),
                                                ValidationRemarks = "No submitted sales file.,Sales amount is zero. If this is fine please tick the 'Manually Checked' checkbox otherwise put a value on sales amount.",
                                                TenantPOSId = posItem.Id,
                                            };
                                            POSSales _salesList = new();
                                            _salesList.SalesList.Add(saleItem);
                                            await SavePosSales(_salesList);
                                        }
                                    }
                                }
                            }
                        }
                    }
                    //Get All Failed Day Sales Per Projects order by date for revalidation
                    var failedSalesList = await GetPOSDailySales(projectItem.Id, dateFrom, dateTo, tenantCode, ValidationStatusEnum.Failed);
                    if (failedSalesList != null)
                    {
                        foreach (var failedSalesItem in failedSalesList)
                        {
                            POSSales salesToValidate = new();
                            salesToValidate.SalesList = new List<TenantPOSSalesState>
                            {
                                failedSalesItem
                            };
                            try
                            {
                                await UpdateFailedPOSSales(salesToValidate);
                            }
                            catch (Exception ex)
                            {
                                _logger.LogInformation("Failed validating the sales. Sales Id : {SalesItemId} / Folder : {Folder} / Exception : {ExceptionMessage}", failedSalesItem.Id, projectItem.SalesUploadFolder, ex.Message.ToString());
                            }
                        }
                    }
                }
            }
        }
        private async Task<IList<ProjectState>> GetProjectList()
        {
            return await _context.Project.Where(l => l.IsDisabled == false
                   && l.Company!.IsDisabled == false
                   && l.Company.DatabaseConnectionSetup!.IsDisabled == false).AsNoTracking().ToListAsync();
        }
        private async Task<TenantPOSState?> GetPOS(string projectId, string tenantCode, string posCode)
        {
            return await _context.TenantPOS.Where(l => l.Tenant!.ProjectId == projectId
                    && l.Tenant.Code == tenantCode
                    && l.Code == posCode).Include(l => l.Tenant).AsNoTracking().FirstOrDefaultAsync();
        }
        private async Task<TenantPOSSalesState?> GetPOSSalesId(string posId, int salesType, DateTime salesDate, string salesCategory)
        {
            return await _context.TenantPOSSales.Where(l => l.TenantPOSId == posId
                    && l.SalesType == salesType
                    && l.SalesDate == salesDate
                    && l.SalesCategory == salesCategory).AsNoTracking().FirstOrDefaultAsync();
        }

        private async Task<IList<TenantState>> GetActiveTenants(string projectId, string? tenantCode)
        {
            var query = _context.Tenant.Where(l => l.ProjectId == projectId && l.IsDisabled == false).AsNoTracking();
            if (!string.IsNullOrEmpty(tenantCode))
            {
                query = query.Where(l => l.Code == tenantCode);
            }
            return await query.Include(l => l.TenantPOSList).Include(l => l.SalesCategoryList).ToListAsync();
        }

        private async Task<TenantPOSSalesState?> GetPOSSales(string posId, int salesType, DateTime dateToValidate, string catCode)
        {
            return await _context.TenantPOSSales.Where(l =>
                 l.TenantPOSId == posId && l.SalesType == salesType && l.SalesDate == dateToValidate && l.SalesCategory == catCode)
                 .AsNoTracking().FirstOrDefaultAsync();
        }
        private async Task<IList<TenantPOSSalesState>> GetPOSDailySales(string? projectId, DateTime? dateFrom, DateTime? dateTo, string? tenantCode, ValidationStatusEnum? validationStatus)
        {
            var query = _context.TenantPOSSales.AsNoTracking();
            if (validationStatus != null)
            {
                query = query.Where(l => l.ValidationStatus == Convert.ToInt32(ValidationStatusEnum.Failed));
            }
            if (!string.IsNullOrEmpty(projectId))
            {
                query = query.Where(l => l.TenantPOS!.Tenant!.ProjectId == projectId);
            }
            if (dateFrom != null && dateFrom != Convert.ToDateTime(DateTime.MinValue))
            {
                query = query.Where(l => l.SalesDate >= dateFrom);
            }
            if (dateTo != null && dateTo != Convert.ToDateTime(DateTime.MinValue))
            {
                query = query.Where(l => l.SalesDate <= dateTo);
            }
            if (!string.IsNullOrEmpty(tenantCode))
            {
                query = query.Where(l => l.TenantPOS!.Tenant!.Code == tenantCode);
            }
            return await query.ToListAsync();
        }
        private async Task SavePosSales(POSSales posSales)
        {
            foreach (var salesItem in posSales.SalesList)
            {
                await _context.AddAsync(salesItem);
            }
            await _context.SaveChangesAsync();
        }
        private async Task UpdateFailedPOSSales(POSSales posSales)
        {
            //Get Pos ID for Updating
            foreach (var item in posSales.SalesList)
            {
                var res = await GetPOSSalesId(item!.TenantPOSId, item.SalesType, item.SalesDate, item!.SalesCategory!);
                if (res != null)
                {
                    res.UpdateFrom(item);
                    _context.Entry(res).State = EntityState.Modified;
                }
            }
            await _context.SaveChangesAsync();
        }
    }
}
