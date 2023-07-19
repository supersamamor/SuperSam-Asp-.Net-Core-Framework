using CompanyPL.Common.Core.Queries;
using CompanyPL.EISPL.Core.EISPL;
using CompanyPL.EISPL.Infrastructure.Data;
using LanguageExt;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CompanyPL.EISPL.Application.Features.EISPL.PLContactInformation.Queries;

public record GetPLContactInformationByIdQuery(string Id) : BaseQueryById(Id), IRequest<Option<PLContactInformationState>>;

public class GetPLContactInformationByIdQueryHandler : BaseQueryByIdHandler<ApplicationContext, PLContactInformationState, GetPLContactInformationByIdQuery>, IRequestHandler<GetPLContactInformationByIdQuery, Option<PLContactInformationState>>
{
    public GetPLContactInformationByIdQueryHandler(ApplicationContext context) : base(context)
    {
    }
	
	public override async Task<Option<PLContactInformationState>> Handle(GetPLContactInformationByIdQuery request, CancellationToken cancellationToken = default)
	{
		return await Context.PLContactInformation.Include(l=>l.PLEmployee)
			.Where(e => e.Id == request.Id).AsNoTracking().FirstOrDefaultAsync(cancellationToken);
	}
	
}
