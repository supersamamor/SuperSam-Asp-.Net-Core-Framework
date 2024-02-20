using CTI.Common.Core.Queries;
using CTI.DSF.Core.DSF;
using CTI.DSF.Infrastructure.Data;
using LanguageExt;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CTI.DSF.Application.Features.DSF.Department.Queries;

public record GetDepartmentByIdQuery(string Id) : BaseQueryById(Id), IRequest<Option<DepartmentState>>;

public class GetDepartmentByIdQueryHandler : BaseQueryByIdHandler<ApplicationContext, DepartmentState, GetDepartmentByIdQuery>, IRequestHandler<GetDepartmentByIdQuery, Option<DepartmentState>>
{
    public GetDepartmentByIdQueryHandler(ApplicationContext context) : base(context)
    {
    }
	
	public override async Task<Option<DepartmentState>> Handle(GetDepartmentByIdQuery request, CancellationToken cancellationToken = default)
	{
		return await Context.Department.Include(l=>l.Company)
			.Include(l=>l.TaskCompanyAssignmentList)
			.Where(e => e.Id == request.Id).AsNoTracking().FirstOrDefaultAsync(cancellationToken);
	}
	
}
