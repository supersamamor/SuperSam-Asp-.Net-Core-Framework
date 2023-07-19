using CompanyPL.Common.Core.Queries;
using CompanyPL.Common.Utility.Models;
using CompanyPL.EISPL.Core.EISPL;
using CompanyPL.EISPL.Infrastructure.Data;
using MediatR;
using CompanyPL.Common.Utility.Extensions;
using Microsoft.EntityFrameworkCore;

namespace CompanyPL.EISPL.Application.Features.EISPL.PLEmployee.Queries;

public record GetPLEmployeeQuery : BaseQuery, IRequest<PagedListResponse<PLEmployeeState>>;

public class GetPLEmployeeQueryHandler : BaseQueryHandler<ApplicationContext, PLEmployeeState, GetPLEmployeeQuery>, IRequestHandler<GetPLEmployeeQuery, PagedListResponse<PLEmployeeState>>
{
    public GetPLEmployeeQueryHandler(ApplicationContext context) : base(context)
    {
    }
		
}
