using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Infrastructure.Data;
using CTI.Common.Core.Base.Models;
using CTI.Common.Identity.Abstractions;
using FluentValidation;
using LanguageExt;
using LanguageExt.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static LanguageExt.Prelude;

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Features.ProjectNamePlaceHolder.Approval.Commands;

public record ResendCommand(string ApprovalId) : IRequest<Validation<Error, ResendResult>>;

public class ResendCommandHandler : IRequestHandler<ResendCommand, Validation<Error, ResendResult>>
{
    private readonly ApplicationContext _context;
    private readonly IAuthenticatedUser _authenticatedUser;
    public ResendCommandHandler(ApplicationContext context, IAuthenticatedUser authenticatedUser)
    {
        _context = context;
        _authenticatedUser = authenticatedUser;
    }

    public async Task<Validation<Error, ResendResult>> Handle(ResendCommand request, CancellationToken cancellationToken) =>
        await Resend(request, cancellationToken);

    public async Task<Validation<Error, ResendResult>> Resend(ResendCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Approval.Where(l => l.Id == request.ApprovalId).SingleAsync();
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
