using CTI.Common.Core.Queries;
using CTI.FAS.Core.FAS;
using CTI.FAS.Infrastructure.Data;
using LanguageExt;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CTI.FAS.Application.Features.FAS.CreditorEmail.Queries;

public record GetCreditorEmailByIdQuery(string Id) : BaseQueryById(Id), IRequest<Option<CreditorEmailState>>;

public class GetCreditorEmailByIdQueryHandler : BaseQueryByIdHandler<ApplicationContext, CreditorEmailState, GetCreditorEmailByIdQuery>, IRequestHandler<GetCreditorEmailByIdQuery, Option<CreditorEmailState>>
{
    public GetCreditorEmailByIdQueryHandler(ApplicationContext context) : base(context)
    {
    }
	
	public override async Task<Option<CreditorEmailState>> Handle(GetCreditorEmailByIdQuery request, CancellationToken cancellationToken = default)
	{
		return await Context.CreditorEmail.Include(l=>l.Creditor)
			.Where(e => e.Id == request.Id).AsNoTracking().FirstOrDefaultAsync(cancellationToken);
	}
	
}
