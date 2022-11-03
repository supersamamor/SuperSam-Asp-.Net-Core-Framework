using CTI.Common.Core.Queries;
using CTI.Common.Utility.Models;
using CTI.ELMS.Core.ELMS;
using CTI.ELMS.Infrastructure.Data;
using MediatR;
using CTI.Common.Utility.Extensions;
using Microsoft.EntityFrameworkCore;

namespace CTI.ELMS.Application.Features.ELMS.Contact.Queries;

public record GetContactQuery : BaseQuery, IRequest<PagedListResponse<ContactState>>;

public class GetContactQueryHandler : BaseQueryHandler<ApplicationContext, ContactState, GetContactQuery>, IRequestHandler<GetContactQuery, PagedListResponse<ContactState>>
{
    public GetContactQueryHandler(ApplicationContext context) : base(context)
    {
    }
	public override async Task<PagedListResponse<ContactState>> Handle(GetContactQuery request, CancellationToken cancellationToken = default) =>
		await Context.Set<ContactState>().Include(l=>l.Lead)
		.AsNoTracking().ToPagedResponse(request.SearchColumns, request.SearchValue,
			request.SortColumn, request.SortOrder,
			request.PageNumber, request.PageSize,
			cancellationToken);	
}
