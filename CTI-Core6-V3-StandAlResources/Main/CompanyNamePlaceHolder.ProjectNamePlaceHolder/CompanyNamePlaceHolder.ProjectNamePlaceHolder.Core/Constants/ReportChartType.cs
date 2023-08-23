namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Core.Constants
{
    public static class ReportChartType
    {
        public const string Table = "Table";
        public const string HorizontalBar = "Horizontal Bar";
        public const string Pie = "Pie";
        public const string ShortCodeFinePrint = @"
            <ul>
                <span>You can use the ff. short codes from the query.</span>
                <li>Current logged user`s id - {{CurrentUserId}}</li>
                <li>Date/time at the time of the report generation - {{CurrentDateTime}}</li>
            </ul>";
        public static readonly Dictionary<string, string> ChartToolTip = new()
        {
            { "Table", "<small>" + ShortCodeFinePrint +"</small>" },
            { "Horizontal Bar", @"<small>Your query should consist of one field with `label` field name and other fields should be a numeric value
                            <br><quote>(eg. Select [Type] <b>Label</b>,
                                Count(*) <b>[Transaction Count]</b>,
                                Count(Distinct UserId) <b>[Number of Users]</b> From AuditLogs Group by [Type])</quote><br>" + ShortCodeFinePrint + "</small>" },
            { "Pie", @"<small>Your query should consist of one field with `label` field name and other fields should be a numeric value
                            <br><quote>(eg. Select [Type] <b>Label</b>,
                                Count(*) <b>[Transaction Count]</b>,
                                Count(Distinct UserId) <b>[Number of Users]</b> From AuditLogs Group by [Type])</quote><br>" + ShortCodeFinePrint + "</small>" },
        };


    }
}
