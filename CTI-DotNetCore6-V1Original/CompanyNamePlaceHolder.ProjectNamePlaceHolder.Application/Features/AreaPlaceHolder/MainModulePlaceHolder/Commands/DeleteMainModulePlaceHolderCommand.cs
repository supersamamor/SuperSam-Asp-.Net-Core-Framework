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

public record DeleteMainModulePlaceHolderCommand : BaseCommand, IRequest<Validation<Error, ProjectState>>;

public class DeleteMainModulePlaceHolderCommandHandler : BaseCommandHandler<ApplicationContext, ProjectState, DeleteMainModulePlaceHolderCommand>, IRequestHandler<DeleteMainModulePlaceHolderCommand, Validation<Error, ProjectState>>
{
    public DeleteMainModulePlaceHolderCommandHandler(ApplicationContext context,
                                       IMapper mapper,
                                       CompositeValidator<DeleteMainModulePlaceHolderCommand> validator) : base(context, mapper, validator)
    {
    }

    public async Task<Validation<Error, ProjectState>> Handle(DeleteMainModulePlaceHolderCommand request, CancellationToken cancellationToken)
    {
        var project = await _context.GetSingle<ProjectState>(p => p.Id == request.Id, cancellationToken, true);
        return await project.MatchAsync(
            Some: async p => await Delete(p, cancellationToken),
            None: () => Fail<Error, ProjectState>($"Project not found"));
    }
}
