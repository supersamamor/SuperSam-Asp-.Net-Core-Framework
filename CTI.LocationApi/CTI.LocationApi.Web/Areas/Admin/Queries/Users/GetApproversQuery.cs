using CTI.LocationApi.Core.Identity;
using CTI.LocationApi.Infrastructure.Data;
using CTI.Common.Core.Queries;
using CTI.Common.Utility.Extensions;
using CTI.Common.Utility.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
namespace CTI.LocationApi.Web.Areas.Admin.Queries.Users;

public record GetApproversQuery(string CurrentSelectedApprover, IList<string> AllSelectedApprovers) : BaseQuery, IRequest<PagedListResponse<ApplicationUser>>
{
}

public class GetApproversQueryHandler : IRequestHandler<GetApproversQuery, PagedListResponse<ApplicationUser>>
{
    private readonly IdentityContext _context;

    public GetApproversQueryHandler(IdentityContext context)
    {
        _context = context;
    }

    public async Task<PagedListResponse<ApplicationUser>> Handle(GetApproversQuery request, CancellationToken cancellationToken)
    {
        var excludedUsers = request.AllSelectedApprovers.Where(l => l != request.CurrentSelectedApprover);
        var query = _context.Users.Where(l => !excludedUsers.Contains(l.Id)).AsNoTracking();
        return await query.ToPagedResponse(request.SearchColumns, request.SearchValue,
                                                       request.SortColumn, request.SortOrder,
                                                       request.PageNumber, request.PageSize, cancellationToken);
    }
}
