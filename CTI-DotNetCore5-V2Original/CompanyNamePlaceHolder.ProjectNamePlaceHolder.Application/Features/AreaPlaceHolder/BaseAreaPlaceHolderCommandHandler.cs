using AutoMapper;
using LanguageExt;
using static LanguageExt.Prelude;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Common.Models;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Core.AreaPlaceHolder;

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Features.AreaPlaceHolder
{
    public class BaseAreaPlaceHolderCommandHandler<T> where T : BaseAreaPlaceHolder
    {
        protected readonly ApplicationContext _context;
        protected readonly IMapper _mapper;

        protected BaseAreaPlaceHolderCommandHandler(ApplicationContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        protected async Task<Validation<Error, TEntity>> Add<TEntity>(T request, CancellationToken cancellationToken)
            where TEntity : BaseAreaPlaceHolder
        {
            var entity = _mapper.Map<TEntity>(request);
            _context.Add(entity);
            _ = await _context.SaveChangesAsync(cancellationToken);
            return Success<Error, TEntity>(entity);
        }

        protected async Task<Validation<Error, TEntity>> Edit<TEntity>(T request, TEntity entity, CancellationToken cancellationToken)
            where TEntity : BaseAreaPlaceHolder
        {
            _mapper.Map(request, entity);
            _context.Update(entity);
            _ = await _context.SaveChangesAsync(cancellationToken);
            return Success<Error, TEntity>(entity);
        }
    }
}
