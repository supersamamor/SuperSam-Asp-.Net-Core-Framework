using CompanyPL.Common.Core.Queries;
using CompanyPL.EISPL.Core.EISPL;
using CompanyPL.EISPL.Infrastructure.Data;
using LanguageExt;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CompanyPL.EISPL.Application.Features.EISPL.PLEmployee.Queries;

public record GetPLEmployeeByIdQuery(string Id) : BaseQueryById(Id), IRequest<Option<PLEmployeeState>>;

public class GetPLEmployeeByIdQueryHandler : BaseQueryByIdHandler<ApplicationContext, PLEmployeeState, GetPLEmployeeByIdQuery>, IRequestHandler<GetPLEmployeeByIdQuery, Option<PLEmployeeState>>
{
    public GetPLEmployeeByIdQueryHandler(ApplicationContext context) : base(context)
    {
    }
	
	public override async Task<Option<PLEmployeeState>> Handle(GetPLEmployeeByIdQuery request, CancellationToken cancellationToken = default)
	{
		return await Context.PLEmployee
			.Include(l=>l.PLContactInformationList)
			.Include(l=>l.PLHealthDeclarationList)
			.Where(e => e.Id == request.Id).AsNoTracking().FirstOrDefaultAsync(cancellationToken);
	}
	
}
