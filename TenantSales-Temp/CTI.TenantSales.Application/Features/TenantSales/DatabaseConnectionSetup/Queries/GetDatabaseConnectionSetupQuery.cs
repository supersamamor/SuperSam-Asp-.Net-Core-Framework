using CTI.Common.Core.Queries;
using CTI.Common.Utility.Models;
using CTI.TenantSales.Core.TenantSales;
using CTI.TenantSales.Infrastructure.Data;
using MediatR;
using CTI.Common.Utility.Extensions;
using Microsoft.EntityFrameworkCore;

namespace CTI.TenantSales.Application.Features.TenantSales.DatabaseConnectionSetup.Queries;

public record GetDatabaseConnectionSetupQuery : BaseQuery, IRequest<PagedListResponse<DatabaseConnectionSetupState>>;

public class GetDatabaseConnectionSetupQueryHandler : BaseQueryHandler<ApplicationContext, DatabaseConnectionSetupState, GetDatabaseConnectionSetupQuery>, IRequestHandler<GetDatabaseConnectionSetupQuery, PagedListResponse<DatabaseConnectionSetupState>>
{
    public GetDatabaseConnectionSetupQueryHandler(ApplicationContext context) : base(context)
    {
    }
		
}
