using CelerSoft.Common.Core.Queries;
using CelerSoft.Common.Utility.Models;
using CelerSoft.TurboERP.Core.TurboERP;
using CelerSoft.TurboERP.Infrastructure.Data;
using MediatR;
using CelerSoft.Common.Utility.Extensions;
using Microsoft.EntityFrameworkCore;

namespace CelerSoft.TurboERP.Application.Features.TurboERP.PurchaseRequisition.Queries;

public record GetPurchaseRequisitionQuery : BaseQuery, IRequest<PagedListResponse<PurchaseRequisitionState>>;

public class GetPurchaseRequisitionQueryHandler : BaseQueryHandler<ApplicationContext, PurchaseRequisitionState, GetPurchaseRequisitionQuery>, IRequestHandler<GetPurchaseRequisitionQuery, PagedListResponse<PurchaseRequisitionState>>
{
    public GetPurchaseRequisitionQueryHandler(ApplicationContext context) : base(context)
    {
    }
		
}
