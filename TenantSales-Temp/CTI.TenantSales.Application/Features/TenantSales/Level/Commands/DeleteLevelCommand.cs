using AutoMapper;
using CTI.Common.Core.Commands;
using CTI.Common.Data;
using CTI.Common.Utility.Validators;
using CTI.TenantSales.Core.TenantSales;
using CTI.TenantSales.Infrastructure.Data;
using FluentValidation;
using LanguageExt;
using LanguageExt.Common;
using MediatR;

namespace CTI.TenantSales.Application.Features.TenantSales.Level.Commands;

public record DeleteLevelCommand : BaseCommand, IRequest<Validation<Error, LevelState>>;

public class DeleteLevelCommandHandler : BaseCommandHandler<ApplicationContext, LevelState, DeleteLevelCommand>, IRequestHandler<DeleteLevelCommand, Validation<Error, LevelState>>
{
    public DeleteLevelCommandHandler(ApplicationContext context,
                                       IMapper mapper,
                                       CompositeValidator<DeleteLevelCommand> validator) : base(context, mapper, validator)
    {
    }

    public async Task<Validation<Error, LevelState>> Handle(DeleteLevelCommand request, CancellationToken cancellationToken) =>
        await Validators.ValidateTAsync(request, cancellationToken).BindT(
            async request => await Delete(request.Id, cancellationToken));
}


public class DeleteLevelCommandValidator : AbstractValidator<DeleteLevelCommand>
{
    readonly ApplicationContext _context;

    public DeleteLevelCommandValidator(ApplicationContext context)
    {
        _context = context;
        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<LevelState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("Level with id {PropertyValue} does not exists");
    }
}
