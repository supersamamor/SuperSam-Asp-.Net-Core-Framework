using CTI.Common.Core.Queries;
using CTI.FAS.Core.FAS;
using CTI.FAS.Infrastructure.Data;
using LanguageExt;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CTI.FAS.Application.Features.FAS.EnrolledPayeeEmail.Queries;

public record GetEnrolledPayeeEmailByIdQuery(string Id) : BaseQueryById(Id), IRequest<Option<EnrolledPayeeEmailState>>;

public class GetEnrolledPayeeEmailByIdQueryHandler : BaseQueryByIdHandler<ApplicationContext, EnrolledPayeeEmailState, GetEnrolledPayeeEmailByIdQuery>, IRequestHandler<GetEnrolledPayeeEmailByIdQuery, Option<EnrolledPayeeEmailState>>
{
    public GetEnrolledPayeeEmailByIdQueryHandler(ApplicationContext context) : base(context)
    {
    }
	
	public override async Task<Option<EnrolledPayeeEmailState>> Handle(GetEnrolledPayeeEmailByIdQuery request, CancellationToken cancellationToken = default)
	{
		return await Context.EnrolledPayeeEmail.Include(l=>l.EnrolledPayee)
			.Where(e => e.Id == request.Id).AsNoTracking().FirstOrDefaultAsync(cancellationToken);
	}
	
}
