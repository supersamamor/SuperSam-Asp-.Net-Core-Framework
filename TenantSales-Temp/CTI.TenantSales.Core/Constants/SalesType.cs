namespace CTI.TenantSales.Core.Constants
{
    public static class SalesType
    {
        public const string SALESTYPE_DAILY = "S";
        public const string SALESTYPE_HOURLY = "H";
        public static int SalesTypeToInt(string salesType)
        {
            return salesType switch
            {
                SALESTYPE_DAILY => Convert.ToInt32(SalesTypeEnum.Daily),
                SALESTYPE_HOURLY => Convert.ToInt32(SalesTypeEnum.Hourly),
                _ => 0,
            };
        }
    }
}
