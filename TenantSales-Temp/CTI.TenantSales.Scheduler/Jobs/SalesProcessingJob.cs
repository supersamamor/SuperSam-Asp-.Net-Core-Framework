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
        }
        public async Task Execute(IJobExecutionContext context)
        {
            await ProcessSalesFile();
        }
        
        private async Task ProcessSalesFile()
        {
            var projectList = await _context.Project.Where(l => l.IsDisabled == false
                && l.Company!.IsDisabled == false
                && l.Company.DatabaseConnectionSetup!.IsDisabled == false).AsNoTracking().ToListAsync();
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
                                    TenantPOSState? pos = new();
                                    TenantState? tenant = new();
                                    using (FileStream? fileStream = File.OpenRead(fileDirectory))
                                    {
                                        var _streamReader = new StreamReader(fileStream);
                                        string _salesType = file.Name[..1];
                                        if (_disableHourly == true && _salesType == Core.Constants.SalesType.SALESTYPE_HOURLY) { continue; }
                                        using var processRepo = new ProcessingMethodFactory(_salesType);
                                        _tenantPOSSalesList = processRepo.ProcessingMethod!.ProcessSalesFile(_streamReader, fileDirectory);
                                        pos = await GetPOS(projectItem.Id, _tenantPOSSalesList.TenantCode, _tenantPOSSalesList.POSCode);
                                        tenant = pos?.Tenant;
                                        //Set CompanyId, ProjectCode, TenantId, TenantCode, TenantPOSCode, Pos ID of List
                                        _ = _tenantPOSSalesList.SalesList.Select(c => { c.ProjectId = projectItem.Id; return c; }).ToList();
                                        _ = _tenantPOSSalesList.SalesList.Select(c => { c.TenantId = pos!.TenantId; return c; }).ToList();
                                        _ = _tenantPOSSalesList.SalesList.Select(c => { c.TenantPOSId = pos!.Id; return c; }).ToList();
                                        //Validate Sales Category
                                        _tenantPOSSalesList = processRepo.ProcessingMethod.ValidateSalesCategory(tenant!.SalesCategoryList!.Select(l => l.Code).ToList()
                                            , _tenantPOSSalesList);
                                    }
                                    //Save Sales File
                                    var saveResult = await SavePosSales(_tenantPOSSalesList);
                                    //If Api Cannot Insert the sales file, Call Api for Updating 
                                    if (saveResult == false)
                                    {
                                        //Get Pos ID for Updating
                                        foreach (var item in _tenantPOSSalesList.SalesList)
                                        {
                                            var res = await GetPOSSalesId(pos!.Id, item.SalesType, item.SalesDate, item.SalesCategory);
                                            if (res != null)
                                            {
                                                item.Id = res.Id;
                                                item.ValidationStatus = res.ValidationStatus;
                                            }
                                        }
                                        //Update Sales File                                      
                                        await UpdateFailedPOSSales(_tenantPOSSalesList);
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
        private async Task<bool> SavePosSales(POSSales posSales)
        {
            foreach (var salesItem in posSales.SalesList)
            {
                await _context.AddAsync(salesItem);
            }          
            await _context.SaveChangesAsync();
            return true;
        }
        private async Task<bool> UpdateFailedPOSSales(POSSales posSales)
        {
            foreach (var salesItem in posSales.SalesList)
            {
                _context.Entry(salesItem).State = EntityState.Modified;               
            }        
            await _context.SaveChangesAsync();
            return true;
        }        
    }
}
