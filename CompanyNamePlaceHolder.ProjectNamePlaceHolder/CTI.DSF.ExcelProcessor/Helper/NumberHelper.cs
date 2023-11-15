namespace CTI.DSF.ExcelProcessor.Helper
{
    public static class NumberHelper
    {
        public static string NumberToExcelColumn(int columnNumber)
        {
            if (columnNumber < 1)
            {
                throw new ArgumentException("Column number must be greater than or equal to 1");
            }
            string columnName = "";
            while (columnNumber > 0)
            {
                int remainder = (columnNumber - 1) % 26;
                char character = (char)('A' + remainder);
                columnName = character + columnName;
                columnNumber = (columnNumber - 1) / 26;
            }
            return columnName;
        }
    }
}
