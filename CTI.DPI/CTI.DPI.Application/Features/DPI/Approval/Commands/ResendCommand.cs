using CTI.DPI.Infrastructure.Data;
using CTI.Common.Core.Base.Models;
using CTI.Common.Identity.Abstractions;
using FluentValidation;
using LanguageExt;
using LanguageExt.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static LanguageExt.Prelude;

namespace CTI.DPI.Application.Features.DPI.Approval.Commands;

public record ResendCommand(string ApprovalId) : IRequest<Validation<Error, ResendResult>>;

public class ResendCommandHandler : IRequestHandler<ResendCommand, Validation<Error, ResendResult>>
{
    private readonly ApplicationContext _context;

    public ResendCommandHandler(ApplicationContext context)
    {
        _context = context; 
    }

    public async Task<Validation<Error, ResendResult>> Handle(ResendCommand request, CancellationToken cancellationToken) =>
        await Resend(request, cancellationToken);

    public async Task<Validation<Error, ResendResult>> Resend(ResendCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Approval.Where(l => l.Id == request.ApprovalId).SingleAsync(cancellationToken);
        entity.SetToPendingEmail();
        _context.Update(entity);
        _ = await _context.SaveChangesAsync(cancellationToken);
        return Success<Error, ResendResult>(new ResendResult(request.ApprovalId));
    }
}
public record ResendResult : BaseEntity
{
    public ResendResult(string ApprovalId)
    {
        this.Id = ApprovalId;
    }
}
