using CompanyPL.Common.Core.Queries;
using CompanyPL.ProjectPL.Core.ProjectPL;
using CompanyPL.ProjectPL.Infrastructure.Data;
using LanguageExt;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CompanyPL.ProjectPL.Application.Features.ProjectPL.Employee.Queries;

public record GetEmployeeByIdQuery(string Id) : BaseQueryById(Id), IRequest<Option<EmployeeState>>;

public class GetEmployeeByIdQueryHandler : BaseQueryByIdHandler<ApplicationContext, EmployeeState, GetEmployeeByIdQuery>, IRequestHandler<GetEmployeeByIdQuery, Option<EmployeeState>>
{
    public GetEmployeeByIdQueryHandler(ApplicationContext context) : base(context)
    {
    }
	
	public override async Task<Option<EmployeeState>> Handle(GetEmployeeByIdQuery request, CancellationToken cancellationToken = default)
	{
		return await Context.Employee
			.Include(l=>l.ContactInformationList)
			.Include(l=>l.HealthDeclarationList)
			.Where(e => e.Id == request.Id).AsNoTracking().FirstOrDefaultAsync(cancellationToken);
	}
	
}
