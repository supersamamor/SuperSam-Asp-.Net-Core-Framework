using System.Globalization;

namespace CTI.DSF.ExcelProcessor.Resources
{
    public static class FieldDictionary
    {
        private static readonly Dictionary<string, string> translations = new();
        static FieldDictionary()
        {
            // Initialize the dictionary with translations
            translations.Add("FirstName", "First Name");
            translations.Add("CompanyCode", "Company Code");
            // Add more translations as needed
        }

        public static string Translate(string term)
        {
            // You may want to handle cases where the term is not in the dictionary
            return translations.TryGetValue(term, out var translation) ? translation : SplitPascalCase(term);
        }
        private static string SplitPascalCase(string pascalCase)
        {
            string[] words = pascalCase.Split('_');
            string fixedUnderScoreSeparatedString = "";
            // Create a TextInfo object to convert the first letter of each word to uppercase
            TextInfo textInfo = CultureInfo.CurrentCulture.TextInfo;
            foreach (string word in words)
            {
                fixedUnderScoreSeparatedString += textInfo.ToTitleCase(word).Trim();
            }
            if (words.Length > 1)
            {
                pascalCase = fixedUnderScoreSeparatedString;
            }
            string spacedString = "";
            for (int i = 0; i < pascalCase.Length; i++)
            {
                char currentChar = pascalCase[i];
                if (i > 0 && char.IsUpper(currentChar) && (!char.IsUpper(pascalCase[i - 1]) || (i < pascalCase.Length - 1 && !char.IsUpper(pascalCase[i + 1]))))
                {
                    spacedString += " ";
                }
                spacedString += currentChar;
            }
            return spacedString;
        }

    }
}
