using CTI.Common.Core.Queries;
using CTI.Common.Utility.Models;
using CTI.ELMS.Core.ELMS;
using CTI.ELMS.Infrastructure.Data;
using MediatR;
using CTI.Common.Utility.Extensions;
using Microsoft.EntityFrameworkCore;

namespace CTI.ELMS.Application.Features.ELMS.PPlusConnectionSetup.Queries;

public record GetPPlusConnectionSetupQuery : BaseQuery, IRequest<PagedListResponse<PPlusConnectionSetupState>>;

public class GetPPlusConnectionSetupQueryHandler : BaseQueryHandler<ApplicationContext, PPlusConnectionSetupState, GetPPlusConnectionSetupQuery>, IRequestHandler<GetPPlusConnectionSetupQuery, PagedListResponse<PPlusConnectionSetupState>>
{
    public GetPPlusConnectionSetupQueryHandler(ApplicationContext context) : base(context)
    {
    }
		
}
