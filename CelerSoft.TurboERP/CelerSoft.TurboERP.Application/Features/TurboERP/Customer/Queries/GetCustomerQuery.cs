using CelerSoft.Common.Core.Queries;
using CelerSoft.Common.Utility.Models;
using CelerSoft.TurboERP.Core.TurboERP;
using CelerSoft.TurboERP.Infrastructure.Data;
using MediatR;
using CelerSoft.Common.Utility.Extensions;
using Microsoft.EntityFrameworkCore;

namespace CelerSoft.TurboERP.Application.Features.TurboERP.Customer.Queries;

public record GetCustomerQuery : BaseQuery, IRequest<PagedListResponse<CustomerState>>;

public class GetCustomerQueryHandler : BaseQueryHandler<ApplicationContext, CustomerState, GetCustomerQuery>, IRequestHandler<GetCustomerQuery, PagedListResponse<CustomerState>>
{
    public GetCustomerQueryHandler(ApplicationContext context) : base(context)
    {
    }
		
}
