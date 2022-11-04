using CTI.Common.Core.Queries;
using CTI.ELMS.Core.ELMS;
using CTI.ELMS.Infrastructure.Data;
using LanguageExt;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CTI.ELMS.Application.Features.ELMS.IFCATenantInformation.Queries;

public record GetIFCATenantInformationByIdQuery(string Id) : BaseQueryById(Id), IRequest<Option<IFCATenantInformationState>>;

public class GetIFCATenantInformationByIdQueryHandler : BaseQueryByIdHandler<ApplicationContext, IFCATenantInformationState, GetIFCATenantInformationByIdQuery>, IRequestHandler<GetIFCATenantInformationByIdQuery, Option<IFCATenantInformationState>>
{
    public GetIFCATenantInformationByIdQueryHandler(ApplicationContext context) : base(context)
    {
    }
	
	public override async Task<Option<IFCATenantInformationState>> Handle(GetIFCATenantInformationByIdQuery request, CancellationToken cancellationToken = default)
	{
		return await Context.IFCATenantInformation.Include(l=>l.Offering).Include(l=>l.Project)
			.Where(e => e.Id == request.Id).AsNoTracking().FirstOrDefaultAsync(cancellationToken);
	}
	
}
