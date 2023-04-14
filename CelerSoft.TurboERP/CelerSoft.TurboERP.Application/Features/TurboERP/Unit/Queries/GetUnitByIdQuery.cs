using CelerSoft.Common.Core.Queries;
using CelerSoft.TurboERP.Core.TurboERP;
using CelerSoft.TurboERP.Infrastructure.Data;
using LanguageExt;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CelerSoft.TurboERP.Application.Features.TurboERP.Unit.Queries;

public record GetUnitByIdQuery(string Id) : BaseQueryById(Id), IRequest<Option<UnitState>>;

public class GetUnitByIdQueryHandler : BaseQueryByIdHandler<ApplicationContext, UnitState, GetUnitByIdQuery>, IRequestHandler<GetUnitByIdQuery, Option<UnitState>>
{
    public GetUnitByIdQueryHandler(ApplicationContext context) : base(context)
    {
    }
		
}
