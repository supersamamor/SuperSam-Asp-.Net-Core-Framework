using AutoMapper;
using CelerSoft.Common.Core.Commands;
using CelerSoft.Common.Data;
using CelerSoft.Common.Utility.Validators;
using CelerSoft.TurboERP.Core.TurboERP;
using CelerSoft.TurboERP.Infrastructure.Data;
using FluentValidation;
using LanguageExt;
using LanguageExt.Common;
using MediatR;

namespace CelerSoft.TurboERP.Application.Features.TurboERP.Supplier.Commands;

public record DeleteSupplierCommand : BaseCommand, IRequest<Validation<Error, SupplierState>>;

public class DeleteSupplierCommandHandler : BaseCommandHandler<ApplicationContext, SupplierState, DeleteSupplierCommand>, IRequestHandler<DeleteSupplierCommand, Validation<Error, SupplierState>>
{
    public DeleteSupplierCommandHandler(ApplicationContext context,
                                       IMapper mapper,
                                       CompositeValidator<DeleteSupplierCommand> validator) : base(context, mapper, validator)
    {
    }

    public async Task<Validation<Error, SupplierState>> Handle(DeleteSupplierCommand request, CancellationToken cancellationToken) =>
        await Validators.ValidateTAsync(request, cancellationToken).BindT(
            async request => await Delete(request.Id, cancellationToken));
}


public class DeleteSupplierCommandValidator : AbstractValidator<DeleteSupplierCommand>
{
    readonly ApplicationContext _context;

    public DeleteSupplierCommandValidator(ApplicationContext context)
    {
        _context = context;
        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<SupplierState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("Supplier with id {PropertyValue} does not exists");
    }
}
