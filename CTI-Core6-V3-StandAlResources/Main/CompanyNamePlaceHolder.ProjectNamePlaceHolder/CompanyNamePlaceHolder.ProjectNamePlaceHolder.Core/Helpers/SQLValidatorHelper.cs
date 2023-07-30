using System.Text.RegularExpressions;

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Core.Helpers
{
    public static class SQLValidatorHelper
    {
        public static string Validate(string? sqlScript)
        {
            var validationResult = "";
            if (sqlScript != null)
            {
                if (Regex.IsMatch(sqlScript, @"\bINSERT\b", RegexOptions.IgnoreCase))
                {
                    validationResult += "Sql Script has `Insert`. ";
                }
                if (Regex.IsMatch(sqlScript, @"\bDELETE\b", RegexOptions.IgnoreCase))
                {
                    validationResult += "Sql Script has `Delete`. ";
                }
                if (Regex.IsMatch(sqlScript, @"\bUPDATE\b", RegexOptions.IgnoreCase))
                {
                    validationResult += "Sql Script has `Update`. ";
                }
                if (Regex.IsMatch(sqlScript, @"\bCREATE\b", RegexOptions.IgnoreCase))
                {
                    validationResult += "Sql Script has `Create`. ";
                }
                if (Regex.IsMatch(sqlScript, @"\bALTER\b", RegexOptions.IgnoreCase))
                {
                    validationResult += "Sql Script has `Alter`. ";
                }
                if (Regex.IsMatch(sqlScript, @"\bDROP\b", RegexOptions.IgnoreCase))
                {
                    validationResult += "Sql Script has `Drop`. ";
                }
            }
            return validationResult;
        }
    }
}
