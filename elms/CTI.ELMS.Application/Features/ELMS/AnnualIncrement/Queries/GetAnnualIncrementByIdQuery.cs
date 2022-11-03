using CTI.Common.Core.Queries;
using CTI.ELMS.Core.ELMS;
using CTI.ELMS.Infrastructure.Data;
using LanguageExt;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CTI.ELMS.Application.Features.ELMS.AnnualIncrement.Queries;

public record GetAnnualIncrementByIdQuery(string Id) : BaseQueryById(Id), IRequest<Option<AnnualIncrementState>>;

public class GetAnnualIncrementByIdQueryHandler : BaseQueryByIdHandler<ApplicationContext, AnnualIncrementState, GetAnnualIncrementByIdQuery>, IRequestHandler<GetAnnualIncrementByIdQuery, Option<AnnualIncrementState>>
{
    public GetAnnualIncrementByIdQueryHandler(ApplicationContext context) : base(context)
    {
    }
	
	public override async Task<Option<AnnualIncrementState>> Handle(GetAnnualIncrementByIdQuery request, CancellationToken cancellationToken = default)
	{
		return await Context.AnnualIncrement.Include(l=>l.UnitOffered)
			.Where(e => e.Id == request.Id).AsNoTracking().FirstOrDefaultAsync(cancellationToken);
	}
	
}
