using CTI.Common.Core.Queries;
using CTI.Common.Utility.Models;
using CTI.ELMS.Core.ELMS;
using CTI.ELMS.Infrastructure.Data;
using MediatR;
using CTI.Common.Utility.Extensions;
using Microsoft.EntityFrameworkCore;

namespace CTI.ELMS.Application.Features.ELMS.ReportTableYTDExpirySummary.Queries;

public record GetReportTableYTDExpirySummaryQuery : BaseQuery, IRequest<PagedListResponse<ReportTableYTDExpirySummaryState>>;

public class GetReportTableYTDExpirySummaryQueryHandler : BaseQueryHandler<ApplicationContext, ReportTableYTDExpirySummaryState, GetReportTableYTDExpirySummaryQuery>, IRequestHandler<GetReportTableYTDExpirySummaryQuery, PagedListResponse<ReportTableYTDExpirySummaryState>>
{
    public GetReportTableYTDExpirySummaryQueryHandler(ApplicationContext context) : base(context)
    {
    }
		
}
