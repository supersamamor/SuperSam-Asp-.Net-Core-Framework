using AutoMapper;
using CTI.Common.Core.Commands;
using CTI.Common.Data;
using CTI.Common.Utility.Validators;
using CTI.FAS.Core.FAS;
using CTI.FAS.Infrastructure.Data;
using FluentValidation;
using LanguageExt;
using LanguageExt.Common;
using MediatR;

namespace CTI.FAS.Application.Features.FAS.Tenant.Commands;

public record DeleteTenantCommand : BaseCommand, IRequest<Validation<Error, TenantState>>;

public class DeleteTenantCommandHandler : BaseCommandHandler<ApplicationContext, TenantState, DeleteTenantCommand>, IRequestHandler<DeleteTenantCommand, Validation<Error, TenantState>>
{
    public DeleteTenantCommandHandler(ApplicationContext context,
                                       IMapper mapper,
                                       CompositeValidator<DeleteTenantCommand> validator) : base(context, mapper, validator)
    {
    }

    public async Task<Validation<Error, TenantState>> Handle(DeleteTenantCommand request, CancellationToken cancellationToken) =>
        await Validators.ValidateTAsync(request, cancellationToken).BindT(
            async request => await Delete(request.Id, cancellationToken));
}


public class DeleteTenantCommandValidator : AbstractValidator<DeleteTenantCommand>
{
    readonly ApplicationContext _context;

    public DeleteTenantCommandValidator(ApplicationContext context)
    {
        _context = context;
        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<TenantState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("Tenant with id {PropertyValue} does not exists");
    }
}
