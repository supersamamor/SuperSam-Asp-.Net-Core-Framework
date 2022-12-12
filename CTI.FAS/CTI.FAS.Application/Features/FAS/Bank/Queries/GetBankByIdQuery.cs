using CTI.Common.Core.Queries;
using CTI.FAS.Core.FAS;
using CTI.FAS.Infrastructure.Data;
using LanguageExt;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CTI.FAS.Application.Features.FAS.Bank.Queries;

public record GetBankByIdQuery(string Id) : BaseQueryById(Id), IRequest<Option<BankState>>;

public class GetBankByIdQueryHandler : BaseQueryByIdHandler<ApplicationContext, BankState, GetBankByIdQuery>, IRequestHandler<GetBankByIdQuery, Option<BankState>>
{
    public GetBankByIdQueryHandler(ApplicationContext context) : base(context)
    {
    }
	
	public override async Task<Option<BankState>> Handle(GetBankByIdQuery request, CancellationToken cancellationToken = default)
	{
		return await Context.Bank.Include(l=>l.Company)
			.Where(e => e.Id == request.Id).AsNoTracking().FirstOrDefaultAsync(cancellationToken);
	}
	
}
