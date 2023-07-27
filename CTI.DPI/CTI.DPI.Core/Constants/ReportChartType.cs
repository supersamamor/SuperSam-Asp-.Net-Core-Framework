namespace CTI.DPI.Core.Constants
{
    public static class ReportChartType
    {
        public const string Table = "Table";
        public const string HorizontalBar = "Horizontal Bar";
        public const string Pie = "Pie";

        public static readonly Dictionary<string, string> ChartToolTip = new()
        {
            { "Table", "" },
            { "Horizontal Bar", "Your query should consist of label and data fields (eg. Select 'Record 1' as <b>Label</b>, 10 as  <b>Data</b>)" },
            { "Pie", "Your query should consist of label and data fields (eg. Select 'Record 1' as <b>Label</b>, 10 as  <b>Data</b>)" },
        };
    }
}
