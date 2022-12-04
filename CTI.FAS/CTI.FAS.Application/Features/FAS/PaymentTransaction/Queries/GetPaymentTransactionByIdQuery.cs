using CTI.Common.Core.Queries;
using CTI.FAS.Core.FAS;
using CTI.FAS.Infrastructure.Data;
using LanguageExt;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CTI.FAS.Application.Features.FAS.PaymentTransaction.Queries;

public record GetPaymentTransactionByIdQuery(string Id) : BaseQueryById(Id), IRequest<Option<PaymentTransactionState>>;

public class GetPaymentTransactionByIdQueryHandler : BaseQueryByIdHandler<ApplicationContext, PaymentTransactionState, GetPaymentTransactionByIdQuery>, IRequestHandler<GetPaymentTransactionByIdQuery, Option<PaymentTransactionState>>
{
    public GetPaymentTransactionByIdQueryHandler(ApplicationContext context) : base(context)
    {
    }
	
	public override async Task<Option<PaymentTransactionState>> Handle(GetPaymentTransactionByIdQuery request, CancellationToken cancellationToken = default)
	{
		return await Context.PaymentTransaction.Include(l=>l.Batch).Include(l=>l.EnrolledPayee)
			.Where(e => e.Id == request.Id).AsNoTracking().FirstOrDefaultAsync(cancellationToken);
	}
	
}
