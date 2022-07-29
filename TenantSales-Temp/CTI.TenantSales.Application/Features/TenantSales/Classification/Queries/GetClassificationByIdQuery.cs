using CTI.Common.Core.Queries;
using CTI.TenantSales.Core.TenantSales;
using CTI.TenantSales.Infrastructure.Data;
using LanguageExt;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CTI.TenantSales.Application.Features.TenantSales.Classification.Queries;

public record GetClassificationByIdQuery(string Id) : BaseQueryById(Id), IRequest<Option<ClassificationState>>;

public class GetClassificationByIdQueryHandler : BaseQueryByIdHandler<ApplicationContext, ClassificationState, GetClassificationByIdQuery>, IRequestHandler<GetClassificationByIdQuery, Option<ClassificationState>>
{
    public GetClassificationByIdQueryHandler(ApplicationContext context) : base(context)
    {
    }
	
	public override async Task<Option<ClassificationState>> Handle(GetClassificationByIdQuery request, CancellationToken cancellationToken = default)
	{
		return await Context.Classification.Include(l=>l.Theme)
			.Include(l=>l.CategoryList)
			.Where(e => e.Id == request.Id).AsNoTracking().FirstOrDefaultAsync(cancellationToken);
	}
	
}
