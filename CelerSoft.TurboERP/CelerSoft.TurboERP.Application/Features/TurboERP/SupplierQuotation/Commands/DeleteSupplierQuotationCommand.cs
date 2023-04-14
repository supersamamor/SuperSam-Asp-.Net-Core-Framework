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

namespace CelerSoft.TurboERP.Application.Features.TurboERP.SupplierQuotation.Commands;

public record DeleteSupplierQuotationCommand : BaseCommand, IRequest<Validation<Error, SupplierQuotationState>>;

public class DeleteSupplierQuotationCommandHandler : BaseCommandHandler<ApplicationContext, SupplierQuotationState, DeleteSupplierQuotationCommand>, IRequestHandler<DeleteSupplierQuotationCommand, Validation<Error, SupplierQuotationState>>
{
    public DeleteSupplierQuotationCommandHandler(ApplicationContext context,
                                       IMapper mapper,
                                       CompositeValidator<DeleteSupplierQuotationCommand> validator) : base(context, mapper, validator)
    {
    }

    public async Task<Validation<Error, SupplierQuotationState>> Handle(DeleteSupplierQuotationCommand request, CancellationToken cancellationToken) =>
        await Validators.ValidateTAsync(request, cancellationToken).BindT(
            async request => await Delete(request.Id, cancellationToken));
}


public class DeleteSupplierQuotationCommandValidator : AbstractValidator<DeleteSupplierQuotationCommand>
{
    readonly ApplicationContext _context;

    public DeleteSupplierQuotationCommandValidator(ApplicationContext context)
    {
        _context = context;
        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<SupplierQuotationState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("SupplierQuotation with id {PropertyValue} does not exists");
    }
}
