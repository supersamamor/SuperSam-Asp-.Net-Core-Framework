using CTI.DSF.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CTI.DSF.ExcelProcessor.CustomValidation
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
