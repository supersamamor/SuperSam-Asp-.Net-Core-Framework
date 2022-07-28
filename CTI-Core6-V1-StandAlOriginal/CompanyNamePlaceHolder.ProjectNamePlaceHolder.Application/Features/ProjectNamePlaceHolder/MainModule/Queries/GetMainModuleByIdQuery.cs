using CompanyNamePlaceHolder.Common.Core.Queries;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Core.ProjectNamePlaceHolder;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Infrastructure.Data;
using LanguageExt;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Features.ProjectNamePlaceHolder.MainModule.Queries;

public record GetMainModuleByIdQuery(string Id) : BaseQueryById(Id), IRequest<Option<MainModuleState>>;

public class GetMainModuleByIdQueryHandler : BaseQueryByIdHandler<ApplicationContext, MainModuleState, GetMainModuleByIdQuery>, IRequestHandler<GetMainModuleByIdQuery, Option<MainModuleState>>
{
    public GetMainModuleByIdQueryHandler(ApplicationContext context) : base(context)
    {
    }
	
	public override async Task<Option<MainModuleState>> Handle(GetMainModuleByIdQuery request, CancellationToken cancellationToken = default)
	{
		return await Context.MainModule.Include(l=>l.ParentModule)
			.Include(l=>l.SubDetailItemList)
			.Include(l=>l.SubDetailListList)
			.Where(e => e.Id == request.Id).AsNoTracking().FirstOrDefaultAsync(cancellationToken);
	}
	
}
