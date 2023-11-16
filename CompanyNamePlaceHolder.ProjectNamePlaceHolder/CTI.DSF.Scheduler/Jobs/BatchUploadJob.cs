using CTI.Common.Services.Shared.Interfaces;
using CTI.Common.Services.Shared.Models.Mail;
using CTI.DSF.Core.DSF;
using CTI.DSF.ExcelProcessor.Models;
using CTI.DSF.Infrastructure.Data;
using CTI.DSF.ExcelProcessor.Services;
using LanguageExt;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Quartz;


namespace CTI.DSF.Scheduler.Jobs
{
    [DisallowConcurrentExecution]
    public class BatchUploadJob : IJob
    {
        private readonly ApplicationContext _context;
		private readonly ILogger<BatchUploadJob> _logger;
		private readonly string? _uploadPath;
		private readonly IMailService _emailSender;
		private readonly ExcelService _excelService;
		public BatchUploadJob(ApplicationContext context, ILogger<BatchUploadJob> logger, IConfiguration configuration, IMailService emailSender, ExcelService excelService)
		{
			_context = context;
			_logger = logger;
			_uploadPath = configuration.GetValue<string>("UsersUpload:UploadFilesPath");
			_emailSender = emailSender;
			_excelService = excelService;
		}
        public async Task Execute(IJobExecutionContext context)
        {
            await ProcessBatchUploadAsync();
        }
        private async Task ProcessBatchUploadAsync()
        {
            var uploadProcessorList = await _context.UploadProcessor.Where(l => l.Status == Core.Constants.FileUploadStatus.Pending).IgnoreQueryFilters().AsNoTracking()
                .OrderBy(l => l.CreatedDate).ToListAsync();
            foreach (var item in uploadProcessorList)
            {
                try
                {
                    //Tag Start Date/Time
                    item.SetStart();
                    _context.Update(item);
                    await _context.UpdateBatchRecordAsync(item);
                    //Start Processing
                    var exceptionFilePath = await ValidateBatchUpload(item.Module, item.Path, item.CreatedBy!);
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
            }
        }
        private async Task<string?> ValidateBatchUpload(string module, string path, string processedByUserId)
        {
            dynamic? importResult = default;
            string? exceptionFilePath = null;
            List<ExcelImportResultModel> errorList = new();
            switch (module)
            {              
				case nameof(DepartmentState):
					importResult = await _excelService.ImportAsync<DepartmentState>(path);
					if (importResult is IList<DepartmentState> departmentlist)
					{
						await _context.AddRangeAsync(departmentlist);
					}
					else if (importResult is List<ExcelImportResultModel>)
					{
						errorList = importResult;
						exceptionFilePath = ExcelService.ExportExcelValidationResult<DepartmentState>(errorList, _uploadPath + "\\BatchUploadErrors");
					}
					break;				
                default: break;
            }
            if (!string.IsNullOrEmpty(exceptionFilePath))
            {
                await SendValidatedBatchUploadFile("", module, exceptionFilePath);
            }
            return exceptionFilePath;
        }
        private async Task SendValidatedBatchUploadFile(string email, string module, string exceptionFilePath)
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
