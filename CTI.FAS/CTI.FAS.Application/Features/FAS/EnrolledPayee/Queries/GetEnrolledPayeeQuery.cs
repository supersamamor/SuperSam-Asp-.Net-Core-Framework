using CTI.Common.Core.Queries;
using CTI.Common.Utility.Models;
using CTI.FAS.Core.FAS;
using CTI.FAS.Infrastructure.Data;
using MediatR;
using CTI.Common.Utility.Extensions;
using Microsoft.EntityFrameworkCore;

namespace CTI.FAS.Application.Features.FAS.EnrolledPayee.Queries;

public record GetEnrolledPayeeQuery : BaseQuery, IRequest<PagedListResponse<EnrolledPayeeState>>;

public class GetEnrolledPayeeQueryHandler : BaseQueryHandler<ApplicationContext, EnrolledPayeeState, GetEnrolledPayeeQuery>, IRequestHandler<GetEnrolledPayeeQuery, PagedListResponse<EnrolledPayeeState>>
{
    public GetEnrolledPayeeQueryHandler(ApplicationContext context) : base(context)
    {
    }
	public override async Task<PagedListResponse<EnrolledPayeeState>> Handle(GetEnrolledPayeeQuery request, CancellationToken cancellationToken = default) =>
		await Context.Set<EnrolledPayeeState>().Include(l=>l.Company).Include(l=>l.Creditor)
		.AsNoTracking().ToPagedResponse(request.SearchColumns, request.SearchValue,
			request.SortColumn, request.SortOrder,
			request.PageNumber, request.PageSize,
			cancellationToken);	
}
