using CTI.Common.Core.Queries;
using CTI.LocationApi.Core.LocationApi;
using CTI.LocationApi.Infrastructure.Data;
using LanguageExt;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CTI.LocationApi.Application.Features.LocationApi.Province.Queries;

public record GetProvinceByIdQuery(string Id) : BaseQueryById(Id), IRequest<Option<ProvinceState>>;

public class GetProvinceByIdQueryHandler : BaseQueryByIdHandler<ApplicationContext, ProvinceState, GetProvinceByIdQuery>, IRequestHandler<GetProvinceByIdQuery, Option<ProvinceState>>
{
    public GetProvinceByIdQueryHandler(ApplicationContext context) : base(context)
    {
    }
	
	public override async Task<Option<ProvinceState>> Handle(GetProvinceByIdQuery request, CancellationToken cancellationToken = default)
	{
		return await Context.Province.Include(l=>l.Region)
			.Where(e => e.Id == request.Id).AsNoTracking().FirstOrDefaultAsync(cancellationToken);
	}
	
}
