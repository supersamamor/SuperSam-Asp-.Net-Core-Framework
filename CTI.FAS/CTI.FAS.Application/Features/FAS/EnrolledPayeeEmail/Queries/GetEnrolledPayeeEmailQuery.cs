using CTI.Common.Core.Queries;
using CTI.Common.Utility.Models;
using CTI.FAS.Core.FAS;
using CTI.FAS.Infrastructure.Data;
using MediatR;
using CTI.Common.Utility.Extensions;
using Microsoft.EntityFrameworkCore;

namespace CTI.FAS.Application.Features.FAS.EnrolledPayeeEmail.Queries;

public record GetEnrolledPayeeEmailQuery : BaseQuery, IRequest<PagedListResponse<EnrolledPayeeEmailState>>;

public class GetEnrolledPayeeEmailQueryHandler : BaseQueryHandler<ApplicationContext, EnrolledPayeeEmailState, GetEnrolledPayeeEmailQuery>, IRequestHandler<GetEnrolledPayeeEmailQuery, PagedListResponse<EnrolledPayeeEmailState>>
{
    public GetEnrolledPayeeEmailQueryHandler(ApplicationContext context) : base(context)
    {
    }
	public override async Task<PagedListResponse<EnrolledPayeeEmailState>> Handle(GetEnrolledPayeeEmailQuery request, CancellationToken cancellationToken = default) =>
		await Context.Set<EnrolledPayeeEmailState>().Include(l=>l.EnrolledPayee)
		.AsNoTracking().ToPagedResponse(request.SearchColumns, request.SearchValue,
			request.SortColumn, request.SortOrder,
			request.PageNumber, request.PageSize,
			cancellationToken);	
}
