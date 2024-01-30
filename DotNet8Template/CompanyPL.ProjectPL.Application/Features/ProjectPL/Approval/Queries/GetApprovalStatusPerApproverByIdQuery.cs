using CompanyPL.ProjectPL.Infrastructure.Data;
using LanguageExt;
using MediatR;
using Microsoft.EntityFrameworkCore;
using CompanyPL.Common.Identity.Abstractions;
using CompanyPL.ProjectPL.Core.ProjectPL;

namespace CompanyPL.ProjectPL.Application.Features.ProjectPL.Approval.Queries;

public record GetApprovalStatusPerApproverByIdQuery(string DataId, string Module) : IRequest<Option<string>>;

public class GetApprovalStatusPerApproverByIdQueryHandler : IRequestHandler<GetApprovalStatusPerApproverByIdQuery, Option<string>>
{
    private readonly ApplicationContext _context;
    private readonly IAuthenticatedUser _authenticatedUser;
    public GetApprovalStatusPerApproverByIdQueryHandler(ApplicationContext context, IAuthenticatedUser authenticatedUser)
    {
        _context = context;
        _authenticatedUser = authenticatedUser;
    }

    public async Task<Option<string>> Handle(GetApprovalStatusPerApproverByIdQuery request, CancellationToken cancellationToken = default)
    {   
       return await (from a in _context.ApprovalRecord
                            join b in _context.Approval on a.Id equals b.ApprovalRecordId
                            join c in _context.ApproverSetup on a.ApproverSetupId equals c.Id
                            where b.ApproverUserId == _authenticatedUser.UserId && a.DataId == request.DataId
                            && c.TableName == request.Module
                            select a.Status).FirstOrDefaultAsync(cancellationToken);
    }
}
