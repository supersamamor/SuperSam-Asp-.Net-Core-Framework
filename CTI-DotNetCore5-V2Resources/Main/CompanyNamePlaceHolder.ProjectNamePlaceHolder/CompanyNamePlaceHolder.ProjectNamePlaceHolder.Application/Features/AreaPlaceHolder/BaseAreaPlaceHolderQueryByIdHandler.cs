using LanguageExt;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Models;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Core;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Core.AreaPlaceHolder;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Infrastructure.Extensions;

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Features.AreaPlaceHolder
{
    public class BaseAreaPlaceHolderQueryByIdHandler<TEntity, TQuery> where TEntity : BaseAreaPlaceHolder where TQuery : BaseQueryById
    {
        protected readonly ApplicationContext _context;

        public BaseAreaPlaceHolderQueryByIdHandler(ApplicationContext context)
        {
            _context = context;
        }

        public virtual async Task<Option<TEntity>> Handle(TQuery request, CancellationToken cancellationToken) =>
            await _context.GetSingle<TEntity>(e => e.Id == request.Id, cancellationToken);
    }
}
