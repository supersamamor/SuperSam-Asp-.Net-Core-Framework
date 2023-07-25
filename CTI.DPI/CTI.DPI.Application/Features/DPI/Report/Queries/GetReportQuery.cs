using CTI.Common.Core.Queries;
using CTI.Common.Utility.Models;
using CTI.DPI.Core.DPI;
using CTI.DPI.Infrastructure.Data;
using MediatR;
using CTI.Common.Utility.Extensions;
using Microsoft.EntityFrameworkCore;

namespace CTI.DPI.Application.Features.DPI.Report.Queries;

public record GetReportQuery : BaseQuery, IRequest<PagedListResponse<ReportState>>;

public class GetReportQueryHandler : BaseQueryHandler<ApplicationContext, ReportState, GetReportQuery>, IRequestHandler<GetReportQuery, PagedListResponse<ReportState>>
{
    public GetReportQueryHandler(ApplicationContext context) : base(context)
    {
    }
		
}
