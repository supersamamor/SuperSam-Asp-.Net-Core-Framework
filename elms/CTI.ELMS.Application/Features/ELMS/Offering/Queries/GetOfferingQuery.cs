using CTI.Common.Core.Queries;
using CTI.Common.Utility.Models;
using CTI.ELMS.Core.ELMS;
using CTI.ELMS.Infrastructure.Data;
using MediatR;
using CTI.Common.Utility.Extensions;
using Microsoft.EntityFrameworkCore;
using CTI.Common.Identity.Abstractions;

namespace CTI.ELMS.Application.Features.ELMS.Offering.Queries;

public record GetOfferingQuery : BaseQuery, IRequest<PagedListResponse<OfferingState>>
{
    public string? LeadId { get; set; }
}
public class GetOfferingQueryHandler : BaseQueryHandler<ApplicationContext, OfferingState, GetOfferingQuery>, IRequestHandler<GetOfferingQuery, PagedListResponse<OfferingState>>
{
    private readonly IAuthenticatedUser _authenticatedUser;
    public GetOfferingQueryHandler(ApplicationContext context, IAuthenticatedUser authenticatedUser) : base(context)
    {
        _authenticatedUser = authenticatedUser;
    }

    public override async Task<PagedListResponse<OfferingState>> Handle(GetOfferingQuery request, CancellationToken cancellationToken = default)
    {
        var query = (from offering in Context.Offering
                     select offering)
                             .AsNoTracking();
        if (!string.IsNullOrEmpty(request.LeadId))
        {
            query = query.Where(l => l.LeadID == request.LeadId);
        }
        if (_authenticatedUser.ClaimsPrincipal != null && !_authenticatedUser.ClaimsPrincipal.IsInRole(Core.Constants.Roles.Admin))
        {
            query = from a in query
                    join p in Context.Project on a.ProjectID equals p.Id
                    join ap in Context.UserProjectAssignment on p.Id equals ap.ProjectID
                    where ap.UserId == _authenticatedUser.UserId
                    select a;
        }
        return await query.Include(l => l.Project)
                            .Include(l => l.Lead)
                            .Include(l => l.Project)
                         .ToPagedResponse(request.SearchColumns, request.SearchValue,
                          request.SortColumn, request.SortOrder,
                          request.PageNumber, request.PageSize,
                          cancellationToken);
    }
}
