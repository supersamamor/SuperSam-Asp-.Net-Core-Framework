namespace CTI.TenantSales.Web.Helper
{
    public static class SalesStatusHelper
    {
        public static string GetSalesStatusBadge(Core.Constants.ValidationStatusEnum validationStatus, string? validationRemarks)
        {
            return validationStatus switch
            {
                Core.Constants.ValidationStatusEnum.Passed => @"<span style=""cursor:pointer;"" class=""badge badge-success"">Passed</span>",
                Core.Constants.ValidationStatusEnum.Failed => @"<span style=""cursor:pointer;"" class=""badge badge-danger"" title=""" + validationRemarks + @""">Failed</span>",
                _ => "",
            };
        }
    }
}
