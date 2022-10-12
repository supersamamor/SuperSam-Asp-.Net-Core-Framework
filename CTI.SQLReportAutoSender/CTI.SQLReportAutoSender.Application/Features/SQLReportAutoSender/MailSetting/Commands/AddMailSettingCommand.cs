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

namespace CTI.SQLReportAutoSender.Application.Features.SQLReportAutoSender.MailSetting.Commands;

public record AddMailSettingCommand : MailSettingState, IRequest<Validation<Error, MailSettingState>>;

public class AddMailSettingCommandHandler : BaseCommandHandler<ApplicationContext, MailSettingState, AddMailSettingCommand>, IRequestHandler<AddMailSettingCommand, Validation<Error, MailSettingState>>
{
	private readonly IdentityContext _identityContext;
    public AddMailSettingCommandHandler(ApplicationContext context,
                                    IMapper mapper,
                                    CompositeValidator<AddMailSettingCommand> validator,
									IdentityContext identityContext) : base(context, mapper, validator)
    {
		_identityContext = identityContext;
    }

    
public async Task<Validation<Error, MailSettingState>> Handle(AddMailSettingCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await Add(request, cancellationToken));
	
	
	
}

public class AddMailSettingCommandValidator : AbstractValidator<AddMailSettingCommand>
{
    readonly ApplicationContext _context;

    public AddMailSettingCommandValidator(ApplicationContext context)
    {
        _context = context;

        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.NotExists<MailSettingState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("MailSetting with id {PropertyValue} already exists");
        
    }
}
