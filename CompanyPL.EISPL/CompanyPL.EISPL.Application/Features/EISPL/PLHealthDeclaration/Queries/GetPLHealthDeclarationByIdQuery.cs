using CompanyPL.Common.Core.Queries;
using CompanyPL.EISPL.Core.EISPL;
using CompanyPL.EISPL.Infrastructure.Data;
using LanguageExt;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CompanyPL.EISPL.Application.Features.EISPL.PLHealthDeclaration.Queries;

public record GetPLHealthDeclarationByIdQuery(string Id) : BaseQueryById(Id), IRequest<Option<PLHealthDeclarationState>>;

public class GetPLHealthDeclarationByIdQueryHandler : BaseQueryByIdHandler<ApplicationContext, PLHealthDeclarationState, GetPLHealthDeclarationByIdQuery>, IRequestHandler<GetPLHealthDeclarationByIdQuery, Option<PLHealthDeclarationState>>
{
    public GetPLHealthDeclarationByIdQueryHandler(ApplicationContext context) : base(context)
    {
    }
	
	public override async Task<Option<PLHealthDeclarationState>> Handle(GetPLHealthDeclarationByIdQuery request, CancellationToken cancellationToken = default)
	{
		return await Context.PLHealthDeclaration.Include(l=>l.PLEmployee)
			.Where(e => e.Id == request.Id).AsNoTracking().FirstOrDefaultAsync(cancellationToken);
	}
	
}
