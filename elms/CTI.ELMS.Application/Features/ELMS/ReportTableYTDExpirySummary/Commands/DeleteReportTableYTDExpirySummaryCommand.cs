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

namespace CTI.ELMS.Application.Features.ELMS.ReportTableYTDExpirySummary.Commands;

public record DeleteReportTableYTDExpirySummaryCommand : BaseCommand, IRequest<Validation<Error, ReportTableYTDExpirySummaryState>>;

public class DeleteReportTableYTDExpirySummaryCommandHandler : BaseCommandHandler<ApplicationContext, ReportTableYTDExpirySummaryState, DeleteReportTableYTDExpirySummaryCommand>, IRequestHandler<DeleteReportTableYTDExpirySummaryCommand, Validation<Error, ReportTableYTDExpirySummaryState>>
{
    public DeleteReportTableYTDExpirySummaryCommandHandler(ApplicationContext context,
                                       IMapper mapper,
                                       CompositeValidator<DeleteReportTableYTDExpirySummaryCommand> validator) : base(context, mapper, validator)
    {
    }

    public async Task<Validation<Error, ReportTableYTDExpirySummaryState>> Handle(DeleteReportTableYTDExpirySummaryCommand request, CancellationToken cancellationToken) =>
        await Validators.ValidateTAsync(request, cancellationToken).BindT(
            async request => await Delete(request.Id, cancellationToken));
}


public class DeleteReportTableYTDExpirySummaryCommandValidator : AbstractValidator<DeleteReportTableYTDExpirySummaryCommand>
{
    readonly ApplicationContext _context;

    public DeleteReportTableYTDExpirySummaryCommandValidator(ApplicationContext context)
    {
        _context = context;
        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<ReportTableYTDExpirySummaryState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("ReportTableYTDExpirySummary with id {PropertyValue} does not exists");
    }
}
