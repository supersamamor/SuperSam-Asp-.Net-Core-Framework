using CompanyNamePlaceHolder.Common.Core.Queries;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Core.ProjectNamePlaceHolder;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Infrastructure.Data;
using LanguageExt;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Features.ProjectNamePlaceHolder.MainModulePlaceHolder.Queries;

public record GetMainModulePlaceHolderByIdQuery(string Id) : BaseQueryById(Id), IRequest<Option<MainModulePlaceHolderState>>;

public class GetMainModulePlaceHolderByIdQueryHandler : BaseQueryByIdHandler<ApplicationContext, MainModulePlaceHolderState, GetMainModulePlaceHolderByIdQuery>, IRequestHandler<GetMainModulePlaceHolderByIdQuery, Option<MainModulePlaceHolderState>>
{
    public GetMainModulePlaceHolderByIdQueryHandler(ApplicationContext context) : base(context)
    {
    }
	
	public override async Task<Option<MainModulePlaceHolderState>> Handle(GetMainModulePlaceHolderByIdQuery request, CancellationToken cancellationToken = default)
	{
		return await _context.MainModulePlaceHolder
			.Include(l=>l.SubDetailItemPlaceHolderList)
			.Include(l=>l.SubDetailListPlaceHolderList)
			.Where(e => e.Id == request.Id).AsNoTracking().FirstOrDefaultAsync(cancellationToken);
	}
	
}
