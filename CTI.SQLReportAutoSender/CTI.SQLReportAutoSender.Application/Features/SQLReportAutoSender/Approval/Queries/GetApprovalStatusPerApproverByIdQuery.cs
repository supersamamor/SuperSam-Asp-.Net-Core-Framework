using CTI.SQLReportAutoSender.Infrastructure.Data;
using LanguageExt;
using MediatR;
using Microsoft.EntityFrameworkCore;
using CTI.Common.Identity.Abstractions;

namespace CTI.SQLReportAutoSender.Application.Features.SQLReportAutoSender.Approval.Queries;

public record GetApprovalStatusPerApproverByIdQuery(string DataId) : IRequest<Option<string>>;

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
                      where b.ApproverUserId == _authenticatedUser.UserId && a.DataId == request.DataId
                      select b.Status).FirstOrDefaultAsync(cancellationToken);
    }
}
