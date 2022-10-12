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

public record AddReportInboxCommand : ReportInboxState, IRequest<Validation<Error, ReportInboxState>>;

public class AddReportInboxCommandHandler : BaseCommandHandler<ApplicationContext, ReportInboxState, AddReportInboxCommand>, IRequestHandler<AddReportInboxCommand, Validation<Error, ReportInboxState>>
{
	private readonly IdentityContext _identityContext;
    public AddReportInboxCommandHandler(ApplicationContext context,
                                    IMapper mapper,
                                    CompositeValidator<AddReportInboxCommand> validator,
									IdentityContext identityContext) : base(context, mapper, validator)
    {
		_identityContext = identityContext;
    }

    
public async Task<Validation<Error, ReportInboxState>> Handle(AddReportInboxCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await Add(request, cancellationToken));
	
	
	
}

public class AddReportInboxCommandValidator : AbstractValidator<AddReportInboxCommand>
{
    readonly ApplicationContext _context;

    public AddReportInboxCommandValidator(ApplicationContext context)
    {
        _context = context;

        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.NotExists<ReportInboxState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("ReportInbox with id {PropertyValue} already exists");
        
    }
}
