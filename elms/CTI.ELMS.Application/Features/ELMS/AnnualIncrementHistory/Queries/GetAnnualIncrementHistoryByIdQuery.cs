using CTI.Common.Core.Queries;
using CTI.ELMS.Core.ELMS;
using CTI.ELMS.Infrastructure.Data;
using LanguageExt;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CTI.ELMS.Application.Features.ELMS.AnnualIncrementHistory.Queries;

public record GetAnnualIncrementHistoryByIdQuery(string Id) : BaseQueryById(Id), IRequest<Option<AnnualIncrementHistoryState>>;

public class GetAnnualIncrementHistoryByIdQueryHandler : BaseQueryByIdHandler<ApplicationContext, AnnualIncrementHistoryState, GetAnnualIncrementHistoryByIdQuery>, IRequestHandler<GetAnnualIncrementHistoryByIdQuery, Option<AnnualIncrementHistoryState>>
{
    public GetAnnualIncrementHistoryByIdQueryHandler(ApplicationContext context) : base(context)
    {
    }
	
	public override async Task<Option<AnnualIncrementHistoryState>> Handle(GetAnnualIncrementHistoryByIdQuery request, CancellationToken cancellationToken = default)
	{
		return await Context.AnnualIncrementHistory.Include(l=>l.UnitOfferedHistory)
			.Where(e => e.Id == request.Id).AsNoTracking().FirstOrDefaultAsync(cancellationToken);
	}
	
}
