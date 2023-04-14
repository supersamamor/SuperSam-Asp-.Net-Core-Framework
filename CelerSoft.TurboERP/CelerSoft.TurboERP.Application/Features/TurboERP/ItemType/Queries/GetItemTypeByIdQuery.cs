using CelerSoft.Common.Core.Queries;
using CelerSoft.TurboERP.Core.TurboERP;
using CelerSoft.TurboERP.Infrastructure.Data;
using LanguageExt;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CelerSoft.TurboERP.Application.Features.TurboERP.ItemType.Queries;

public record GetItemTypeByIdQuery(string Id) : BaseQueryById(Id), IRequest<Option<ItemTypeState>>;

public class GetItemTypeByIdQueryHandler : BaseQueryByIdHandler<ApplicationContext, ItemTypeState, GetItemTypeByIdQuery>, IRequestHandler<GetItemTypeByIdQuery, Option<ItemTypeState>>
{
    public GetItemTypeByIdQueryHandler(ApplicationContext context) : base(context)
    {
    }
		
}
