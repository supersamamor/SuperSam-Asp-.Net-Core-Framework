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

namespace CTI.ELMS.Application.Features.ELMS.OfferingHistory.Commands;

public record DeleteOfferingHistoryCommand : BaseCommand, IRequest<Validation<Error, OfferingHistoryState>>;

public class DeleteOfferingHistoryCommandHandler : BaseCommandHandler<ApplicationContext, OfferingHistoryState, DeleteOfferingHistoryCommand>, IRequestHandler<DeleteOfferingHistoryCommand, Validation<Error, OfferingHistoryState>>
{
    public DeleteOfferingHistoryCommandHandler(ApplicationContext context,
                                       IMapper mapper,
                                       CompositeValidator<DeleteOfferingHistoryCommand> validator) : base(context, mapper, validator)
    {
    }

    public async Task<Validation<Error, OfferingHistoryState>> Handle(DeleteOfferingHistoryCommand request, CancellationToken cancellationToken) =>
        await Validators.ValidateTAsync(request, cancellationToken).BindT(
            async request => await Delete(request.Id, cancellationToken));
}


public class DeleteOfferingHistoryCommandValidator : AbstractValidator<DeleteOfferingHistoryCommand>
{
    readonly ApplicationContext _context;

    public DeleteOfferingHistoryCommandValidator(ApplicationContext context)
    {
        _context = context;
        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<OfferingHistoryState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("OfferingHistory with id {PropertyValue} does not exists");
    }
}
