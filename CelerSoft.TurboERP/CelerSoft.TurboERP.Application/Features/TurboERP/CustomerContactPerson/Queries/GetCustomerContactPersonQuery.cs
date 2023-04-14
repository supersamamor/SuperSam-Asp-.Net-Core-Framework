using CelerSoft.Common.Core.Queries;
using CelerSoft.Common.Utility.Models;
using CelerSoft.TurboERP.Core.TurboERP;
using CelerSoft.TurboERP.Infrastructure.Data;
using MediatR;
using CelerSoft.Common.Utility.Extensions;
using Microsoft.EntityFrameworkCore;

namespace CelerSoft.TurboERP.Application.Features.TurboERP.CustomerContactPerson.Queries;

public record GetCustomerContactPersonQuery : BaseQuery, IRequest<PagedListResponse<CustomerContactPersonState>>;

public class GetCustomerContactPersonQueryHandler : BaseQueryHandler<ApplicationContext, CustomerContactPersonState, GetCustomerContactPersonQuery>, IRequestHandler<GetCustomerContactPersonQuery, PagedListResponse<CustomerContactPersonState>>
{
    public GetCustomerContactPersonQueryHandler(ApplicationContext context) : base(context)
    {
    }
	public override async Task<PagedListResponse<CustomerContactPersonState>> Handle(GetCustomerContactPersonQuery request, CancellationToken cancellationToken = default) =>
		await Context.Set<CustomerContactPersonState>().Include(l=>l.Customer)
		.AsNoTracking().ToPagedResponse(request.SearchColumns, request.SearchValue,
			request.SortColumn, request.SortOrder,
			request.PageNumber, request.PageSize,
			cancellationToken);	
}
