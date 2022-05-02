using AutoMapper;
using LanguageExt;
using LanguageExt.Common;
using Microsoft.EntityFrameworkCore;
using static LanguageExt.Prelude;

namespace CompanyNamePlaceHolder.WebAppTemplate.Application.Common;

public class BaseCommandHandler<TContext, TEntity, TCommand>
    where TContext : DbContext
    where TEntity : class
    where TCommand : class

{
    protected readonly TContext _context;
    protected readonly IMapper _mapper;
    protected readonly CompositeValidator<TCommand> _validator;

    public BaseCommandHandler(TContext context,
                              IMapper mapper,
                              CompositeValidator<TCommand> validator)
    {
        _context = context;
        _mapper = mapper;
        _validator = validator;
    }

    protected async Task<Validation<Error, TEntity>> Add(TCommand request, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<TEntity>(request);
        _context.Add(entity);
        _ = await _context.SaveChangesAsync(cancellationToken);
        return Success<Error, TEntity>(entity);
    }

    protected async Task<Validation<Error, TEntity>> Edit(TCommand request, TEntity entity, CancellationToken cancellationToken)
    {
        _mapper.Map(request, entity);
        _context.Update(entity);
        _ = await _context.SaveChangesAsync(cancellationToken);
        return Success<Error, TEntity>(entity);
    }

    protected async Task<Validation<Error, TEntity>> Delete(TEntity entity, CancellationToken cancellationToken)
    {
        _context.Remove(entity);
        _ = await _context.SaveChangesAsync(cancellationToken);
        return Success<Error, TEntity>(entity);
    }
}
