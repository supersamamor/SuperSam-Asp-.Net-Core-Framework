using LanguageExt;
using Microsoft.EntityFrameworkCore;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Core;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Infrastructure.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Models
{
    public record BaseQueryById(string Id);

    public class BaseQueryByIdHandler<TContext, TEntity, TQuery>
        where TContext : DbContext
        where TEntity : BaseEntity
        where TQuery : BaseQueryById
    {
        protected readonly TContext _context;

        public BaseQueryByIdHandler(TContext context)
        {
            _context = context;
        }

        public virtual async Task<Option<TEntity>> Handle(TQuery request, CancellationToken cancellationToken) =>
            await _context.GetSingle<TEntity>(e => e.Id == request.Id, cancellationToken);
    }
}
