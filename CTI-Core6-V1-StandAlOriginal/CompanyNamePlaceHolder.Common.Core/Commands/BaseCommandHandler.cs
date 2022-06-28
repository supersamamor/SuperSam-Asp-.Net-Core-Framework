using AutoMapper;
using CompanyNamePlaceHolder.Common.Core.Base.Models;
using CompanyNamePlaceHolder.Common.Data;
using CompanyNamePlaceHolder.Common.Utility.Validators;
using LanguageExt;
using LanguageExt.Common;
using Microsoft.EntityFrameworkCore;
using static LanguageExt.Prelude;

namespace CompanyNamePlaceHolder.Common.Core.Commands;

public class BaseCommandHandler<TContext, TEntity, TCommand>
    where TContext : DbContext
    where TEntity : BaseEntity
    where TCommand : IEntity
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

    protected async Task<Validation<Error, TEntity>> Add(TCommand request, CancellationToken cancellationToken = default)
    {
        var entity = _mapper.Map<TEntity>(request);
        _context.Add(entity);
        _ = await _context.SaveChangesAsync(cancellationToken);
        return Success<Error, TEntity>(entity);
    }

    protected async Task<Validation<Error, TEntity>> Edit(TCommand request, CancellationToken cancellationToken = default) =>
        await _context.GetSingle<TEntity>(e => e.Id == request.Id, cancellationToken: cancellationToken).MatchAsync(
            Some: async entity =>
            {
                _mapper.Map(request, entity);
                _context.Update(entity);
                _ = await _context.SaveChangesAsync(cancellationToken);
                return Success<Error, TEntity>(entity);
            },
            None: () => Fail<Error, TEntity>($"Record with id {request.Id} does not exist"));

    protected async Task<Validation<Error, TEntity>> Delete(string id, CancellationToken cancellationToken = default) =>
        await _context.GetSingle<TEntity>(e => e.Id == id, cancellationToken: cancellationToken).MatchAsync(
            Some: async entity =>
            {
                _context.Remove(entity);
                _ = await _context.SaveChangesAsync(cancellationToken);
                return Success<Error, TEntity>(entity);
            },
            None: () => Fail<Error, TEntity>($"Record with id {id} does not exist"));
}
