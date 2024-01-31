using CompanyPL.ProjectPL.Infrastructure.Data;
using LanguageExt;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CompanyPL.ProjectPL.Application.Features.ProjectPL.Approval.Queries;

public record GetApprovalStatusByIdQuery(string DataId) : IRequest<Option<string>>;

public class GetApprovalStatusByIdQueryHandler : IRequestHandler<GetApprovalStatusByIdQuery, Option<string>>
{
    private readonly ApplicationContext _context;
   
    public GetApprovalStatusByIdQueryHandler(ApplicationContext context)
    {
        _context = context;        
    }

    public async Task<Option<string>> Handle(GetApprovalStatusByIdQuery request, CancellationToken cancellationToken = default)
    {
        return await (from a in _context.ApprovalRecord
                      where a.DataId == request.DataId
                      select a.Status).FirstOrDefaultAsync(cancellationToken);
    }
}
