using CTI.Common.Core.Queries;
using CTI.Common.Utility.Models;
using CTI.ELMS.Core.ELMS;
using CTI.ELMS.Infrastructure.Data;
using MediatR;
using CTI.Common.Utility.Extensions;
using Microsoft.EntityFrameworkCore;

namespace CTI.ELMS.Application.Features.ELMS.IFCATenantInformation.Queries;

public record GetIFCATenantInformationQuery : BaseQuery, IRequest<PagedListResponse<IFCATenantInformationState>>;

public class GetIFCATenantInformationQueryHandler : BaseQueryHandler<ApplicationContext, IFCATenantInformationState, GetIFCATenantInformationQuery>, IRequestHandler<GetIFCATenantInformationQuery, PagedListResponse<IFCATenantInformationState>>
{
    public GetIFCATenantInformationQueryHandler(ApplicationContext context) : base(context)
    {
    }
	public override async Task<PagedListResponse<IFCATenantInformationState>> Handle(GetIFCATenantInformationQuery request, CancellationToken cancellationToken = default) =>
		await Context.Set<IFCATenantInformationState>().Include(l=>l.Offering).Include(l=>l.Project)
		.AsNoTracking().ToPagedResponse(request.SearchColumns, request.SearchValue,
			request.SortColumn, request.SortOrder,
			request.PageNumber, request.PageSize,
			cancellationToken);	
}
