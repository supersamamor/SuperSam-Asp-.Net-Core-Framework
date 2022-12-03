using CTI.Common.Core.Queries;
using CTI.Common.Utility.Models;
using CTI.FAS.Core.FAS;
using CTI.FAS.Infrastructure.Data;
using MediatR;
using CTI.Common.Utility.Extensions;
using Microsoft.EntityFrameworkCore;

namespace CTI.FAS.Application.Features.FAS.DatabaseConnectionSetup.Queries;

public record GetDatabaseConnectionSetupQuery : BaseQuery, IRequest<PagedListResponse<DatabaseConnectionSetupState>>;

public class GetDatabaseConnectionSetupQueryHandler : BaseQueryHandler<ApplicationContext, DatabaseConnectionSetupState, GetDatabaseConnectionSetupQuery>, IRequestHandler<GetDatabaseConnectionSetupQuery, PagedListResponse<DatabaseConnectionSetupState>>
{
    public GetDatabaseConnectionSetupQueryHandler(ApplicationContext context) : base(context)
    {
    }
		
}
