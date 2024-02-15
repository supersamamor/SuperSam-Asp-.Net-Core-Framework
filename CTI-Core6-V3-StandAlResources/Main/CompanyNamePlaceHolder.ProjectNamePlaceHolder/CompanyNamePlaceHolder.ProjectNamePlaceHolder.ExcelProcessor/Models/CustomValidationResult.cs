using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.ExcelProcessor.Models
{  
    public class CustomValidationResult
    {
        public int RowNumber { get; set; }
        public required Dictionary<string, object?> Data { get; set; }
        public string Remarks { get; set; } = "";
    }
}
