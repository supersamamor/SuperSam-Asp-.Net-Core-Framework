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

namespace CTI.TenantSales.Application.Features.TenantSales.BusinessUnit.Commands;

public record DeleteBusinessUnitCommand : BaseCommand, IRequest<Validation<Error, BusinessUnitState>>;

public class DeleteBusinessUnitCommandHandler : BaseCommandHandler<ApplicationContext, BusinessUnitState, DeleteBusinessUnitCommand>, IRequestHandler<DeleteBusinessUnitCommand, Validation<Error, BusinessUnitState>>
{
    public DeleteBusinessUnitCommandHandler(ApplicationContext context,
                                       IMapper mapper,
                                       CompositeValidator<DeleteBusinessUnitCommand> validator) : base(context, mapper, validator)
    {
    }

    public async Task<Validation<Error, BusinessUnitState>> Handle(DeleteBusinessUnitCommand request, CancellationToken cancellationToken) =>
        await Validators.ValidateTAsync(request, cancellationToken).BindT(
            async request => await Delete(request.Id, cancellationToken));
}


public class DeleteBusinessUnitCommandValidator : AbstractValidator<DeleteBusinessUnitCommand>
{
    readonly ApplicationContext _context;

    public DeleteBusinessUnitCommandValidator(ApplicationContext context)
    {
        _context = context;
        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<BusinessUnitState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("BusinessUnit with id {PropertyValue} does not exists");
    }
}
