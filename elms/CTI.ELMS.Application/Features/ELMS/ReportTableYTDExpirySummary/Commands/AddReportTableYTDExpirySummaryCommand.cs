using AutoMapper;
using CTI.Common.Core.Commands;
using CTI.Common.Data;
using CTI.Common.Utility.Validators;
using CTI.ELMS.Core.ELMS;
using CTI.ELMS.Infrastructure.Data;
using FluentValidation;
using LanguageExt;
using LanguageExt.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static LanguageExt.Prelude;

namespace CTI.ELMS.Application.Features.ELMS.ReportTableYTDExpirySummary.Commands;

public record AddReportTableYTDExpirySummaryCommand : ReportTableYTDExpirySummaryState, IRequest<Validation<Error, ReportTableYTDExpirySummaryState>>;

public class AddReportTableYTDExpirySummaryCommandHandler : BaseCommandHandler<ApplicationContext, ReportTableYTDExpirySummaryState, AddReportTableYTDExpirySummaryCommand>, IRequestHandler<AddReportTableYTDExpirySummaryCommand, Validation<Error, ReportTableYTDExpirySummaryState>>
{
	private readonly IdentityContext _identityContext;
    public AddReportTableYTDExpirySummaryCommandHandler(ApplicationContext context,
                                    IMapper mapper,
                                    CompositeValidator<AddReportTableYTDExpirySummaryCommand> validator,
									IdentityContext identityContext) : base(context, mapper, validator)
    {
		_identityContext = identityContext;
    }

    
public async Task<Validation<Error, ReportTableYTDExpirySummaryState>> Handle(AddReportTableYTDExpirySummaryCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await Add(request, cancellationToken));
	
	
	
}

public class AddReportTableYTDExpirySummaryCommandValidator : AbstractValidator<AddReportTableYTDExpirySummaryCommand>
{
    readonly ApplicationContext _context;

    public AddReportTableYTDExpirySummaryCommandValidator(ApplicationContext context)
    {
        _context = context;

        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.NotExists<ReportTableYTDExpirySummaryState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("ReportTableYTDExpirySummary with id {PropertyValue} already exists");
        
    }
}
