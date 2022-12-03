using CTI.Common.Core.Queries;
using CTI.FAS.Core.FAS;
using CTI.FAS.Infrastructure.Data;
using LanguageExt;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CTI.FAS.Application.Features.FAS.DatabaseConnectionSetup.Queries;

public record GetDatabaseConnectionSetupByIdQuery(string Id) : BaseQueryById(Id), IRequest<Option<DatabaseConnectionSetupState>>;

public class GetDatabaseConnectionSetupByIdQueryHandler : BaseQueryByIdHandler<ApplicationContext, DatabaseConnectionSetupState, GetDatabaseConnectionSetupByIdQuery>, IRequestHandler<GetDatabaseConnectionSetupByIdQuery, Option<DatabaseConnectionSetupState>>
{
    public GetDatabaseConnectionSetupByIdQueryHandler(ApplicationContext context) : base(context)
    {
    }
		
}
