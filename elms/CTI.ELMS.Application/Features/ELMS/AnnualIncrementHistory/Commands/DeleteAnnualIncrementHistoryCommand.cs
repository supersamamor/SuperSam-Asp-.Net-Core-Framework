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

namespace CTI.ELMS.Application.Features.ELMS.AnnualIncrementHistory.Commands;

public record DeleteAnnualIncrementHistoryCommand : BaseCommand, IRequest<Validation<Error, AnnualIncrementHistoryState>>;

public class DeleteAnnualIncrementHistoryCommandHandler : BaseCommandHandler<ApplicationContext, AnnualIncrementHistoryState, DeleteAnnualIncrementHistoryCommand>, IRequestHandler<DeleteAnnualIncrementHistoryCommand, Validation<Error, AnnualIncrementHistoryState>>
{
    public DeleteAnnualIncrementHistoryCommandHandler(ApplicationContext context,
                                       IMapper mapper,
                                       CompositeValidator<DeleteAnnualIncrementHistoryCommand> validator) : base(context, mapper, validator)
    {
    }

    public async Task<Validation<Error, AnnualIncrementHistoryState>> Handle(DeleteAnnualIncrementHistoryCommand request, CancellationToken cancellationToken) =>
        await Validators.ValidateTAsync(request, cancellationToken).BindT(
            async request => await Delete(request.Id, cancellationToken));
}


public class DeleteAnnualIncrementHistoryCommandValidator : AbstractValidator<DeleteAnnualIncrementHistoryCommand>
{
    readonly ApplicationContext _context;

    public DeleteAnnualIncrementHistoryCommandValidator(ApplicationContext context)
    {
        _context = context;
        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<AnnualIncrementHistoryState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("AnnualIncrementHistory with id {PropertyValue} does not exists");
    }
}
