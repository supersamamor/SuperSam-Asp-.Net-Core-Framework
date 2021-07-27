using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Core.Inventory
{
    public record Project : BaseEntity
    {
        public string? Code { get; init; }
        public string? Name { get; init; }
        public string? Description { get; init; }
        public string? Type { get; init; }
        public string? Status { get; init; }
    }
}
