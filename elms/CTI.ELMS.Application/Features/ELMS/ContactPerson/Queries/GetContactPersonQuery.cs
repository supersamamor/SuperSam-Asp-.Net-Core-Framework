using CTI.Common.Core.Queries;
using CTI.Common.Utility.Models;
using CTI.ELMS.Core.ELMS;
using CTI.ELMS.Infrastructure.Data;
using MediatR;
using CTI.Common.Utility.Extensions;
using Microsoft.EntityFrameworkCore;

namespace CTI.ELMS.Application.Features.ELMS.ContactPerson.Queries;

public record GetContactPersonQuery : BaseQuery, IRequest<PagedListResponse<ContactPersonState>>;

public class GetContactPersonQueryHandler : BaseQueryHandler<ApplicationContext, ContactPersonState, GetContactPersonQuery>, IRequestHandler<GetContactPersonQuery, PagedListResponse<ContactPersonState>>
{
    public GetContactPersonQueryHandler(ApplicationContext context) : base(context)
    {
    }
	public override async Task<PagedListResponse<ContactPersonState>> Handle(GetContactPersonQuery request, CancellationToken cancellationToken = default) =>
		await Context.Set<ContactPersonState>().Include(l=>l.Salutation).Include(l=>l.Lead)
		.AsNoTracking().ToPagedResponse(request.SearchColumns, request.SearchValue,
			request.SortColumn, request.SortOrder,
			request.PageNumber, request.PageSize,
			cancellationToken);	
}
