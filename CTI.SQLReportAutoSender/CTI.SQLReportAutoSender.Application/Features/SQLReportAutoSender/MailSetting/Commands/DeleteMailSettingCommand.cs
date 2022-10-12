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

namespace CTI.SQLReportAutoSender.Application.Features.SQLReportAutoSender.MailSetting.Commands;

public record DeleteMailSettingCommand : BaseCommand, IRequest<Validation<Error, MailSettingState>>;

public class DeleteMailSettingCommandHandler : BaseCommandHandler<ApplicationContext, MailSettingState, DeleteMailSettingCommand>, IRequestHandler<DeleteMailSettingCommand, Validation<Error, MailSettingState>>
{
    public DeleteMailSettingCommandHandler(ApplicationContext context,
                                       IMapper mapper,
                                       CompositeValidator<DeleteMailSettingCommand> validator) : base(context, mapper, validator)
    {
    }

    public async Task<Validation<Error, MailSettingState>> Handle(DeleteMailSettingCommand request, CancellationToken cancellationToken) =>
        await Validators.ValidateTAsync(request, cancellationToken).BindT(
            async request => await Delete(request.Id, cancellationToken));
}


public class DeleteMailSettingCommandValidator : AbstractValidator<DeleteMailSettingCommand>
{
    readonly ApplicationContext _context;

    public DeleteMailSettingCommandValidator(ApplicationContext context)
    {
        _context = context;
        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<MailSettingState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("MailSetting with id {PropertyValue} does not exists");
    }
}
