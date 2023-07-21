using CompanyPL.Common.Core.Queries;
using CompanyPL.Common.Utility.Models;
using CompanyPL.EISPL.Core.EISPL;
using CompanyPL.EISPL.Infrastructure.Data;
using MediatR;
using CompanyPL.Common.Utility.Extensions;
using Microsoft.EntityFrameworkCore;

namespace CompanyPL.EISPL.Application.Features.EISPL.PLContactInformation.Queries;

public record GetPLContactInformationQuery : BaseQuery, IRequest<PagedListResponse<PLContactInformationState>>;

public class GetPLContactInformationQueryHandler : BaseQueryHandler<ApplicationContext, PLContactInformationState, GetPLContactInformationQuery>, IRequestHandler<GetPLContactInformationQuery, PagedListResponse<PLContactInformationState>>
{
    public GetPLContactInformationQueryHandler(ApplicationContext context) : base(context)
    {
    }
	public override async Task<PagedListResponse<PLContactInformationState>> Handle(GetPLContactInformationQuery request, CancellationToken cancellationToken = default) =>
		await Context.Set<PLContactInformationState>().Include(l=>l.PLEmployee)
		.AsNoTracking().ToPagedResponse(request.SearchColumns, request.SearchValue,
			request.SortColumn, request.SortOrder,
			request.PageNumber, request.PageSize,
			cancellationToken);	
}
