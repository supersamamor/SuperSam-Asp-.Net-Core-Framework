using CTI.Common.Core.Queries;
using CTI.FAS.Core.FAS;
using CTI.FAS.Infrastructure.Data;
using LanguageExt;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CTI.FAS.Application.Features.FAS.UserEntity.Queries;

public record GetUserEntityByIdQuery(string Id) : BaseQueryById(Id), IRequest<Option<UserEntityState>>;

public class GetUserEntityByIdQueryHandler : BaseQueryByIdHandler<ApplicationContext, UserEntityState, GetUserEntityByIdQuery>, IRequestHandler<GetUserEntityByIdQuery, Option<UserEntityState>>
{
    public GetUserEntityByIdQueryHandler(ApplicationContext context) : base(context)
    {
    }
	
	public override async Task<Option<UserEntityState>> Handle(GetUserEntityByIdQuery request, CancellationToken cancellationToken = default)
	{
		return await Context.UserEntity.Include(l=>l.Company)
			.Where(e => e.Id == request.Id).AsNoTracking().FirstOrDefaultAsync(cancellationToken);
	}
	
}
