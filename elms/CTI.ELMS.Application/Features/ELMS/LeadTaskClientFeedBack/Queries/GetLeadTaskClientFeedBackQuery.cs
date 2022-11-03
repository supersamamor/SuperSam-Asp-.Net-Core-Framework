using CTI.Common.Core.Queries;
using CTI.Common.Utility.Models;
using CTI.ELMS.Core.ELMS;
using CTI.ELMS.Infrastructure.Data;
using MediatR;
using CTI.Common.Utility.Extensions;
using Microsoft.EntityFrameworkCore;

namespace CTI.ELMS.Application.Features.ELMS.LeadTaskClientFeedBack.Queries;

public record GetLeadTaskClientFeedBackQuery : BaseQuery, IRequest<PagedListResponse<LeadTaskClientFeedBackState>>;

public class GetLeadTaskClientFeedBackQueryHandler : BaseQueryHandler<ApplicationContext, LeadTaskClientFeedBackState, GetLeadTaskClientFeedBackQuery>, IRequestHandler<GetLeadTaskClientFeedBackQuery, PagedListResponse<LeadTaskClientFeedBackState>>
{
    public GetLeadTaskClientFeedBackQueryHandler(ApplicationContext context) : base(context)
    {
    }
	public override async Task<PagedListResponse<LeadTaskClientFeedBackState>> Handle(GetLeadTaskClientFeedBackQuery request, CancellationToken cancellationToken = default) =>
		await Context.Set<LeadTaskClientFeedBackState>().Include(l=>l.ClientFeedback).Include(l=>l.LeadTask)
		.AsNoTracking().ToPagedResponse(request.SearchColumns, request.SearchValue,
			request.SortColumn, request.SortOrder,
			request.PageNumber, request.PageSize,
			cancellationToken);	
}
