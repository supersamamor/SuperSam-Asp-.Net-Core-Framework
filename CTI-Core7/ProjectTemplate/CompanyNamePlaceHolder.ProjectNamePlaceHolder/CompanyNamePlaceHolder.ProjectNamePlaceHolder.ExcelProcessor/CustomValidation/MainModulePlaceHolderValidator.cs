using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Core.AreaPlaceHolder;

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.ExcelProcessor.CustomValidation
{
    public static class MainModulePlaceHolderValidator
    {
        public static async Task<Dictionary<string, object?>>  ValidatePerRecordAsync(ApplicationContext context, Dictionary<string, object?> rowValue)
        {
            Template:[ExcelValidationPerField]
            return rowValue;
        }
    }
}
