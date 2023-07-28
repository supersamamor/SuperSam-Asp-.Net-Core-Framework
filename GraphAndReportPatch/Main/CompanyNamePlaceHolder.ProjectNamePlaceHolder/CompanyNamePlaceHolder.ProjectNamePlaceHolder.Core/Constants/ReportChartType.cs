namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Core.Constants
{
    public static class ReportChartType
    {
        public const string Table = "Table";
        public const string HorizontalBar = "Horizontal Bar";
        public const string Pie = "Pie";
        public const string ShortCodeFinePrint = @"<br>
            <ul>
                <span></span>
                <li>Current logged user`s id - {{CurrentUserId}}</li>
                <li>Date/time at the time of the report generation - {{CurrentDateTime}}</li>
            </ul>";
        public static readonly Dictionary<string, string> ChartToolTip = new()
        {
            { "Table", ShortCodeFinePrint },
            { "Horizontal Bar", "Your query should consist of label and data fields (eg. Select 'Record 1' as <b>Label</b>, 10 as  <b>Data</b>)" + ShortCodeFinePrint },
            { "Pie", "Your query should consist of label and data fields (eg. Select 'Record 1' as <b>Label</b>, 10 as  <b>Data</b>)"+ ShortCodeFinePrint},
        };


    }
}
