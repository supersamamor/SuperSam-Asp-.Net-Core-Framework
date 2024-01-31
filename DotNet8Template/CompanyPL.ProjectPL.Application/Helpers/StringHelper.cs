using System.Globalization;
using System.Text.RegularExpressions;

namespace CompanyPL.ProjectPL.Application.Helpers
{
    public static class StringHelper
    {
        public static string ToProperCase(string input)
        {
            // Create a TextInfo object with the current culture to handle casing rules correctly.
            TextInfo textInfo = CultureInfo.CurrentCulture.TextInfo;
            return textInfo.ToTitleCase(input);
        }
        public static string Sanitize(string? input)
        {
            return RemoveSpecialCharacters(ToCamelCase(input));
        }
        public static string ReplaceCaseInsensitive(string input, string pattern, string replacement)
        {
            // Escape the pattern for regex and enable case-insensitive matching
            string escapedPattern = Regex.Escape(pattern);
            string regexPattern = "(?i)" + escapedPattern;

            // Replace using the regex pattern
            string result = Regex.Replace(input, regexPattern, replacement);

            return result;
        }
        private static string ToCamelCase(string? input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return "";
            }
            TextInfo textInfo = CultureInfo.CurrentCulture.TextInfo;
            string[] words = input.Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < words.Length; i++)
            {
                if (i == 0)
                {
                    words[i] = words[i].ToLower(); // First word remains in lowercase
                }
                else
                {
                    words[i] = textInfo.ToTitleCase(words[i]);
                }
            }

            return string.Join("", words);
        }
        private static string RemoveSpecialCharacters(string? input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return "";
            }
            // Define the regular expression pattern to match special characters.
            string pattern = @"[^a-zA-Z0-9\s]";

            // Use Regex.Replace to remove special characters from the input string.
            string result = Regex.Replace(input, pattern, "");

            return result;
        }

       
    }
}
