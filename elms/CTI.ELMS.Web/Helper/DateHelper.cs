namespace CTI.ELMS.Web.Helper
{
    public static class DateHelper
    {
        public static DateTimeSpan AutocalculateYearMonthDayFromStartAndEndDate(DateTime startDate, DateTime endDate)
        {
            return DateTimeSpan.DateSpan(startDate, endDate.AddDays(1));     
        }
    }
}
