using CTI.Common.Core.Queries;
using CTI.Common.Utility.Models;
using CTI.FAS.Core.FAS;
using CTI.FAS.Infrastructure.Data;
using MediatR;
using CTI.Common.Utility.Extensions;
using Microsoft.EntityFrameworkCore;

namespace CTI.FAS.Application.Features.FAS.UserEntity.Queries;

public record GetUserEntityQuery : BaseQuery, IRequest<PagedListResponse<UserEntityState>>;

public class GetUserEntityQueryHandler : BaseQueryHandler<ApplicationContext, UserEntityState, GetUserEntityQuery>, IRequestHandler<GetUserEntityQuery, PagedListResponse<UserEntityState>>
{
    public GetUserEntityQueryHandler(ApplicationContext context) : base(context)
    {
    }
	public override async Task<PagedListResponse<UserEntityState>> Handle(GetUserEntityQuery request, CancellationToken cancellationToken = default) =>
		await Context.Set<UserEntityState>().Include(l=>l.Company)
		.AsNoTracking().ToPagedResponse(request.SearchColumns, request.SearchValue,
			request.SortColumn, request.SortOrder,
			request.PageNumber, request.PageSize,
			cancellationToken);	
}
