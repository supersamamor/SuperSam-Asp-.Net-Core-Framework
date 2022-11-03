using CTI.Common.Core.Queries;
using CTI.ELMS.Core.ELMS;
using CTI.ELMS.Infrastructure.Data;
using LanguageExt;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CTI.ELMS.Application.Features.ELMS.LeadTouchPoint.Queries;

public record GetLeadTouchPointByIdQuery(string Id) : BaseQueryById(Id), IRequest<Option<LeadTouchPointState>>;

public class GetLeadTouchPointByIdQueryHandler : BaseQueryByIdHandler<ApplicationContext, LeadTouchPointState, GetLeadTouchPointByIdQuery>, IRequestHandler<GetLeadTouchPointByIdQuery, Option<LeadTouchPointState>>
{
    public GetLeadTouchPointByIdQueryHandler(ApplicationContext context) : base(context)
    {
    }
		
}
