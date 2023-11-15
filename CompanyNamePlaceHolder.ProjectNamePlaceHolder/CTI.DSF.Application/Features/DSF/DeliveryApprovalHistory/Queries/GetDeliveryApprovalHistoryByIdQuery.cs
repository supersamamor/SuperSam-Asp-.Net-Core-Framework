using CTI.Common.Core.Queries;
using CTI.DSF.Core.DSF;
using CTI.DSF.Infrastructure.Data;
using LanguageExt;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CTI.DSF.Application.Features.DSF.DeliveryApprovalHistory.Queries;

public record GetDeliveryApprovalHistoryByIdQuery(string Id) : BaseQueryById(Id), IRequest<Option<DeliveryApprovalHistoryState>>;

public class GetDeliveryApprovalHistoryByIdQueryHandler : BaseQueryByIdHandler<ApplicationContext, DeliveryApprovalHistoryState, GetDeliveryApprovalHistoryByIdQuery>, IRequestHandler<GetDeliveryApprovalHistoryByIdQuery, Option<DeliveryApprovalHistoryState>>
{
    public GetDeliveryApprovalHistoryByIdQueryHandler(ApplicationContext context) : base(context)
    {
    }
	
	public override async Task<Option<DeliveryApprovalHistoryState>> Handle(GetDeliveryApprovalHistoryByIdQuery request, CancellationToken cancellationToken = default)
	{
		return await Context.DeliveryApprovalHistory.Include(l=>l.Delivery)
			.Where(e => e.Id == request.Id).AsNoTracking().FirstOrDefaultAsync(cancellationToken);
	}
	
}
