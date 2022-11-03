using CTI.Common.Core.Queries;
using CTI.ELMS.Core.ELMS;
using CTI.ELMS.Infrastructure.Data;
using LanguageExt;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CTI.ELMS.Application.Features.ELMS.LeadTaskClientFeedBack.Queries;

public record GetLeadTaskClientFeedBackByIdQuery(string Id) : BaseQueryById(Id), IRequest<Option<LeadTaskClientFeedBackState>>;

public class GetLeadTaskClientFeedBackByIdQueryHandler : BaseQueryByIdHandler<ApplicationContext, LeadTaskClientFeedBackState, GetLeadTaskClientFeedBackByIdQuery>, IRequestHandler<GetLeadTaskClientFeedBackByIdQuery, Option<LeadTaskClientFeedBackState>>
{
    public GetLeadTaskClientFeedBackByIdQueryHandler(ApplicationContext context) : base(context)
    {
    }
	
	public override async Task<Option<LeadTaskClientFeedBackState>> Handle(GetLeadTaskClientFeedBackByIdQuery request, CancellationToken cancellationToken = default)
	{
		return await Context.LeadTaskClientFeedBack.Include(l=>l.ClientFeedback).Include(l=>l.LeadTask)
			.Where(e => e.Id == request.Id).AsNoTracking().FirstOrDefaultAsync(cancellationToken);
	}
	
}
