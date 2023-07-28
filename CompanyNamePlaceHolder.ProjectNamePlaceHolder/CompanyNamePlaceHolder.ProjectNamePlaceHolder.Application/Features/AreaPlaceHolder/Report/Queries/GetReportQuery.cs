using CompanyNamePlaceHolder.Common.Core.Queries;
using CompanyNamePlaceHolder.Common.Utility.Models;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Core.ProjectNamePlaceHolder;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Infrastructure.Data;
using MediatR;
using CompanyNamePlaceHolder.Common.Utility.Extensions;
using Microsoft.EntityFrameworkCore;

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Features.AreaPlaceHolder.Report.Queries;

public record GetReportQuery : BaseQuery, IRequest<PagedListResponse<ReportState>>;

public class GetReportQueryHandler : BaseQueryHandler<ApplicationContext, ReportState, GetReportQuery>, IRequestHandler<GetReportQuery, PagedListResponse<ReportState>>
{
    public GetReportQueryHandler(ApplicationContext context) : base(context)
    {
    }
		
}
