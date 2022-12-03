using AutoMapper;
using CTI.Common.Core.Commands;
using CTI.Common.Data;
using CTI.Common.Utility.Validators;
using CTI.FAS.Core.FAS;
using CTI.FAS.Infrastructure.Data;
using FluentValidation;
using LanguageExt;
using LanguageExt.Common;
using MediatR;

namespace CTI.FAS.Application.Features.FAS.Creditor.Commands;

public record DeleteCreditorCommand : BaseCommand, IRequest<Validation<Error, CreditorState>>;

public class DeleteCreditorCommandHandler : BaseCommandHandler<ApplicationContext, CreditorState, DeleteCreditorCommand>, IRequestHandler<DeleteCreditorCommand, Validation<Error, CreditorState>>
{
    public DeleteCreditorCommandHandler(ApplicationContext context,
                                       IMapper mapper,
                                       CompositeValidator<DeleteCreditorCommand> validator) : base(context, mapper, validator)
    {
    }

    public async Task<Validation<Error, CreditorState>> Handle(DeleteCreditorCommand request, CancellationToken cancellationToken) =>
        await Validators.ValidateTAsync(request, cancellationToken).BindT(
            async request => await Delete(request.Id, cancellationToken));
}


public class DeleteCreditorCommandValidator : AbstractValidator<DeleteCreditorCommand>
{
    readonly ApplicationContext _context;

    public DeleteCreditorCommandValidator(ApplicationContext context)
    {
        _context = context;
        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<CreditorState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("Creditor with id {PropertyValue} does not exists");
    }
}
