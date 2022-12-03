using CTI.Common.Core.Queries;
using CTI.Common.Utility.Models;
using CTI.FAS.Core.FAS;
using CTI.FAS.Infrastructure.Data;
using MediatR;
using CTI.Common.Utility.Extensions;
using Microsoft.EntityFrameworkCore;

namespace CTI.FAS.Application.Features.FAS.CreditorEmail.Queries;

public record GetCreditorEmailQuery : BaseQuery, IRequest<PagedListResponse<CreditorEmailState>>;

public class GetCreditorEmailQueryHandler : BaseQueryHandler<ApplicationContext, CreditorEmailState, GetCreditorEmailQuery>, IRequestHandler<GetCreditorEmailQuery, PagedListResponse<CreditorEmailState>>
{
    public GetCreditorEmailQueryHandler(ApplicationContext context) : base(context)
    {
    }
	public override async Task<PagedListResponse<CreditorEmailState>> Handle(GetCreditorEmailQuery request, CancellationToken cancellationToken = default) =>
		await Context.Set<CreditorEmailState>().Include(l=>l.Creditor)
		.AsNoTracking().ToPagedResponse(request.SearchColumns, request.SearchValue,
			request.SortColumn, request.SortOrder,
			request.PageNumber, request.PageSize,
			cancellationToken);	
}
