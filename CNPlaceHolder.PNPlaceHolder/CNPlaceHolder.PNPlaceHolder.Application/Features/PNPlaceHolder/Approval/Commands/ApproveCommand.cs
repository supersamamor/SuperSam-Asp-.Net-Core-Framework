using CNPlaceHolder.PNPlaceHolder.Core.PNPlaceHolder;
using CNPlaceHolder.PNPlaceHolder.Infrastructure.Data;
using CNPlaceHolder.Common.Core.Base.Models;
using CNPlaceHolder.Common.Identity.Abstractions;
using FluentValidation;
using LanguageExt;
using LanguageExt.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static LanguageExt.Prelude;

namespace CNPlaceHolder.PNPlaceHolder.Application.Features.PNPlaceHolder.Approval.Commands;

public record ApproveCommand(string DataId, string ApprovalRemarks) : IRequest<Validation<Error, ApprovalResult>>;

public class ApproveCommandHandler : IRequestHandler<ApproveCommand, Validation<Error, ApprovalResult>>
{
    private readonly ApplicationContext _context;
    private readonly IAuthenticatedUser _authenticatedUser;
    public ApproveCommandHandler(ApplicationContext context, IAuthenticatedUser authenticatedUser)
    {
        _context = context;
        _authenticatedUser = authenticatedUser;
    }

    public async Task<Validation<Error, ApprovalResult>> Handle(ApproveCommand request, CancellationToken cancellationToken) =>
        await Approve(request, cancellationToken);

    public async Task<Validation<Error, ApprovalResult>> Approve(ApproveCommand request, CancellationToken cancellationToken)
    {
        var entity = await (from a in _context.Approval
                            join b in _context.ApprovalRecord on a.ApprovalRecordId equals b.Id
                            where b.DataId == request.DataId && a.ApproverUserId == _authenticatedUser.UserId
                            select a).SingleAsync(cancellationToken);
        entity.Approve(request.ApprovalRemarks);
        _context.Update(entity);
        await SetPendingEmailForInSequence(entity, cancellationToken);
        await SkipSameSequence(entity, cancellationToken);
        _ = await _context.SaveChangesAsync(cancellationToken);
        return Success<Error, ApprovalResult>(new ApprovalResult(request.DataId));
    }
    private async Task SetPendingEmailForInSequence(ApprovalState entity, CancellationToken cancellationToken)
    {
        var nextApprovalList = await _context.Approval.Where(l => l.ApprovalRecordId == entity.ApprovalRecordId && l.Sequence == entity.Sequence + 1).ToListAsync(cancellationToken);
        foreach (var nextApproval in nextApprovalList)
        {
            nextApproval.SetToPendingEmail();
            _context.Update(nextApproval);
        }     
    }
    private async Task SkipSameSequence(ApprovalState entity, CancellationToken cancellationToken)
    {
        var approvalToSkipList = await _context.Approval.Where(l => l.ApprovalRecordId == entity.ApprovalRecordId && l.Sequence == entity.Sequence && l.Id != entity.Id).ToListAsync(cancellationToken);
        foreach (var approvalToSkip in approvalToSkipList)
        {
            approvalToSkip.Skip();
            _context.Update(approvalToSkip);
        }       
    }
}
public record ApprovalResult : BaseEntity
{
    public ApprovalResult(string dataId)
    {
        this.Id = dataId;
    }
}
