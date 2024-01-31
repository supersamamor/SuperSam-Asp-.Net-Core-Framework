using CompanyPL.Common.Core.Queries;
using CompanyPL.Common.Utility.Models;
using CompanyPL.ProjectPL.Core.ProjectPL;
using CompanyPL.ProjectPL.Infrastructure.Data;
using MediatR;
using CompanyPL.Common.Utility.Extensions;
using Microsoft.EntityFrameworkCore;

namespace CompanyPL.ProjectPL.Application.Features.ProjectPL.Employee.Queries;

public record GetEmployeeQuery : BaseQuery, IRequest<PagedListResponse<EmployeeState>>;

public class GetEmployeeQueryHandler : BaseQueryHandler<ApplicationContext, EmployeeState, GetEmployeeQuery>, IRequestHandler<GetEmployeeQuery, PagedListResponse<EmployeeState>>
{
    public GetEmployeeQueryHandler(ApplicationContext context) : base(context)
    {
    }
		
}
