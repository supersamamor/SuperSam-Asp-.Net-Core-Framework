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

public record AddMailRecipientCommand : MailRecipientState, IRequest<Validation<Error, MailRecipientState>>;

public class AddMailRecipientCommandHandler : BaseCommandHandler<ApplicationContext, MailRecipientState, AddMailRecipientCommand>, IRequestHandler<AddMailRecipientCommand, Validation<Error, MailRecipientState>>
{
	private readonly IdentityContext _identityContext;
    public AddMailRecipientCommandHandler(ApplicationContext context,
                                    IMapper mapper,
                                    CompositeValidator<AddMailRecipientCommand> validator,
									IdentityContext identityContext) : base(context, mapper, validator)
    {
		_identityContext = identityContext;
    }

    
public async Task<Validation<Error, MailRecipientState>> Handle(AddMailRecipientCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await Add(request, cancellationToken));
	
	
	
}

public class AddMailRecipientCommandValidator : AbstractValidator<AddMailRecipientCommand>
{
    readonly ApplicationContext _context;

    public AddMailRecipientCommandValidator(ApplicationContext context)
    {
        _context = context;

        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.NotExists<MailRecipientState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("MailRecipient with id {PropertyValue} already exists");
        
    }
}
