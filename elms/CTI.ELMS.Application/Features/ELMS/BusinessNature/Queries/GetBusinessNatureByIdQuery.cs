using CTI.Common.Core.Queries;
using CTI.ELMS.Core.ELMS;
using CTI.ELMS.Infrastructure.Data;
using LanguageExt;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CTI.ELMS.Application.Features.ELMS.BusinessNature.Queries;

public record GetBusinessNatureByIdQuery(string Id) : BaseQueryById(Id), IRequest<Option<BusinessNatureState>>;

public class GetBusinessNatureByIdQueryHandler : BaseQueryByIdHandler<ApplicationContext, BusinessNatureState, GetBusinessNatureByIdQuery>, IRequestHandler<GetBusinessNatureByIdQuery, Option<BusinessNatureState>>
{
    public GetBusinessNatureByIdQueryHandler(ApplicationContext context) : base(context)
    {
    }
		
}
