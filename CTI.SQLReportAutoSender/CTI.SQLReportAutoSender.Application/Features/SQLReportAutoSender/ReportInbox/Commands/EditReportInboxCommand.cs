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

namespace CTI.SQLReportAutoSender.Application.Features.SQLReportAutoSender.ReportInbox.Commands;

public record EditReportInboxCommand : ReportInboxState, IRequest<Validation<Error, ReportInboxState>>;

public class EditReportInboxCommandHandler : BaseCommandHandler<ApplicationContext, ReportInboxState, EditReportInboxCommand>, IRequestHandler<EditReportInboxCommand, Validation<Error, ReportInboxState>>
{
    public EditReportInboxCommandHandler(ApplicationContext context,
                                     IMapper mapper,
                                     CompositeValidator<EditReportInboxCommand> validator) : base(context, mapper, validator)
    {
    }

    
public async Task<Validation<Error, ReportInboxState>> Handle(EditReportInboxCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await Edit(request, cancellationToken));
	
	
}

public class EditReportInboxCommandValidator : AbstractValidator<EditReportInboxCommand>
{
    readonly ApplicationContext _context;

    public EditReportInboxCommandValidator(ApplicationContext context)
    {
        _context = context;
		RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<ReportInboxState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("ReportInbox with id {PropertyValue} does not exists");
        
    }
}
