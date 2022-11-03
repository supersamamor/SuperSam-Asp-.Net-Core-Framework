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

public record EditReportTableYTDExpirySummaryCommand : ReportTableYTDExpirySummaryState, IRequest<Validation<Error, ReportTableYTDExpirySummaryState>>;

public class EditReportTableYTDExpirySummaryCommandHandler : BaseCommandHandler<ApplicationContext, ReportTableYTDExpirySummaryState, EditReportTableYTDExpirySummaryCommand>, IRequestHandler<EditReportTableYTDExpirySummaryCommand, Validation<Error, ReportTableYTDExpirySummaryState>>
{
    public EditReportTableYTDExpirySummaryCommandHandler(ApplicationContext context,
                                     IMapper mapper,
                                     CompositeValidator<EditReportTableYTDExpirySummaryCommand> validator) : base(context, mapper, validator)
    {
    }

    
public async Task<Validation<Error, ReportTableYTDExpirySummaryState>> Handle(EditReportTableYTDExpirySummaryCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await Edit(request, cancellationToken));
	
	
}

public class EditReportTableYTDExpirySummaryCommandValidator : AbstractValidator<EditReportTableYTDExpirySummaryCommand>
{
    readonly ApplicationContext _context;

    public EditReportTableYTDExpirySummaryCommandValidator(ApplicationContext context)
    {
        _context = context;
		RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<ReportTableYTDExpirySummaryState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("ReportTableYTDExpirySummary with id {PropertyValue} does not exists");
        
    }
}
