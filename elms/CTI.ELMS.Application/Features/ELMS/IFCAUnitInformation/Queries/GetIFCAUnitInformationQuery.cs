using CTI.Common.Core.Queries;
using CTI.Common.Utility.Models;
using CTI.ELMS.Core.ELMS;
using CTI.ELMS.Infrastructure.Data;
using MediatR;
using CTI.Common.Utility.Extensions;
using Microsoft.EntityFrameworkCore;

namespace CTI.ELMS.Application.Features.ELMS.IFCAUnitInformation.Queries;

public record GetIFCAUnitInformationQuery : BaseQuery, IRequest<PagedListResponse<IFCAUnitInformationState>>;

public class GetIFCAUnitInformationQueryHandler : BaseQueryHandler<ApplicationContext, IFCAUnitInformationState, GetIFCAUnitInformationQuery>, IRequestHandler<GetIFCAUnitInformationQuery, PagedListResponse<IFCAUnitInformationState>>
{
    public GetIFCAUnitInformationQueryHandler(ApplicationContext context) : base(context)
    {
    }
	public override async Task<PagedListResponse<IFCAUnitInformationState>> Handle(GetIFCAUnitInformationQuery request, CancellationToken cancellationToken = default) =>
		await Context.Set<IFCAUnitInformationState>().Include(l=>l.Unit).Include(l=>l.IFCATenantInformation)
		.AsNoTracking().ToPagedResponse(request.SearchColumns, request.SearchValue,
			request.SortColumn, request.SortOrder,
			request.PageNumber, request.PageSize,
			cancellationToken);	
}
