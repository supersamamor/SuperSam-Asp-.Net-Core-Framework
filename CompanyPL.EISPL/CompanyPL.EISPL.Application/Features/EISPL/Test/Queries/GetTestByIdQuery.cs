using CompanyPL.Common.Core.Queries;
using CompanyPL.EISPL.Core.EISPL;
using CompanyPL.EISPL.Infrastructure.Data;
using LanguageExt;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CompanyPL.EISPL.Application.Features.EISPL.Test.Queries;

public record GetTestByIdQuery(string Id) : BaseQueryById(Id), IRequest<Option<TestState>>;

public class GetTestByIdQueryHandler : BaseQueryByIdHandler<ApplicationContext, TestState, GetTestByIdQuery>, IRequestHandler<GetTestByIdQuery, Option<TestState>>
{
    public GetTestByIdQueryHandler(ApplicationContext context) : base(context)
    {
    }
	
	public override async Task<Option<TestState>> Handle(GetTestByIdQuery request, CancellationToken cancellationToken = default)
	{
		return await Context.Test.Include(l=>l.PLEmployee)
			.Where(e => e.Id == request.Id).AsNoTracking().FirstOrDefaultAsync(cancellationToken);
	}
	
}
