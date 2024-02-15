namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Helpers
{
    public static class DateHelper
    {
        public static int TimeOffset { get; set; } 
        public static DateTimeSpan AutocalculateYearMonthDayFromStartAndEndDate(DateTime startDate, DateTime endDate)
        {
            return DateTimeSpan.DateSpan(startDate, endDate.AddDays(1));     
        }
        public static DateTime ApplyTimeOffset(this DateTime dateTimeValue)
        {
            return dateTimeValue.AddHours(TimeOffset);
        }
    }
}
