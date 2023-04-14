using CelerSoft.Common.Core.Queries;
using CelerSoft.Common.Utility.Models;
using CelerSoft.TurboERP.Core.TurboERP;
using CelerSoft.TurboERP.Infrastructure.Data;
using MediatR;
using CelerSoft.Common.Utility.Extensions;
using Microsoft.EntityFrameworkCore;

namespace CelerSoft.TurboERP.Application.Features.TurboERP.Unit.Queries;

public record GetUnitQuery : BaseQuery, IRequest<PagedListResponse<UnitState>>;

public class GetUnitQueryHandler : BaseQueryHandler<ApplicationContext, UnitState, GetUnitQuery>, IRequestHandler<GetUnitQuery, PagedListResponse<UnitState>>
{
    public GetUnitQueryHandler(ApplicationContext context) : base(context)
    {
    }
		
}
