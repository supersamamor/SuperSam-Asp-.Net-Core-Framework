using CTI.Common.Core.Queries;
using CTI.FAS.Core.FAS;
using CTI.FAS.Infrastructure.Data;
using LanguageExt;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CTI.FAS.Application.Features.FAS.Creditor.Queries;

public record GetCreditorByIdQuery(string Id) : BaseQueryById(Id), IRequest<Option<CreditorState>>;

public class GetCreditorByIdQueryHandler : BaseQueryByIdHandler<ApplicationContext, CreditorState, GetCreditorByIdQuery>, IRequestHandler<GetCreditorByIdQuery, Option<CreditorState>>
{
    public GetCreditorByIdQueryHandler(ApplicationContext context) : base(context)
    {
    }
	
	public override async Task<Option<CreditorState>> Handle(GetCreditorByIdQuery request, CancellationToken cancellationToken = default)
	{
		return await Context.Creditor.Include(l=>l.Company)
			.Include(l=>l.CheckReleaseOptionList)
			.Include(l=>l.CreditorEmailList)
			.Where(e => e.Id == request.Id).AsNoTracking().FirstOrDefaultAsync(cancellationToken);
	}
	
}
