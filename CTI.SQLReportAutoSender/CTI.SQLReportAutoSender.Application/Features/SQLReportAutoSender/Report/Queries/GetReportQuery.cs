using CTI.Common.Core.Queries;
using CTI.Common.Utility.Models;
using CTI.SQLReportAutoSender.Core.SQLReportAutoSender;
using CTI.SQLReportAutoSender.Infrastructure.Data;
using MediatR;
using CTI.Common.Utility.Extensions;
using Microsoft.EntityFrameworkCore;
using CTI.Common.Identity.Abstractions;

namespace CTI.SQLReportAutoSender.Application.Features.SQLReportAutoSender.Report.Queries;

public record GetReportQuery : BaseQuery, IRequest<PagedListResponse<ReportState>>;

public class GetReportQueryHandler : BaseQueryHandler<ApplicationContext, ReportState, GetReportQuery>, IRequestHandler<GetReportQuery, PagedListResponse<ReportState>>
{
    private readonly IAuthenticatedUser _authenticatedUser;
    public GetReportQueryHandler(ApplicationContext context, IAuthenticatedUser authenticatedUser) : base(context)
    {
        _authenticatedUser = authenticatedUser;
    }
    public override async Task<PagedListResponse<ReportState>> Handle(GetReportQuery request, CancellationToken cancellationToken = default)
    {
        var query = Context.Set<ReportState>().Include(l => l.ScheduleFrequency)
                        .AsNoTracking();
        if (_authenticatedUser.ClaimsPrincipal != null && !_authenticatedUser.ClaimsPrincipal.IsInRole(Core.Constants.Roles.Admin))
        {
            query = query.Where(l => l.CreatedBy == _authenticatedUser.UserId);
        }
        return await query.ToPagedResponse(request.SearchColumns, request.SearchValue,
                            request.SortColumn, request.SortOrder,
                            request.PageNumber, request.PageSize,
                            cancellationToken);
    }

}
