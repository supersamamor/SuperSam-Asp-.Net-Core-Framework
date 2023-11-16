using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.ExcelProcessor.CustomValidation
{
    public static class MainModulePlaceHolderValidator
    {
        public static async Task<Dictionary<string, object?>>  ValidateAsync(ApplicationContext context, Dictionary<string, object?> rowValue)
        {
            Template:[ExcelValidationPerField]
            return rowValue;
        }
    }
}
