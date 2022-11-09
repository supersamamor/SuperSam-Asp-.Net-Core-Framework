using CTI.Common.Core.Queries;
using CTI.Common.Utility.Models;
using CTI.ELMS.Core.ELMS;
using CTI.ELMS.Infrastructure.Data;
using MediatR;
using CTI.Common.Utility.Extensions;
using Microsoft.EntityFrameworkCore;

namespace CTI.ELMS.Application.Features.ELMS.Lead.Queries;

public record GetLeadQuery : BaseQuery, IRequest<PagedListResponse<LeadState>>;

public class GetLeadQueryHandler : BaseQueryHandler<ApplicationContext, LeadState, GetLeadQuery>, IRequestHandler<GetLeadQuery, PagedListResponse<LeadState>>
{
    public GetLeadQueryHandler(ApplicationContext context) : base(context)
    {
    }
    public override async Task<PagedListResponse<LeadState>> Handle(GetLeadQuery request, CancellationToken cancellationToken = default) =>
        await Context.Set<LeadState>().Include(l => l.BusinessNatureCategory)
        .Include(l => l.BusinessNatureSubItem)
        .Include(l => l.LeadSource)
        .Include(l => l.BusinessNature)
        .Include(l => l.OperationType)
        .Include(l => l.LeadTouchPoint)
        .Include(l => l.ContactList)
        .Include(l => l.ContactPersonList)
        .AsNoTracking().ToPagedResponse(request.SearchColumns, request.SearchValue,
            request.SortColumn, request.SortOrder,
            request.PageNumber, request.PageSize,
            cancellationToken);
}
