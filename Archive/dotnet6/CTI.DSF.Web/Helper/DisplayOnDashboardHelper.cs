namespace CTI.DSF.Web.Helper
{
    public static class DisplayOnDashboardHelper
    {
        public static string GetBadge(bool isActive)
        {
            switch (isActive)
            {
                case true:
                    return @"<span class=""badge bg-success"">Show</span>";
                case false:
                    return @"<span class=""badge bg-secondary"">Hide</span>";
                default:
            }
        }
    }
}
