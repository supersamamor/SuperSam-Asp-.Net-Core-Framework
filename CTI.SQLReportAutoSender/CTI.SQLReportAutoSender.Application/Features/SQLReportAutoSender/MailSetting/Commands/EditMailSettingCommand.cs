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

public record EditMailSettingCommand : MailSettingState, IRequest<Validation<Error, MailSettingState>>;

public class EditMailSettingCommandHandler : BaseCommandHandler<ApplicationContext, MailSettingState, EditMailSettingCommand>, IRequestHandler<EditMailSettingCommand, Validation<Error, MailSettingState>>
{
    public EditMailSettingCommandHandler(ApplicationContext context,
                                     IMapper mapper,
                                     CompositeValidator<EditMailSettingCommand> validator) : base(context, mapper, validator)
    {
    }

    
public async Task<Validation<Error, MailSettingState>> Handle(EditMailSettingCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await Edit(request, cancellationToken));
	
	
}

public class EditMailSettingCommandValidator : AbstractValidator<EditMailSettingCommand>
{
    readonly ApplicationContext _context;

    public EditMailSettingCommandValidator(ApplicationContext context)
    {
        _context = context;
		RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<MailSettingState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("MailSetting with id {PropertyValue} does not exists");
        
    }
}
