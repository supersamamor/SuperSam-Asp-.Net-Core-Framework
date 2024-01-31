using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyPL.ProjectPL.ExcelProcessor.Models
{  
    public class CustomValidationResult
    {
        public int RowNumber { get; set; }
        public required Dictionary<string, object?> Data { get; set; }
        public string Remarks { get; set; } = "";
    }
}
