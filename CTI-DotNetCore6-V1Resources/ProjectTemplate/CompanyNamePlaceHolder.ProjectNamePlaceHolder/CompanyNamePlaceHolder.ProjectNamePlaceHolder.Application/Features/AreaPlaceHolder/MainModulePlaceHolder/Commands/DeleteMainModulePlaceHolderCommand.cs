using AutoMapper;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Common;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Core.AreaPlaceHolder;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Infrastructure.Data;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Infrastructure.Extensions;
using LanguageExt;
using LanguageExt.Common;
using MediatR;
using static LanguageExt.Prelude;

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Features.AreaPlaceHolder.MainModulePlaceHolder.Commands;

public record DeleteMainModulePlaceHolderCommand : BaseCommand, IRequest<Validation<Error, MainModulePlaceHolderState>>;

public class DeleteMainModulePlaceHolderCommandHandler : BaseCommandHandler<ApplicationContext, MainModulePlaceHolderState, DeleteMainModulePlaceHolderCommand>, IRequestHandler<DeleteMainModulePlaceHolderCommand, Validation<Error, MainModulePlaceHolderState>>
{
    public DeleteMainModulePlaceHolderCommandHandler(ApplicationContext context,
                                       IMapper mapper,
                                       CompositeValidator<DeleteMainModulePlaceHolderCommand> validator) : base(context, mapper, validator)
    {
    }

    public async Task<Validation<Error, MainModulePlaceHolderState>> Handle(DeleteMainModulePlaceHolderCommand request, CancellationToken cancellationToken)
    {
        var mainModulePlaceHolder = await _context.GetSingle<MainModulePlaceHolderState>(p => p.Id == request.Id, cancellationToken, true);
        return await mainModulePlaceHolder.MatchAsync(
            Some: async p => await Delete(p, cancellationToken),
            None: () => Fail<Error, MainModulePlaceHolderState>($"MainModulePlaceHolder not found"));
    }
}
