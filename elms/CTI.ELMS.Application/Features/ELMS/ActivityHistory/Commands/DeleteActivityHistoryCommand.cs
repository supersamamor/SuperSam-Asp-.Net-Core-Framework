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

namespace CTI.ELMS.Application.Features.ELMS.ActivityHistory.Commands;

public record DeleteActivityHistoryCommand : BaseCommand, IRequest<Validation<Error, ActivityHistoryState>>;

public class DeleteActivityHistoryCommandHandler : BaseCommandHandler<ApplicationContext, ActivityHistoryState, DeleteActivityHistoryCommand>, IRequestHandler<DeleteActivityHistoryCommand, Validation<Error, ActivityHistoryState>>
{
    public DeleteActivityHistoryCommandHandler(ApplicationContext context,
                                       IMapper mapper,
                                       CompositeValidator<DeleteActivityHistoryCommand> validator) : base(context, mapper, validator)
    {
    }

    public async Task<Validation<Error, ActivityHistoryState>> Handle(DeleteActivityHistoryCommand request, CancellationToken cancellationToken) =>
        await Validators.ValidateTAsync(request, cancellationToken).BindT(
            async request => await Delete(request.Id, cancellationToken));
}


public class DeleteActivityHistoryCommandValidator : AbstractValidator<DeleteActivityHistoryCommand>
{
    readonly ApplicationContext _context;

    public DeleteActivityHistoryCommandValidator(ApplicationContext context)
    {
        _context = context;
        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<ActivityHistoryState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("ActivityHistory with id {PropertyValue} does not exists");
    }
}
