﻿using CTI.DSF.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CTI.DSF.ExcelProcessor.CustomValidation
{
    public static class DepartmentValidator
    {
        public static async Task<Dictionary<string, object?>>  ValidateAsync(ApplicationContext context, Dictionary<string, object?> rowValue)
        {
            var companyCode = rowValue["CompanyCode"]?.ToString();
            var company = await context.Company.Where(l => l.CompanyName == companyCode).AsNoTracking().IgnoreQueryFilters().FirstOrDefaultAsync();
            if(company == null) {
                throw new Exception("Company code is not valid.");
            }
            rowValue["CompanyCode"] = company?.Id;
            return rowValue;
        }
    }
}
