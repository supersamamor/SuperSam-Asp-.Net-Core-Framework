using Cti.Core.Application.Common.Models;

namespace Cti.Core.Application.Common.Specification
{ 
    public class EntitiesByPaginatedRequestSpec<T, TResult> : EntitiesByBaseFilterSpec<T, TResult>
    {
        public EntitiesByPaginatedRequestSpec(PaginatedRequest request)
            : base(request) =>
            Query.PaginateBy(request);
    }

    public class EntitiesByPaginatedRequestSpec<T> : EntitiesByBaseFilterSpec<T>
    {
        public EntitiesByPaginatedRequestSpec(PaginatedRequest request)
            : base(request) =>
            Query.PaginateBy(request);
    }
}