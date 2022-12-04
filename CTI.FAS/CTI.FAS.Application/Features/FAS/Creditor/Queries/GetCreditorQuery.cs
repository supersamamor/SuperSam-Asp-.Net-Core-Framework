using CTI.Common.Core.Queries;
using CTI.Common.Utility.Models;
using CTI.FAS.Core.FAS;
using CTI.FAS.Infrastructure.Data;
using MediatR;
using CTI.Common.Utility.Extensions;
using Microsoft.EntityFrameworkCore;

namespace CTI.FAS.Application.Features.FAS.Creditor.Queries;

public record GetCreditorQuery : BaseQuery, IRequest<PagedListResponse<CreditorState>>;

public class GetCreditorQueryHandler : BaseQueryHandler<ApplicationContext, CreditorState, GetCreditorQuery>, IRequestHandler<GetCreditorQuery, PagedListResponse<CreditorState>>
{
    public GetCreditorQueryHandler(ApplicationContext context) : base(context)
    {
    }
	public override async Task<PagedListResponse<CreditorState>> Handle(GetCreditorQuery request, CancellationToken cancellationToken = default) =>
		await Context.Set<CreditorState>().Include(l=>l.DatabaseConnectionSetup)
		.AsNoTracking().ToPagedResponse(request.SearchColumns, request.SearchValue,
			request.SortColumn, request.SortOrder,
			request.PageNumber, request.PageSize,
			cancellationToken);	
}
