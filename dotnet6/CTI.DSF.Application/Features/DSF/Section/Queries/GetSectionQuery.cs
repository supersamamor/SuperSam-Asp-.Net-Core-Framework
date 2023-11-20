using CTI.Common.Core.Queries;
using CTI.Common.Utility.Models;
using CTI.DSF.Core.DSF;
using CTI.DSF.Infrastructure.Data;
using MediatR;
using CTI.Common.Utility.Extensions;
using Microsoft.EntityFrameworkCore;

namespace CTI.DSF.Application.Features.DSF.Section.Queries;

public record GetSectionQuery : BaseQuery, IRequest<PagedListResponse<SectionState>>;

public class GetSectionQueryHandler : BaseQueryHandler<ApplicationContext, SectionState, GetSectionQuery>, IRequestHandler<GetSectionQuery, PagedListResponse<SectionState>>
{
    public GetSectionQueryHandler(ApplicationContext context) : base(context)
    {
    }
	public override async Task<PagedListResponse<SectionState>> Handle(GetSectionQuery request, CancellationToken cancellationToken = default) =>
		await Context.Set<SectionState>().Include(l=>l.Department)
		.AsNoTracking().ToPagedResponse(request.SearchColumns, request.SearchValue,
			request.SortColumn, request.SortOrder,
			request.PageNumber, request.PageSize,
			cancellationToken);	
}
