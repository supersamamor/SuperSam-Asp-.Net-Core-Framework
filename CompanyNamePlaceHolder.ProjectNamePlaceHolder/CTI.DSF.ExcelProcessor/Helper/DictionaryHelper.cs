using CTI.DSF.ExcelProcessor.Models;
using CTI.DSF.ExcelProcessor.Resources;
namespace CTI.DSF.ExcelProcessor.Helper
{
    public static class DictionaryHelper
    {
        public static Dictionary<string, HashSet<int>> FindDuplicateRowNumbersPerKey(List<ExcelRecord> records, List<string> listOfKeys)
        {
            Dictionary<string, HashSet<int>> duplicates = new();
            Dictionary<string, Dictionary<string, int>> keyCountsPerField = new();
            foreach (var key in listOfKeys)
            {
                var translatedKey = FieldDictionary.Translate(key);
                keyCountsPerField[translatedKey] = new Dictionary<string, int>();
                duplicates[translatedKey] = new HashSet<int>();
            }
            foreach (var record in records)
            {
                foreach (var key in listOfKeys)
                {
                    var translatedKey = FieldDictionary.Translate(key);
                    var keyCounts = keyCountsPerField[translatedKey];
                    var duplicateRowNumbers = duplicates[translatedKey];
                    if (record.Data.TryGetValue(key, out var value))
                    {
                        string stringValue = value?.ToString() ?? string.Empty;
                        if (keyCounts.TryGetValue(stringValue, out var count))
                        {
                            keyCounts[stringValue]++;
                            if (count == 1)
                            {
                                duplicateRowNumbers.Add(record.RowNumber);
                            }
                        }
                        else
                        {
                            keyCounts[stringValue] = 1;
                        }
                    }
                }
            }
            return duplicates;
        }
    }
}
