using CTI.Common.Core.Queries;
using CTI.ELMS.Core.ELMS;
using CTI.ELMS.Infrastructure.Data;
using LanguageExt;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CTI.ELMS.Application.Features.ELMS.IFCAUnitInformation.Queries;

public record GetIFCAUnitInformationByIdQuery(string Id) : BaseQueryById(Id), IRequest<Option<IFCAUnitInformationState>>;

public class GetIFCAUnitInformationByIdQueryHandler : BaseQueryByIdHandler<ApplicationContext, IFCAUnitInformationState, GetIFCAUnitInformationByIdQuery>, IRequestHandler<GetIFCAUnitInformationByIdQuery, Option<IFCAUnitInformationState>>
{
    public GetIFCAUnitInformationByIdQueryHandler(ApplicationContext context) : base(context)
    {
    }
	
	public override async Task<Option<IFCAUnitInformationState>> Handle(GetIFCAUnitInformationByIdQuery request, CancellationToken cancellationToken = default)
	{
		return await Context.IFCAUnitInformation.Include(l=>l.IFCATenantInformation).Include(l=>l.Unit)
			.Where(e => e.Id == request.Id).AsNoTracking().FirstOrDefaultAsync(cancellationToken);
	}
	
}
