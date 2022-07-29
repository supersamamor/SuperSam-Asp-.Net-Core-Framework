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

namespace CTI.TenantSales.Application.Features.TenantSales.TenantLot.Commands;

public record DeleteTenantLotCommand : BaseCommand, IRequest<Validation<Error, TenantLotState>>;

public class DeleteTenantLotCommandHandler : BaseCommandHandler<ApplicationContext, TenantLotState, DeleteTenantLotCommand>, IRequestHandler<DeleteTenantLotCommand, Validation<Error, TenantLotState>>
{
    public DeleteTenantLotCommandHandler(ApplicationContext context,
                                       IMapper mapper,
                                       CompositeValidator<DeleteTenantLotCommand> validator) : base(context, mapper, validator)
    {
    }

    public async Task<Validation<Error, TenantLotState>> Handle(DeleteTenantLotCommand request, CancellationToken cancellationToken) =>
        await Validators.ValidateTAsync(request, cancellationToken).BindT(
            async request => await Delete(request.Id, cancellationToken));
}


public class DeleteTenantLotCommandValidator : AbstractValidator<DeleteTenantLotCommand>
{
    readonly ApplicationContext _context;

    public DeleteTenantLotCommandValidator(ApplicationContext context)
    {
        _context = context;
        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<TenantLotState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("TenantLot with id {PropertyValue} does not exists");
    }
}
