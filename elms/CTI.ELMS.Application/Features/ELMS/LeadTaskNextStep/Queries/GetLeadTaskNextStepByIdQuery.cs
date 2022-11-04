using CTI.Common.Core.Queries;
using CTI.ELMS.Core.ELMS;
using CTI.ELMS.Infrastructure.Data;
using LanguageExt;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CTI.ELMS.Application.Features.ELMS.LeadTaskNextStep.Queries;

public record GetLeadTaskNextStepByIdQuery(string Id) : BaseQueryById(Id), IRequest<Option<LeadTaskNextStepState>>;

public class GetLeadTaskNextStepByIdQueryHandler : BaseQueryByIdHandler<ApplicationContext, LeadTaskNextStepState, GetLeadTaskNextStepByIdQuery>, IRequestHandler<GetLeadTaskNextStepByIdQuery, Option<LeadTaskNextStepState>>
{
    public GetLeadTaskNextStepByIdQueryHandler(ApplicationContext context) : base(context)
    {
    }
	
	public override async Task<Option<LeadTaskNextStepState>> Handle(GetLeadTaskNextStepByIdQuery request, CancellationToken cancellationToken = default)
	{
		return await Context.LeadTaskNextStep.Include(l=>l.ClientFeedback).Include(l=>l.NextStep).Include(l=>l.LeadTask)
			.Where(e => e.Id == request.Id).AsNoTracking().FirstOrDefaultAsync(cancellationToken);
	}
	
}
