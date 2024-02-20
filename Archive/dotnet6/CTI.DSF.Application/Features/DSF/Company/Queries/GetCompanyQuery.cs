using CTI.Common.Core.Queries;
using CTI.Common.Utility.Models;
using CTI.DSF.Core.DSF;
using CTI.DSF.Infrastructure.Data;
using MediatR;
using CTI.Common.Utility.Extensions;
using Microsoft.EntityFrameworkCore;

namespace CTI.DSF.Application.Features.DSF.Company.Queries;

public record GetCompanyQuery : BaseQuery, IRequest<PagedListResponse<CompanyState>>;

public class GetCompanyQueryHandler : BaseQueryHandler<ApplicationContext, CompanyState, GetCompanyQuery>, IRequestHandler<GetCompanyQuery, PagedListResponse<CompanyState>>
{
    public GetCompanyQueryHandler(ApplicationContext context) : base(context)
    {
    }
		
}
