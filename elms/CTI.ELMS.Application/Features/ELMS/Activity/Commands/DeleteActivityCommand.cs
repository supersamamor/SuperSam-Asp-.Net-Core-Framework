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

namespace CTI.ELMS.Application.Features.ELMS.Activity.Commands;

public record DeleteActivityCommand : BaseCommand, IRequest<Validation<Error, ActivityState>>;

public class DeleteActivityCommandHandler : BaseCommandHandler<ApplicationContext, ActivityState, DeleteActivityCommand>, IRequestHandler<DeleteActivityCommand, Validation<Error, ActivityState>>
{
    public DeleteActivityCommandHandler(ApplicationContext context,
                                       IMapper mapper,
                                       CompositeValidator<DeleteActivityCommand> validator) : base(context, mapper, validator)
    {
    }

    public async Task<Validation<Error, ActivityState>> Handle(DeleteActivityCommand request, CancellationToken cancellationToken) =>
        await Validators.ValidateTAsync(request, cancellationToken).BindT(
            async request => await Delete(request.Id, cancellationToken));
}


public class DeleteActivityCommandValidator : AbstractValidator<DeleteActivityCommand>
{
    readonly ApplicationContext _context;

    public DeleteActivityCommandValidator(ApplicationContext context)
    {
        _context = context;
        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<ActivityState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("Activity with id {PropertyValue} does not exists");
    }
}
