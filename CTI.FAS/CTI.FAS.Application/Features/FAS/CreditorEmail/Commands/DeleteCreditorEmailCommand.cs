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

namespace CTI.FAS.Application.Features.FAS.CreditorEmail.Commands;

public record DeleteCreditorEmailCommand : BaseCommand, IRequest<Validation<Error, CreditorEmailState>>;

public class DeleteCreditorEmailCommandHandler : BaseCommandHandler<ApplicationContext, CreditorEmailState, DeleteCreditorEmailCommand>, IRequestHandler<DeleteCreditorEmailCommand, Validation<Error, CreditorEmailState>>
{
    public DeleteCreditorEmailCommandHandler(ApplicationContext context,
                                       IMapper mapper,
                                       CompositeValidator<DeleteCreditorEmailCommand> validator) : base(context, mapper, validator)
    {
    }

    public async Task<Validation<Error, CreditorEmailState>> Handle(DeleteCreditorEmailCommand request, CancellationToken cancellationToken) =>
        await Validators.ValidateTAsync(request, cancellationToken).BindT(
            async request => await Delete(request.Id, cancellationToken));
}


public class DeleteCreditorEmailCommandValidator : AbstractValidator<DeleteCreditorEmailCommand>
{
    readonly ApplicationContext _context;

    public DeleteCreditorEmailCommandValidator(ApplicationContext context)
    {
        _context = context;
        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<CreditorEmailState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("CreditorEmail with id {PropertyValue} does not exists");
    }
}
