using CTI.Common.Core.Queries;
using CTI.ELMS.Core.ELMS;
using CTI.ELMS.Infrastructure.Data;
using LanguageExt;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CTI.ELMS.Application.Features.ELMS.LeadSource.Queries;

public record GetLeadSourceByIdQuery(string Id) : BaseQueryById(Id), IRequest<Option<LeadSourceState>>;

public class GetLeadSourceByIdQueryHandler : BaseQueryByIdHandler<ApplicationContext, LeadSourceState, GetLeadSourceByIdQuery>, IRequestHandler<GetLeadSourceByIdQuery, Option<LeadSourceState>>
{
    public GetLeadSourceByIdQueryHandler(ApplicationContext context) : base(context)
    {
    }
		
}
