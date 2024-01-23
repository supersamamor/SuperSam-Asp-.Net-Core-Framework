using CompanyPL.Common.Core.Queries;
using CompanyPL.ProjectPL.Core.ProjectPL;
using CompanyPL.ProjectPL.Infrastructure.Data;
using LanguageExt;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CompanyPL.ProjectPL.Application.Features.ProjectPL.HealthDeclaration.Queries;

public record GetHealthDeclarationByIdQuery(string Id) : BaseQueryById(Id), IRequest<Option<HealthDeclarationState>>;

public class GetHealthDeclarationByIdQueryHandler : BaseQueryByIdHandler<ApplicationContext, HealthDeclarationState, GetHealthDeclarationByIdQuery>, IRequestHandler<GetHealthDeclarationByIdQuery, Option<HealthDeclarationState>>
{
    public GetHealthDeclarationByIdQueryHandler(ApplicationContext context) : base(context)
    {
    }
	
	public override async Task<Option<HealthDeclarationState>> Handle(GetHealthDeclarationByIdQuery request, CancellationToken cancellationToken = default)
	{
		return await Context.HealthDeclaration.Include(l=>l.Employee)
			.Where(e => e.Id == request.Id).AsNoTracking().FirstOrDefaultAsync(cancellationToken);
	}
	
}
