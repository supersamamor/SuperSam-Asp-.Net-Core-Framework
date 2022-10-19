using CompanyNamePlaceHolder.Common.Core.Queries;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Core.AreaPlaceHolder;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Infrastructure.Data;
using LanguageExt;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Features.ProjectNamePlaceHolder.SubDetailItem.Queries;

public record GetSubDetailItemByIdQuery(string Id) : BaseQueryById(Id), IRequest<Option<SubDetailItemState>>;

public class GetSubDetailItemByIdQueryHandler : BaseQueryByIdHandler<ApplicationContext, SubDetailItemState, GetSubDetailItemByIdQuery>, IRequestHandler<GetSubDetailItemByIdQuery, Option<SubDetailItemState>>
{
    public GetSubDetailItemByIdQueryHandler(ApplicationContext context) : base(context)
    {
    }
	
	public override async Task<Option<SubDetailItemState>> Handle(GetSubDetailItemByIdQuery request, CancellationToken cancellationToken = default)
	{
		return await Context.SubDetailItem.Include(l=>l.MainModule)
			.Where(e => e.Id == request.Id).AsNoTracking().FirstOrDefaultAsync(cancellationToken);
	}
	
}
