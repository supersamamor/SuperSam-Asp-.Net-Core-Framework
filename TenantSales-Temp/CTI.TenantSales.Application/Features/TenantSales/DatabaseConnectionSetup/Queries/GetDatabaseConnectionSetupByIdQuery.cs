using CTI.Common.Core.Queries;
using CTI.TenantSales.Core.TenantSales;
using CTI.TenantSales.Infrastructure.Data;
using LanguageExt;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CTI.TenantSales.Application.Features.TenantSales.DatabaseConnectionSetup.Queries;

public record GetDatabaseConnectionSetupByIdQuery(string Id) : BaseQueryById(Id), IRequest<Option<DatabaseConnectionSetupState>>;

public class GetDatabaseConnectionSetupByIdQueryHandler : BaseQueryByIdHandler<ApplicationContext, DatabaseConnectionSetupState, GetDatabaseConnectionSetupByIdQuery>, IRequestHandler<GetDatabaseConnectionSetupByIdQuery, Option<DatabaseConnectionSetupState>>
{
    public GetDatabaseConnectionSetupByIdQueryHandler(ApplicationContext context) : base(context)
    {
    }
		
}
