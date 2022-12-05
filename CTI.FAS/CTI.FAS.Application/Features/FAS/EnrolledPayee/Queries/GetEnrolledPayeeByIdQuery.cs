using CTI.Common.Core.Queries;
using CTI.FAS.Core.FAS;
using CTI.FAS.Infrastructure.Data;
using LanguageExt;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CTI.FAS.Application.Features.FAS.EnrolledPayee.Queries;

public record GetEnrolledPayeeByIdQuery(string Id) : BaseQueryById(Id), IRequest<Option<EnrolledPayeeState>>;

public class GetEnrolledPayeeByIdQueryHandler : BaseQueryByIdHandler<ApplicationContext, EnrolledPayeeState, GetEnrolledPayeeByIdQuery>, IRequestHandler<GetEnrolledPayeeByIdQuery, Option<EnrolledPayeeState>>
{
    public GetEnrolledPayeeByIdQueryHandler(ApplicationContext context) : base(context)
    {
    }
	
	public override async Task<Option<EnrolledPayeeState>> Handle(GetEnrolledPayeeByIdQuery request, CancellationToken cancellationToken = default)
	{
		return await Context.EnrolledPayee.Include(l=>l.Company).ThenInclude(l=>l!.DatabaseConnectionSetup).Include(l=>l.Creditor)
			.Include(l=>l.EnrolledPayeeEmailList)
			.Where(e => e.Id == request.Id).AsNoTracking().FirstOrDefaultAsync(cancellationToken);
	}
	
}
