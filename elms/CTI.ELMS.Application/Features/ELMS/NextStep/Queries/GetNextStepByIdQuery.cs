using CTI.Common.Core.Queries;
using CTI.ELMS.Core.ELMS;
using CTI.ELMS.Infrastructure.Data;
using LanguageExt;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CTI.ELMS.Application.Features.ELMS.NextStep.Queries;

public record GetNextStepByIdQuery(string Id) : BaseQueryById(Id), IRequest<Option<NextStepState>>;

public class GetNextStepByIdQueryHandler : BaseQueryByIdHandler<ApplicationContext, NextStepState, GetNextStepByIdQuery>, IRequestHandler<GetNextStepByIdQuery, Option<NextStepState>>
{
    public GetNextStepByIdQueryHandler(ApplicationContext context) : base(context)
    {
    }
	
	public override async Task<Option<NextStepState>> Handle(GetNextStepByIdQuery request, CancellationToken cancellationToken = default)
	{
		return await Context.NextStep
			.Include(l=>l.LeadTaskNextStepList)
			.Include(l=>l.ActivityHistoryList)
			.Where(e => e.Id == request.Id).AsNoTracking().FirstOrDefaultAsync(cancellationToken);
	}
	
}
