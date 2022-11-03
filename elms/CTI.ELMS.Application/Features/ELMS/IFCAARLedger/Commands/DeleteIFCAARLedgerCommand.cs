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

namespace CTI.ELMS.Application.Features.ELMS.IFCAARLedger.Commands;

public record DeleteIFCAARLedgerCommand : BaseCommand, IRequest<Validation<Error, IFCAARLedgerState>>;

public class DeleteIFCAARLedgerCommandHandler : BaseCommandHandler<ApplicationContext, IFCAARLedgerState, DeleteIFCAARLedgerCommand>, IRequestHandler<DeleteIFCAARLedgerCommand, Validation<Error, IFCAARLedgerState>>
{
    public DeleteIFCAARLedgerCommandHandler(ApplicationContext context,
                                       IMapper mapper,
                                       CompositeValidator<DeleteIFCAARLedgerCommand> validator) : base(context, mapper, validator)
    {
    }

    public async Task<Validation<Error, IFCAARLedgerState>> Handle(DeleteIFCAARLedgerCommand request, CancellationToken cancellationToken) =>
        await Validators.ValidateTAsync(request, cancellationToken).BindT(
            async request => await Delete(request.Id, cancellationToken));
}


public class DeleteIFCAARLedgerCommandValidator : AbstractValidator<DeleteIFCAARLedgerCommand>
{
    readonly ApplicationContext _context;

    public DeleteIFCAARLedgerCommandValidator(ApplicationContext context)
    {
        _context = context;
        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<IFCAARLedgerState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("IFCAARLedger with id {PropertyValue} does not exists");
    }
}
