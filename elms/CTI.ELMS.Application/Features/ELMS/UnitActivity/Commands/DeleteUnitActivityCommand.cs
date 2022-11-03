using AutoMapper;
using CTI.Common.Core.Commands;
using CTI.Common.Data;
using CTI.Common.Utility.Validators;
using CTI.ELMS.Core.ELMS;
using CTI.ELMS.Infrastructure.Data;
using FluentValidation;
using LanguageExt;
using LanguageExt.Common;
using MediatR;

namespace CTI.ELMS.Application.Features.ELMS.UnitActivity.Commands;

public record DeleteUnitActivityCommand : BaseCommand, IRequest<Validation<Error, UnitActivityState>>;

public class DeleteUnitActivityCommandHandler : BaseCommandHandler<ApplicationContext, UnitActivityState, DeleteUnitActivityCommand>, IRequestHandler<DeleteUnitActivityCommand, Validation<Error, UnitActivityState>>
{
    public DeleteUnitActivityCommandHandler(ApplicationContext context,
                                       IMapper mapper,
                                       CompositeValidator<DeleteUnitActivityCommand> validator) : base(context, mapper, validator)
    {
    }

    public async Task<Validation<Error, UnitActivityState>> Handle(DeleteUnitActivityCommand request, CancellationToken cancellationToken) =>
        await Validators.ValidateTAsync(request, cancellationToken).BindT(
            async request => await Delete(request.Id, cancellationToken));
}


public class DeleteUnitActivityCommandValidator : AbstractValidator<DeleteUnitActivityCommand>
{
    readonly ApplicationContext _context;

    public DeleteUnitActivityCommandValidator(ApplicationContext context)
    {
        _context = context;
        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<UnitActivityState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("UnitActivity with id {PropertyValue} does not exists");
    }
}
