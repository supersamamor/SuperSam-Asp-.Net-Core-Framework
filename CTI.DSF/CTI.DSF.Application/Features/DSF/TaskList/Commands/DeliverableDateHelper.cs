namespace CTI.DSF.Application.Helpers
{
    public static class DeliverableDateHelper
    {
        public static List<DateTime> GenerateDatesOfDeliverablesDueDate(DateTime startDate, DateTime endDate, string frequency)
        {
            List<DateTime> dates = new();
            DateTime currentDate = startDate;
            while (currentDate <= endDate)
            {
                if (frequency == Core.Constants.TaskFrequencies.Annual)
                {
                    dates.Add(currentDate);
                    currentDate = currentDate.AddYears(1).AddDays(-1);
                }
                else if (frequency == Core.Constants.TaskFrequencies.SemiAnnual)
                {
                    dates.Add(currentDate);
                    currentDate = currentDate.AddMonths(6).AddDays(-1);
                }
                else if (frequency == Core.Constants.TaskFrequencies.Quarterly)
                {
                    dates.Add(currentDate);
                    currentDate = currentDate.AddMonths(3).AddDays(-1);
                }
                else if (frequency == Core.Constants.TaskFrequencies.Monthly)
                {
                    dates.Add(currentDate);
                    currentDate = currentDate.AddMonths(1).AddDays(-1);
                }
                else if (frequency == Core.Constants.TaskFrequencies.Weekly)
                {
                    dates.Add(currentDate);
                    currentDate = currentDate.AddDays(7).AddDays(-1);
                }
                else if (frequency == Core.Constants.TaskFrequencies.Daily)
                {
                    dates.Add(currentDate);
                    currentDate = currentDate.AddDays(1).AddDays(-1);
                }
                else if (frequency == Core.Constants.TaskFrequencies.OneTime)
                {
                    dates.Add(currentDate);
                    break;
                }
            }
            // Filter out weekends
            dates = dates.Where(date => date.DayOfWeek != DayOfWeek.Saturday && date.DayOfWeek != DayOfWeek.Sunday).ToList();
            return dates;
        }
    }
}
