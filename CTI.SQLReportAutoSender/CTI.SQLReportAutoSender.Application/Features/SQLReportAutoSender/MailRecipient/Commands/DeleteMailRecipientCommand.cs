using AutoMapper;
using CTI.Common.Core.Commands;
using CTI.Common.Data;
using CTI.Common.Utility.Validators;
using CTI.SQLReportAutoSender.Core.SQLReportAutoSender;
using CTI.SQLReportAutoSender.Infrastructure.Data;
using FluentValidation;
using LanguageExt;
using LanguageExt.Common;
using MediatR;

namespace CTI.SQLReportAutoSender.Application.Features.SQLReportAutoSender.MailRecipient.Commands;

public record DeleteMailRecipientCommand : BaseCommand, IRequest<Validation<Error, MailRecipientState>>;

public class DeleteMailRecipientCommandHandler : BaseCommandHandler<ApplicationContext, MailRecipientState, DeleteMailRecipientCommand>, IRequestHandler<DeleteMailRecipientCommand, Validation<Error, MailRecipientState>>
{
    public DeleteMailRecipientCommandHandler(ApplicationContext context,
                                       IMapper mapper,
                                       CompositeValidator<DeleteMailRecipientCommand> validator) : base(context, mapper, validator)
    {
    }

    public async Task<Validation<Error, MailRecipientState>> Handle(DeleteMailRecipientCommand request, CancellationToken cancellationToken) =>
        await Validators.ValidateTAsync(request, cancellationToken).BindT(
            async request => await Delete(request.Id, cancellationToken));
}


public class DeleteMailRecipientCommandValidator : AbstractValidator<DeleteMailRecipientCommand>
{
    readonly ApplicationContext _context;

    public DeleteMailRecipientCommandValidator(ApplicationContext context)
    {
        _context = context;
        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<MailRecipientState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("MailRecipient with id {PropertyValue} does not exists");
    }
}
