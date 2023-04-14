using CelerSoft.TurboERP.Infrastructure.Data;
using CelerSoft.Common.Core.Queries;
using CelerSoft.Common.Utility.Extensions;
using CelerSoft.Common.Utility.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace CelerSoft.TurboERP.Web.Areas.Admin.Queries.Roles;

public record GetApproverRolesQuery(string CurrentSelectedApprover, IList<string> AllSelectedApprovers) : BaseQuery, IRequest<PagedListResponse<IdentityRole>>
{
}

public class GetApproverRolesQueryHandler : IRequestHandler<GetApproverRolesQuery, PagedListResponse<IdentityRole>>
{
    private readonly IdentityContext _context;

    public GetApproverRolesQueryHandler(IdentityContext context)
    {
        _context = context;
    }

    public async Task<PagedListResponse<IdentityRole>> Handle(GetApproverRolesQuery request, CancellationToken cancellationToken)
    {
        var excludedUsers = request.AllSelectedApprovers.Where(l => l != request.CurrentSelectedApprover);
        var query = _context.Roles.Where(l => !excludedUsers.Contains(l.Id)).AsNoTracking();
        return await query.ToPagedResponse(request.SearchColumns, request.SearchValue,
                                                       request.SortColumn, request.SortOrder,
                                                       request.PageNumber, request.PageSize, cancellationToken);
    }
}
