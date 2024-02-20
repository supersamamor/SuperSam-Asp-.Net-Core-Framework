using System.Collections.Generic;

namespace Cti.Core.Application.Common.Models
{
    public class Search
    {
        public List<string> Fields { get; set; } = new List<string>();
        public string Keyword { get; set; }
    }
}