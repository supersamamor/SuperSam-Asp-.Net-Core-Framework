using CTI.Common.Core.Queries;
using CTI.ELMS.Core.ELMS;
using CTI.ELMS.Infrastructure.Data;
using LanguageExt;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CTI.ELMS.Application.Features.ELMS.ClientFeedback.Queries;

public record GetClientFeedbackByIdQuery(string Id) : BaseQueryById(Id), IRequest<Option<ClientFeedbackState>>;

public class GetClientFeedbackByIdQueryHandler : BaseQueryByIdHandler<ApplicationContext, ClientFeedbackState, GetClientFeedbackByIdQuery>, IRequestHandler<GetClientFeedbackByIdQuery, Option<ClientFeedbackState>>
{
    public GetClientFeedbackByIdQueryHandler(ApplicationContext context) : base(context)
    {
    }
	
	public override async Task<Option<ClientFeedbackState>> Handle(GetClientFeedbackByIdQuery request, CancellationToken cancellationToken = default)
	{
		return await Context.ClientFeedback
			.Include(l=>l.LeadTaskClientFeedBackList)
			.Include(l=>l.LeadTaskNextStepList)
			.Include(l=>l.ActivityHistoryList)
			.Where(e => e.Id == request.Id).AsNoTracking().FirstOrDefaultAsync(cancellationToken);
	}
	
}
