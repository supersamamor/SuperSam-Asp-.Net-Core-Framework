using System.Text.RegularExpressions;

namespace CTI.DSF.Core.Helpers
{
    public static class SQLValidatorHelper
    {
        public static string Validate(string? sqlScript)
        {
            var validationResult = "";
            if (sqlScript != null)
            {
                if (Regex.IsMatch(sqlScript, @"\bINSERT\b(?!.*INTO\s+(#TempTable|#\w+))", RegexOptions.IgnoreCase))
                {
                    validationResult += "Sql Script has `Insert`. ";
                }
                if (Regex.IsMatch(sqlScript, @"\bDELETE\b", RegexOptions.IgnoreCase))
                {
                    validationResult += "Sql Script has `Delete`. ";
                }
                if (Regex.IsMatch(sqlScript, @"\bUPDATE\b(?!.*#TempTable\b)(?!.*#)", RegexOptions.IgnoreCase))
                {
                    validationResult += "Sql Script has `Update`. ";
                }
                if (Regex.IsMatch(sqlScript, @"\bCREATE\b(?!.*TABLE\s+(#TempTable|#\w+))", RegexOptions.IgnoreCase))
                {
                    validationResult += "Sql Script has `Create`. "; 
                }
                if (Regex.IsMatch(sqlScript, @"\bALTER\b", RegexOptions.IgnoreCase))
                {
                    validationResult += "Sql Script has `Alter`. ";
                }

                // Add a condition to check for DROP but allow DROP TABLE #TempTable or tables with hash "#"
                if (Regex.IsMatch(sqlScript, @"\bDROP\b(?!.*TABLE\s+(#TempTable|#\w+))", RegexOptions.IgnoreCase))
                {
                    validationResult += "Sql Script has `Drop`. ";
                }
            }
            return validationResult;
        }
    }
}
