using CTI.ELMS.Infrastructure.Data;
using CTI.Common.Core.Base.Models;
using CTI.Common.Identity.Abstractions;
using FluentValidation;
using LanguageExt;
using LanguageExt.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static LanguageExt.Prelude;

namespace CTI.ELMS.Application.Features.ELMS.Approval.Commands;

public record RejectCommand(string DataId, string RejectRemarks) : IRequest<Validation<Error, RejectResult>>;

public class RejectCommandHandler : IRequestHandler<RejectCommand, Validation<Error, RejectResult>>
{
    private readonly ApplicationContext _context;
    private readonly IAuthenticatedUser _authenticatedUser;
    public RejectCommandHandler(ApplicationContext context, IAuthenticatedUser authenticatedUser)
    {
        _context = context;
        _authenticatedUser = authenticatedUser;
    }

    public async Task<Validation<Error, RejectResult>> Handle(RejectCommand request, CancellationToken cancellationToken) =>
        await Reject(request, cancellationToken);

    public async Task<Validation<Error, RejectResult>> Reject(RejectCommand request, CancellationToken cancellationToken)
    {
        var entity = await (from a in _context.Approval
                            join b in _context.ApprovalRecord on a.ApprovalRecordId equals b.Id
                            where b.DataId == request.DataId && a.ApproverUserId == _authenticatedUser.UserId
                            select a).SingleAsync(cancellationToken);
        entity.Reject(request.RejectRemarks);
        _context.Update(entity);
        _ = await _context.SaveChangesAsync(cancellationToken);
        return Success<Error, RejectResult>(new RejectResult(request.DataId));
    }
}
public record RejectResult : BaseEntity
{
    public RejectResult(string dataId)
    {
        this.Id = dataId;
    }
}
