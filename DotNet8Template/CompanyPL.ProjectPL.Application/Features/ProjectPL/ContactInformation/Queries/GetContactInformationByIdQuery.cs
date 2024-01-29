using CompanyPL.Common.Core.Queries;
using CompanyPL.ProjectPL.Core.ProjectPL;
using CompanyPL.ProjectPL.Infrastructure.Data;
using LanguageExt;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CompanyPL.ProjectPL.Application.Features.ProjectPL.ContactInformation.Queries;

public record GetContactInformationByIdQuery(string Id) : BaseQueryById(Id), IRequest<Option<ContactInformationState>>;

public class GetContactInformationByIdQueryHandler : BaseQueryByIdHandler<ApplicationContext, ContactInformationState, GetContactInformationByIdQuery>, IRequestHandler<GetContactInformationByIdQuery, Option<ContactInformationState>>
{
    public GetContactInformationByIdQueryHandler(ApplicationContext context) : base(context)
    {
    }
	
	public override async Task<Option<ContactInformationState>> Handle(GetContactInformationByIdQuery request, CancellationToken cancellationToken = default)
	{
		return await Context.ContactInformation.Include(l=>l.Employee)
			.Where(e => e.Id == request.Id).AsNoTracking().FirstOrDefaultAsync(cancellationToken);
	}
	
}
