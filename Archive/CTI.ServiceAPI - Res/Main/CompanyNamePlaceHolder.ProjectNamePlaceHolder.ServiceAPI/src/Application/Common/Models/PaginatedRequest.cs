using System.Linq;

namespace Cti.Core.Application.Common.Models
{
    public class PaginatedRequest : BaseFilter
    {
        public int PageNumber { get; set; }

        public int PageSize { get; set; } = int.MaxValue;

        public string[] OrderBy { get; set; }
    }

    public static class PaginatedRequestExtensions
    {
        public static bool HasOrderBy(this PaginatedRequest filter) =>
            filter.OrderBy?.Any() is true;
    }
}