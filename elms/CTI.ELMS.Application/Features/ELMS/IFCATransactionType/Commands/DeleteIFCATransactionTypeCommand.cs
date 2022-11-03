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

namespace CTI.ELMS.Application.Features.ELMS.IFCATransactionType.Commands;

public record DeleteIFCATransactionTypeCommand : BaseCommand, IRequest<Validation<Error, IFCATransactionTypeState>>;

public class DeleteIFCATransactionTypeCommandHandler : BaseCommandHandler<ApplicationContext, IFCATransactionTypeState, DeleteIFCATransactionTypeCommand>, IRequestHandler<DeleteIFCATransactionTypeCommand, Validation<Error, IFCATransactionTypeState>>
{
    public DeleteIFCATransactionTypeCommandHandler(ApplicationContext context,
                                       IMapper mapper,
                                       CompositeValidator<DeleteIFCATransactionTypeCommand> validator) : base(context, mapper, validator)
    {
    }

    public async Task<Validation<Error, IFCATransactionTypeState>> Handle(DeleteIFCATransactionTypeCommand request, CancellationToken cancellationToken) =>
        await Validators.ValidateTAsync(request, cancellationToken).BindT(
            async request => await Delete(request.Id, cancellationToken));
}


public class DeleteIFCATransactionTypeCommandValidator : AbstractValidator<DeleteIFCATransactionTypeCommand>
{
    readonly ApplicationContext _context;

    public DeleteIFCATransactionTypeCommandValidator(ApplicationContext context)
    {
        _context = context;
        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<IFCATransactionTypeState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("IFCATransactionType with id {PropertyValue} does not exists");
    }
}
