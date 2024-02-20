using CTI.Common.Core.Queries;
using CTI.DSF.Core.DSF;
using CTI.DSF.Infrastructure.Data;
using LanguageExt;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CTI.DSF.Application.Features.DSF.Company.Queries;

public record GetCompanyByIdQuery(string Id) : BaseQueryById(Id), IRequest<Option<CompanyState>>;

public class GetCompanyByIdQueryHandler : BaseQueryByIdHandler<ApplicationContext, CompanyState, GetCompanyByIdQuery>, IRequestHandler<GetCompanyByIdQuery, Option<CompanyState>>
{
    public GetCompanyByIdQueryHandler(ApplicationContext context) : base(context)
    {
    }
	
	public override async Task<Option<CompanyState>> Handle(GetCompanyByIdQuery request, CancellationToken cancellationToken = default)
	{
		return await Context.Company
			.Include(l=>l.TaskCompanyAssignmentList)
			.Where(e => e.Id == request.Id).AsNoTracking().FirstOrDefaultAsync(cancellationToken);
	}
	
}
