using Ardalis.Specification;
using Ardalis.Specification.EntityFrameworkCore;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Cti.Core.Application.Common.Persistence;
using Cti.Core.Domain.Common.Contracts;
using System.Linq;

namespace CHANGE_TO_APP_NAME.Services.Infrastructure.Persistence.Repositories
{
    public class ApplicationDbRepository<T> : RepositoryBase<T>, IReadRepository<T>, IRepository<T>
        where T : class, IAggregateRoot
    {
        private readonly IMapper _mapper;

        public ApplicationDbRepository(ApplicationDbContext dbContext, IMapper mapper)
            : base(dbContext)
        {
            _mapper = mapper;
        }

        // We override the default behavior when mapping to a dto.
        // We're using Automapper ProjectTo here to immediately map the result from the database.
        protected override IQueryable<TResult> ApplySpecification<TResult>(ISpecification<T, TResult> specification) =>
            ApplySpecification(specification, false)
                .ProjectTo<TResult>(_mapper.ConfigurationProvider);
    }
}
