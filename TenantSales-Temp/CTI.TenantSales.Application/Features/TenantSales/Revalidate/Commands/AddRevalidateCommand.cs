using AutoMapper;
using CTI.Common.Core.Commands;
using CTI.Common.Data;
using CTI.Common.Utility.Validators;
using CTI.TenantSales.Core.TenantSales;
using CTI.TenantSales.Infrastructure.Data;
using FluentValidation;
using LanguageExt;
using LanguageExt.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static LanguageExt.Prelude;

namespace CTI.TenantSales.Application.Features.TenantSales.Revalidate.Commands;

public record AddRevalidateCommand : RevalidateState, IRequest<Validation<Error, RevalidateState>>;

public class AddRevalidateCommandHandler : BaseCommandHandler<ApplicationContext, RevalidateState, AddRevalidateCommand>, IRequestHandler<AddRevalidateCommand, Validation<Error, RevalidateState>>
{
    public AddRevalidateCommandHandler(ApplicationContext context,
                                    IMapper mapper,
                                    CompositeValidator<AddRevalidateCommand> validator) : base(context, mapper, validator)
    {
    }


    public async Task<Validation<Error, RevalidateState>> Handle(AddRevalidateCommand request, CancellationToken cancellationToken) =>
            await Validators.ValidateTAsync(request, cancellationToken).BindT(
                async request => await Add(request, cancellationToken));



}

public class AddRevalidateCommandValidator : AbstractValidator<AddRevalidateCommand>
{
    readonly ApplicationContext _context;

    public AddRevalidateCommandValidator(ApplicationContext context)
    {
        _context = context;

        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.NotExists<RevalidateState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("Revalidate with id {PropertyValue} already exists");

        RuleFor(x => new { x.ProjectId, x.TenantId, x.SalesDate })
            .MustAsync(async (a, cancellation) => await _context.NotExists<RevalidateState>(
                x => x.ProjectId == a.ProjectId
                && ((string.IsNullOrEmpty(a.TenantId)) || (!string.IsNullOrEmpty(a.TenantId) && x.TenantId == a.TenantId))
                && x.SalesDate == a.SalesDate
                && x.Status == Core.TenantSales.Status.Pending, cancellationToken: cancellation))
                  .WithMessage(a => $"This project, tenant and sales date combination is already queued for revalidation.");
    }
}
