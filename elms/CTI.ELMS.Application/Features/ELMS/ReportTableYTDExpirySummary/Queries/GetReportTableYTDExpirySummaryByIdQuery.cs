using CTI.Common.Core.Queries;
using CTI.ELMS.Core.ELMS;
using CTI.ELMS.Infrastructure.Data;
using LanguageExt;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CTI.ELMS.Application.Features.ELMS.ReportTableYTDExpirySummary.Queries;

public record GetReportTableYTDExpirySummaryByIdQuery(string Id) : BaseQueryById(Id), IRequest<Option<ReportTableYTDExpirySummaryState>>;

public class GetReportTableYTDExpirySummaryByIdQueryHandler : BaseQueryByIdHandler<ApplicationContext, ReportTableYTDExpirySummaryState, GetReportTableYTDExpirySummaryByIdQuery>, IRequestHandler<GetReportTableYTDExpirySummaryByIdQuery, Option<ReportTableYTDExpirySummaryState>>
{
    public GetReportTableYTDExpirySummaryByIdQueryHandler(ApplicationContext context) : base(context)
    {
    }
		
}
