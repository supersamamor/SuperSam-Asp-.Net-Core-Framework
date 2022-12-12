using CTI.Common.Core.Queries;
using CTI.Common.Utility.Models;
using CTI.FAS.Core.FAS;
using CTI.FAS.Infrastructure.Data;
using MediatR;
using CTI.Common.Utility.Extensions;
using Microsoft.EntityFrameworkCore;

namespace CTI.FAS.Application.Features.FAS.Bank.Queries;

public record GetBankQuery : BaseQuery, IRequest<PagedListResponse<BankState>>
{
    public string? CompanyId { get; set; }
}

public class GetBankQueryHandler : BaseQueryHandler<ApplicationContext, BankState, GetBankQuery>, IRequestHandler<GetBankQuery, PagedListResponse<BankState>>
{
    public GetBankQueryHandler(ApplicationContext context) : base(context)
    {
    }
    public override async Task<PagedListResponse<BankState>> Handle(GetBankQuery request, CancellationToken cancellationToken = default)
    {
        var query = Context.Bank.Include(l => l.Company).AsNoTracking();
        if (!string.IsNullOrEmpty(request.CompanyId))
        {
            query = query.Where(l => l.CompanyId == request.CompanyId);
        }
        return await query.ToPagedResponse(request.SearchColumns, request.SearchValue,
            request.SortColumn, request.SortOrder,
            request.PageNumber, request.PageSize,
            cancellationToken);
    }

}
