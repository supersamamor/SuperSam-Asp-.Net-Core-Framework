using CompanyPL.Common.Core.Queries;
using CompanyPL.Common.Utility.Models;
using CompanyPL.ProjectPL.Core.ProjectPL;
using CompanyPL.ProjectPL.Infrastructure.Data;
using MediatR;
using CompanyPL.Common.Utility.Extensions;
using Microsoft.EntityFrameworkCore;

namespace CompanyPL.ProjectPL.Application.Features.ProjectPL.ContactInformation.Queries;

public record GetContactInformationQuery : BaseQuery, IRequest<PagedListResponse<ContactInformationState>>;

public class GetContactInformationQueryHandler : BaseQueryHandler<ApplicationContext, ContactInformationState, GetContactInformationQuery>, IRequestHandler<GetContactInformationQuery, PagedListResponse<ContactInformationState>>
{
    public GetContactInformationQueryHandler(ApplicationContext context) : base(context)
    {
    }
	public override async Task<PagedListResponse<ContactInformationState>> Handle(GetContactInformationQuery request, CancellationToken cancellationToken = default) =>
		await Context.Set<ContactInformationState>().Include(l=>l.Employee)
		.AsNoTracking().ToPagedResponse(request.SearchColumns, request.SearchValue,
			request.SortColumn, request.SortOrder,
			request.PageNumber, request.PageSize,
			cancellationToken);	
}
