using CTI.Common.Core.Queries;
using CTI.Common.Utility.Models;
using CTI.FAS.Core.FAS;
using CTI.FAS.Infrastructure.Data;
using MediatR;
using CTI.Common.Utility.Extensions;
using Microsoft.EntityFrameworkCore;

namespace CTI.FAS.Application.Features.FAS.PaymentTransaction.Queries;

public record GetPaymentTransactionQuery : BaseQuery, IRequest<PagedListResponse<PaymentTransactionState>>;

public class GetPaymentTransactionQueryHandler : BaseQueryHandler<ApplicationContext, PaymentTransactionState, GetPaymentTransactionQuery>, IRequestHandler<GetPaymentTransactionQuery, PagedListResponse<PaymentTransactionState>>
{
    public GetPaymentTransactionQueryHandler(ApplicationContext context) : base(context)
    {
    }
	public override async Task<PagedListResponse<PaymentTransactionState>> Handle(GetPaymentTransactionQuery request, CancellationToken cancellationToken = default) =>
		await Context.Set<PaymentTransactionState>().Include(l=>l.Batch).Include(l=>l.EnrolledPayee)
		.AsNoTracking().ToPagedResponse(request.SearchColumns, request.SearchValue,
			request.SortColumn, request.SortOrder,
			request.PageNumber, request.PageSize,
			cancellationToken);	
}
