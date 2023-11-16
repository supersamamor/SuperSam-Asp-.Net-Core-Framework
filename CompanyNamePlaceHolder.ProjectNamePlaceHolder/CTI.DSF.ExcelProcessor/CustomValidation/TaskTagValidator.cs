using CTI.DSF.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using CTI.DSF.Core.DSF;

namespace CTI.DSF.ExcelProcessor.CustomValidation
{
    public static class TaskTagValidator
    {
        public static async Task<Dictionary<string, object?>>  ValidateAsync(ApplicationContext context, Dictionary<string, object?> rowValue)
        {
            
            return rowValue;
        }
    }
}
