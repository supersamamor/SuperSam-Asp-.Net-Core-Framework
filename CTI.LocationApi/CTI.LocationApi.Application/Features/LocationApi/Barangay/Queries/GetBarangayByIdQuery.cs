using CTI.Common.Core.Queries;
using CTI.LocationApi.Core.LocationApi;
using CTI.LocationApi.Infrastructure.Data;
using LanguageExt;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CTI.LocationApi.Application.Features.LocationApi.Barangay.Queries;

public record GetBarangayByIdQuery(string Id) : BaseQueryById(Id), IRequest<Option<BarangayState>>;

public class GetBarangayByIdQueryHandler : BaseQueryByIdHandler<ApplicationContext, BarangayState, GetBarangayByIdQuery>, IRequestHandler<GetBarangayByIdQuery, Option<BarangayState>>
{
    public GetBarangayByIdQueryHandler(ApplicationContext context) : base(context)
    {
    }
	
	public override async Task<Option<BarangayState>> Handle(GetBarangayByIdQuery request, CancellationToken cancellationToken = default)
	{
		return await Context.Barangay.Include(l=>l.City)
			.Where(e => e.Id == request.Id).AsNoTracking().FirstOrDefaultAsync(cancellationToken);
	}
	
}
