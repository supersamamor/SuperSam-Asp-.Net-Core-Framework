using CTI.Common.Core.Queries;
using CTI.ELMS.Core.ELMS;
using CTI.ELMS.Infrastructure.Data;
using LanguageExt;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CTI.ELMS.Application.Features.ELMS.PPlusConnectionSetup.Queries;

public record GetPPlusConnectionSetupByIdQuery(string Id) : BaseQueryById(Id), IRequest<Option<PPlusConnectionSetupState>>;

public class GetPPlusConnectionSetupByIdQueryHandler : BaseQueryByIdHandler<ApplicationContext, PPlusConnectionSetupState, GetPPlusConnectionSetupByIdQuery>, IRequestHandler<GetPPlusConnectionSetupByIdQuery, Option<PPlusConnectionSetupState>>
{
    public GetPPlusConnectionSetupByIdQueryHandler(ApplicationContext context) : base(context)
    {
    }
		
}
