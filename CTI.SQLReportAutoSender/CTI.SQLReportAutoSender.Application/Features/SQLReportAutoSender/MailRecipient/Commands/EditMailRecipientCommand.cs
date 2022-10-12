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
using Microsoft.EntityFrameworkCore;
using static LanguageExt.Prelude;

namespace CTI.SQLReportAutoSender.Application.Features.SQLReportAutoSender.MailRecipient.Commands;

public record EditMailRecipientCommand : MailRecipientState, IRequest<Validation<Error, MailRecipientState>>;

public class EditMailRecipientCommandHandler : BaseCommandHandler<ApplicationContext, MailRecipientState, EditMailRecipientCommand>, IRequestHandler<EditMailRecipientCommand, Validation<Error, MailRecipientState>>
{
    public EditMailRecipientCommandHandler(ApplicationContext context,
                                     IMapper mapper,
                                     CompositeValidator<EditMailRecipientCommand> validator) : base(context, mapper, validator)
    {
    }

    
public async Task<Validation<Error, MailRecipientState>> Handle(EditMailRecipientCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await Edit(request, cancellationToken));
	
	
}

public class EditMailRecipientCommandValidator : AbstractValidator<EditMailRecipientCommand>
{
    readonly ApplicationContext _context;

    public EditMailRecipientCommandValidator(ApplicationContext context)
    {
        _context = context;
		RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<MailRecipientState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("MailRecipient with id {PropertyValue} does not exists");
        
    }
}
