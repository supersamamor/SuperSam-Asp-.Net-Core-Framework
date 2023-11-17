using CTI.DSF.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using CTI.DSF.Core.DSF;
using CTI.DSF.ExcelProcessor.Models;
using CTI.DSF.ExcelProcessor.Helper;

namespace CTI.DSF.ExcelProcessor.CustomValidation
{
    public static class DepartmentValidator
    {
        public static async Task<Dictionary<string, object?>> ValidatePerRecordAsync(ApplicationContext context, Dictionary<string, object?> rowValue)
        {
            string errorValidation = "";
            //foreign key validation
            var companyCode = rowValue[nameof(DepartmentState.CompanyCode)]?.ToString();
            var company = await context.Company.Where(l => l.CompanyCode == companyCode).AsNoTracking().IgnoreQueryFilters().FirstOrDefaultAsync();
            if (company == null)
            {
                errorValidation += $"Company Code is not valid.;";
            }
            else
            {
                rowValue[nameof(DepartmentState.CompanyCode)] = company?.Id;
            }
          
            if (!string.IsNullOrEmpty(companyCode))
            {
                //Data Length Validation
                var companyCodeMaxLength = 255;
                if (companyCode.Length > companyCodeMaxLength)
                {
                    errorValidation += $"Company Code should be less than {companyCodeMaxLength} characters.;";
                }
                //Duplicate Validation Per Record       
                if (await context.Department.Where(l => l.CompanyCode == companyCode).AsNoTracking().IgnoreQueryFilters().AnyAsync())
                {
                    errorValidation += $"Company Code should be less than {companyCodeMaxLength} characters.;";
                }
            }
            
            if (!string.IsNullOrEmpty(errorValidation))
            {
                throw new Exception(errorValidation);
            }
            return rowValue;
        }
        public static Dictionary<string, HashSet<int>> DuplicateValidation(List<ExcelRecord> records)
        {
            List<string> listOfKeys = new()
            {
                nameof(DepartmentState.CompanyCode),
                nameof(DepartmentState.DepartmentName),
            };
            return DictionaryHelper.FindDuplicateRowNumbersPerKey(records, listOfKeys);
        }
    }
}
