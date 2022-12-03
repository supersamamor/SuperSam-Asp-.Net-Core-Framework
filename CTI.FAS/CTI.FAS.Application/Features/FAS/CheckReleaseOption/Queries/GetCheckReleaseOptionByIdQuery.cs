using CTI.Common.Core.Queries;
using CTI.FAS.Core.FAS;
using CTI.FAS.Infrastructure.Data;
using LanguageExt;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CTI.FAS.Application.Features.FAS.CheckReleaseOption.Queries;

public record GetCheckReleaseOptionByIdQuery(string Id) : BaseQueryById(Id), IRequest<Option<CheckReleaseOptionState>>;

public class GetCheckReleaseOptionByIdQueryHandler : BaseQueryByIdHandler<ApplicationContext, CheckReleaseOptionState, GetCheckReleaseOptionByIdQuery>, IRequestHandler<GetCheckReleaseOptionByIdQuery, Option<CheckReleaseOptionState>>
{
    public GetCheckReleaseOptionByIdQueryHandler(ApplicationContext context) : base(context)
    {
    }
	
	public override async Task<Option<CheckReleaseOptionState>> Handle(GetCheckReleaseOptionByIdQuery request, CancellationToken cancellationToken = default)
	{
		return await Context.CheckReleaseOption.Include(l=>l.Creditor)
			.Where(e => e.Id == request.Id).AsNoTracking().FirstOrDefaultAsync(cancellationToken);
	}
	
}
