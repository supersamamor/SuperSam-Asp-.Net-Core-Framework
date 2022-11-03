using CTI.Common.Core.Queries;
using CTI.ELMS.Core.ELMS;
using CTI.ELMS.Infrastructure.Data;
using LanguageExt;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CTI.ELMS.Application.Features.ELMS.LeadTask.Queries;

public record GetLeadTaskByIdQuery(string Id) : BaseQueryById(Id), IRequest<Option<LeadTaskState>>;

public class GetLeadTaskByIdQueryHandler : BaseQueryByIdHandler<ApplicationContext, LeadTaskState, GetLeadTaskByIdQuery>, IRequestHandler<GetLeadTaskByIdQuery, Option<LeadTaskState>>
{
    public GetLeadTaskByIdQueryHandler(ApplicationContext context) : base(context)
    {
    }
	
	public override async Task<Option<LeadTaskState>> Handle(GetLeadTaskByIdQuery request, CancellationToken cancellationToken = default)
	{
		return await Context.LeadTask
			.Include(l=>l.ActivityHistoryList)
			.Where(e => e.Id == request.Id).AsNoTracking().FirstOrDefaultAsync(cancellationToken);
	}
	
}
