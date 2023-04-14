using CelerSoft.Common.Core.Queries;
using CelerSoft.Common.Utility.Models;
using CelerSoft.TurboERP.Core.TurboERP;
using CelerSoft.TurboERP.Infrastructure.Data;
using MediatR;
using CelerSoft.Common.Utility.Extensions;
using Microsoft.EntityFrameworkCore;

namespace CelerSoft.TurboERP.Application.Features.TurboERP.ItemType.Queries;

public record GetItemTypeQuery : BaseQuery, IRequest<PagedListResponse<ItemTypeState>>;

public class GetItemTypeQueryHandler : BaseQueryHandler<ApplicationContext, ItemTypeState, GetItemTypeQuery>, IRequestHandler<GetItemTypeQuery, PagedListResponse<ItemTypeState>>
{
    public GetItemTypeQueryHandler(ApplicationContext context) : base(context)
    {
    }
		
}
