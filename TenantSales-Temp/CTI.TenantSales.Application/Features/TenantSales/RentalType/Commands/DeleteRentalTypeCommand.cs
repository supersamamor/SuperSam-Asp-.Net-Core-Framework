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

namespace CTI.TenantSales.Application.Features.TenantSales.RentalType.Commands;

public record DeleteRentalTypeCommand : BaseCommand, IRequest<Validation<Error, RentalTypeState>>;

public class DeleteRentalTypeCommandHandler : BaseCommandHandler<ApplicationContext, RentalTypeState, DeleteRentalTypeCommand>, IRequestHandler<DeleteRentalTypeCommand, Validation<Error, RentalTypeState>>
{
    public DeleteRentalTypeCommandHandler(ApplicationContext context,
                                       IMapper mapper,
                                       CompositeValidator<DeleteRentalTypeCommand> validator) : base(context, mapper, validator)
    {
    }

    public async Task<Validation<Error, RentalTypeState>> Handle(DeleteRentalTypeCommand request, CancellationToken cancellationToken) =>
        await Validators.ValidateTAsync(request, cancellationToken).BindT(
            async request => await Delete(request.Id, cancellationToken));
}


public class DeleteRentalTypeCommandValidator : AbstractValidator<DeleteRentalTypeCommand>
{
    readonly ApplicationContext _context;

    public DeleteRentalTypeCommandValidator(ApplicationContext context)
    {
        _context = context;
        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<RentalTypeState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("RentalType with id {PropertyValue} does not exists");
    }
}
