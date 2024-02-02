using CompanyPL.Common.Services.Shared.Interfaces;
using CompanyPL.Common.Services.Shared.Models.Mail;
using CompanyPL.ProjectPL.Core.ProjectPL;
using CompanyPL.ProjectPL.Infrastructure.Data;
using CompanyPL.ProjectPL.ExcelProcessor.Services;
using LanguageExt;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Quartz;


namespace CompanyPL.ProjectPL.Scheduler.Jobs
{
    [DisallowConcurrentExecution]
    public class BatchUploadJob : IJob
    {
        private readonly ApplicationContext _context;
		private readonly ILogger<BatchUploadJob> _logger;
		private readonly string? _uploadPath;
		private readonly IMailService _emailSender;
		private readonly ExcelService _excelService;
        private readonly IdentityContext _identityContext;
        public BatchUploadJob(ApplicationContext context, ILogger<BatchUploadJob> logger, IConfiguration configuration, IMailService emailSender, ExcelService excelService, IdentityContext identityContext)
		{
			_context = context;
			_logger = logger;
			_uploadPath = configuration.GetValue<string>("UsersUpload:UploadFilesPath");
			_emailSender = emailSender;
			_excelService = excelService;
            _identityContext = identityContext;

        }
        public async Task Execute(IJobExecutionContext context)
        {
            await ProcessBatchUploadAsync();
        }
        private async Task ProcessBatchUploadAsync()
        {
            var uploadProcessorList = await _context.UploadProcessor.Where(l => l.Status == Core.Constants.FileUploadStatus.Pending).IgnoreQueryFilters().AsNoTracking()
                .OrderBy(l => l.CreatedDate).ToListAsync();
            var exceptionFilePath = "";
            foreach (var item in uploadProcessorList)
            {
                try
                {
                    //Tag Start Date/Time
                    item.SetStart();
                    _context.Update(item);
                    await _context.UpdateBatchRecordAsync(item);
                    //Start Processing
                    exceptionFilePath = await ValidateBatchUpload(item.Module, item.Path, item.CreatedBy!);
                    if (string.IsNullOrEmpty(exceptionFilePath))
                    {
                        item.SetDone();
                    }
                    else
                    {
                        item.SetFailed(exceptionFilePath, "Error from the file.");
                    }
                    _context.Update(item);
                    await _context.UpdateBatchRecordAsync(item);
                }
                catch (Exception ex)
                {
					_context.DetachAllTrackedEntities();
                    _logger.LogError(ex, @"ProcessBatchUploadAsync Error Message : {Message} / StackTrace : {StackTrace}", ex.Message, ex.StackTrace);
                    item.SetFailed("", ex.Message);
                    _context.Update(item);
                    await _context.UpdateBatchRecordAsync(item);
                }
                try
                {
                    if (!string.IsNullOrEmpty(exceptionFilePath))
                    {
                        var email = (await _identityContext.Users.Where(l => l.Id == item.CreatedBy).AsNoTracking().FirstOrDefaultAsync())!.Email!;
                        await SendValidatedBatchUploadFile(email, item.Module, exceptionFilePath);
                    }
                }
                catch (Exception ex)
                {               
                    _logger.LogError(ex, @"ProcessBatchUploadAsync Error Message : {Message} / StackTrace : {StackTrace}", ex.Message, ex.StackTrace);
                }              
            }
        }
		private async Task<string?> ValidateBatchUpload(string module, string path, string processedByUserId)
        {
            string? exceptionFilePath = null;   
            switch (module)
            {
                case nameof(SampleParentState):
					var sampleParentImportResult = await _excelService.ImportAsync<SampleParentState>(path);
					if (sampleParentImportResult.IsSuccess)
					{
						await _context.AddRangeAsync(sampleParentImportResult.SuccessRecords);
					}
					else
					{
						exceptionFilePath = ExcelService.UpdateExistingExcelValidationResult<SampleParentState>(sampleParentImportResult.FailedRecords, _uploadPath + "\\BatchUploadErrors", path);
					}
					break;
				case nameof(EmployeeState):
					var employeeImportResult = await _excelService.ImportAsync<EmployeeState>(path);
					if (employeeImportResult.IsSuccess)
					{
						await _context.AddRangeAsync(employeeImportResult.SuccessRecords);
					}
					else
					{
						exceptionFilePath = ExcelService.UpdateExistingExcelValidationResult<EmployeeState>(employeeImportResult.FailedRecords, _uploadPath + "\\BatchUploadErrors", path);
					}
					break;
				case nameof(ContactInformationState):
					var contactInformationImportResult = await _excelService.ImportAsync<ContactInformationState>(path);
					if (contactInformationImportResult.IsSuccess)
					{
						await _context.AddRangeAsync(contactInformationImportResult.SuccessRecords);
					}
					else
					{
						exceptionFilePath = ExcelService.UpdateExistingExcelValidationResult<ContactInformationState>(contactInformationImportResult.FailedRecords, _uploadPath + "\\BatchUploadErrors", path);
					}
					break;
				case nameof(HealthDeclarationState):
					var healthDeclarationImportResult = await _excelService.ImportAsync<HealthDeclarationState>(path);
					if (healthDeclarationImportResult.IsSuccess)
					{
						await _context.AddRangeAsync(healthDeclarationImportResult.SuccessRecords);
					}
					else
					{
						exceptionFilePath = ExcelService.UpdateExistingExcelValidationResult<HealthDeclarationState>(healthDeclarationImportResult.FailedRecords, _uploadPath + "\\BatchUploadErrors", path);
					}
					break;
				
                default: break;
            }           
            return exceptionFilePath;
        }
        
        public async Task SendValidatedBatchUploadFile(string email, string module, string exceptionFilePath)
        {
            string wordToRemove = "State";
            if (module.EndsWith(wordToRemove))
            {
                module = module[..^wordToRemove.Length];
            }
            string subject = $"Batch Upload - " + module;
            string message = GenerateEmailBody();
            var emailRequest = new MailRequest()
            {
                Subject = subject,
                Body = message,
                Attachments = new List<string>() { exceptionFilePath },
                To = email
            };           
            await _emailSender.SendAsync(emailRequest);
        }
        private static string GenerateEmailBody()
        {
            string str = "<span style='font-size:10pt; font-family:Arial;'> ";
            str += "Your uploaded file has been failed on processing. Please see attached file for the validation remarks.";
            str += "<br />";           
            str += "</span> ";
            return str;
        }
    }
}
