using Ardalis.Specification;
using Cti.Core.Application.Common.Specification;
using System.Threading;
using System.Threading.Tasks;

namespace Cti.Core.Application.Common.Models
{
    public static class PaginatedListExtensions
    {
        public static async Task<PaginatedList<TDestination>> PaginatedListAsync<T, TDestination>(
            this IReadRepositoryBase<T> repository, ISpecification<T, TDestination> spec, int pageNumber, int pageSize, CancellationToken cancellationToken = default)
            where T : class
            where TDestination : class
        {
            var list = await repository.ListAsync(spec, cancellationToken);
            int count = await repository.CountAsync(spec, cancellationToken);

            return new PaginatedList<TDestination>(list, count, pageNumber, pageSize);
        }
    }
}