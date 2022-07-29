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

namespace CTI.TenantSales.Application.Features.TenantSales.TenantPOS.Commands;

public record DeleteTenantPOSCommand : BaseCommand, IRequest<Validation<Error, TenantPOSState>>;

public class DeleteTenantPOSCommandHandler : BaseCommandHandler<ApplicationContext, TenantPOSState, DeleteTenantPOSCommand>, IRequestHandler<DeleteTenantPOSCommand, Validation<Error, TenantPOSState>>
{
    public DeleteTenantPOSCommandHandler(ApplicationContext context,
                                       IMapper mapper,
                                       CompositeValidator<DeleteTenantPOSCommand> validator) : base(context, mapper, validator)
    {
    }

    public async Task<Validation<Error, TenantPOSState>> Handle(DeleteTenantPOSCommand request, CancellationToken cancellationToken) =>
        await Validators.ValidateTAsync(request, cancellationToken).BindT(
            async request => await Delete(request.Id, cancellationToken));
}


public class DeleteTenantPOSCommandValidator : AbstractValidator<DeleteTenantPOSCommand>
{
    readonly ApplicationContext _context;

    public DeleteTenantPOSCommandValidator(ApplicationContext context)
    {
        _context = context;
        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<TenantPOSState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("TenantPOS with id {PropertyValue} does not exists");
    }
}
