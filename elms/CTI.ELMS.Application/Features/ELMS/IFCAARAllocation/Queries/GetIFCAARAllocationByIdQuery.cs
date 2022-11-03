using CTI.Common.Core.Queries;
using CTI.ELMS.Core.ELMS;
using CTI.ELMS.Infrastructure.Data;
using LanguageExt;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CTI.ELMS.Application.Features.ELMS.IFCAARAllocation.Queries;

public record GetIFCAARAllocationByIdQuery(string Id) : BaseQueryById(Id), IRequest<Option<IFCAARAllocationState>>;

public class GetIFCAARAllocationByIdQueryHandler : BaseQueryByIdHandler<ApplicationContext, IFCAARAllocationState, GetIFCAARAllocationByIdQuery>, IRequestHandler<GetIFCAARAllocationByIdQuery, Option<IFCAARAllocationState>>
{
    public GetIFCAARAllocationByIdQueryHandler(ApplicationContext context) : base(context)
    {
    }
	
	public override async Task<Option<IFCAARAllocationState>> Handle(GetIFCAARAllocationByIdQuery request, CancellationToken cancellationToken = default)
	{
		return await Context.IFCAARAllocation.Include(l=>l.Project)
			.Where(e => e.Id == request.Id).AsNoTracking().FirstOrDefaultAsync(cancellationToken);
	}
	
}
